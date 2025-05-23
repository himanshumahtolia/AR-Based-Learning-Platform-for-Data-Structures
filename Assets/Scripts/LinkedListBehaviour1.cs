using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;
using TMPro;
public class LinkedListBehaviour1 : MonoBehaviour
{
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;
    private GameObject touchedObject;
    private GameObject CurrentObject;
    private int RemovedLinkPos;
    public List<GameObject> ChainLinks = new List<GameObject>();
    public List<GameObject> ActiveChainLinks = new List<GameObject>();
    private float RangeVarX;
    private float RangeVarY;
    private bool Check = true;
    private float NearVal =1.1f;
    public TextMeshProUGUI TextContent;
    public Toggle CheckBoxSnip;
    public Toggle CheckBoxAna;

    
    public GameObject ANE;
    public GameObject ANB;
    public GameObject DNE;
    public GameObject DNB;
    // public int CurrentObjectTag;
    void Update() {
        DragObj();
        for (int z = 0; z < ChainLinks.Count; z++)
        {   
        if(ChainLinks[z]){
            for (int i = 0; i < ChainLinks.Count; i++)
            {
                ChainLinks[i].transform.position = new Vector3(ChainLinks[i].transform.position.x,ChainLinks[i].transform.position.y,5);
            }
        }
        }
        if(toDrag!=null){
        TextContent.text = ""+toDrag.position;
        Debug.Log(""+toDrag.position);
        }
         
    }
     void Start() {
        ActiveChainLinks.Add(ChainLinks[0]);
        ChainLinks[0].transform.position = new Vector3(-1f,-1.5f,5);
        for (int i = 1; i < ChainLinks.Count; i++)
        {
            ChainLinks[i].transform.position = new Vector3(10,2.5f,5);
        }
    }

   
    public void DragObj(){
    
    Vector3 v3;

    if (Input.touchCount != 1)
    {
            dragging = false;
            return;
    }
    Touch touch = Input.touches[0];
    Vector3 pos = touch.position;
   
    if (touch.phase == TouchPhase.Began)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            touchedObject = hit.transform.gameObject;
            if(hit.collider.tag == touchedObject.transform.tag){
                toDrag = hit.transform;
                // dist  = hit.transform.position.z - Camera.main.transform.position.z;
                dist = 5;
                v3 = new Vector3(pos.x, pos.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                offset = toDrag.position - v3;
                dragging = true;
               
            }
          
        }
        
    }
    
    if (dragging && touch.phase == TouchPhase.Moved)
    {
        v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
        v3 = Camera.main.ScreenToWorldPoint(v3);
         ANE.SetActive(true);
                if(ANB.activeSelf == true || DNB.activeSelf == true || DNE.activeSelf == true){
                    ANB.SetActive(false); 
                    DNB.SetActive(false); 
                    DNE.SetActive(false); 
                }
        if(toDrag == null){ 
        }
        else{
            toDrag.position = v3 + offset;
            CurrentObject = GameObject.FindGameObjectWithTag(toDrag.tag);
        }
         if(CurrentObject!=null & CurrentObject.GetComponent<Outline>()==false){
                CurrentObject.AddComponent<Outline>();
        }
        if(ActiveChainLinks.Contains(CurrentObject)==false && CurrentObject!=null){
          ActiveChainLinks.Add(CurrentObject);
        }
        if(CheckBoxSnip.isOn){
            CheckForSnip();
        } 
    }
    if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;
            if(CurrentObject!=null){
                DestroyImmediate(CurrentObject.GetComponent<Outline>());
            }
            if(CheckBoxAna.isOn){
                CheckForNearbyLinks();
                Debug.Log("Int="+CheckForNearbyLinks());
                AddingNodeBetween();
            }
        }
    }
    public void RemoveLinkDisp(){
          DNB.SetActive(true);
         if(ANE.activeSelf == true || DNE.activeSelf == true || ANB.activeSelf == true){
                ANE.SetActive(false); 
                DNE.SetActive(false); 
                ANB.SetActive(false); 
            } 
            
    }
    async public void RemoveLink(){
        if(toDrag & CurrentObject !=null){
            RemovedLinkPos = int.Parse(CurrentObject.tag);
                Destroy(ActiveChainLinks[RemovedLinkPos]);
                Destroy(ChainLinks[RemovedLinkPos]);
            for (int i = RemovedLinkPos+1; i < ChainLinks.Count; i++)
                {
                    ChainLinks[i].tag = (int.Parse(ChainLinks[i].tag) - 1).ToString();
                }
                ChainLinks.RemoveAt(RemovedLinkPos);
                ActiveChainLinks.RemoveAt(RemovedLinkPos);
                await Task.Delay(1000);
                if(ActiveChainLinks.Count != RemovedLinkPos){
                    for (int i = RemovedLinkPos; i < ActiveChainLinks.Count; i++)
                    {
                        ActiveChainLinks[i].transform.position = ActiveChainLinks[i].transform.position -   new Vector3(2.5f,0,0);
                        Debug.Log(""+ActiveChainLinks[i].name);
                    }
                }
        }
    }
    void Snipping(){
        if(toDrag!=null & CurrentObject!=null){
            int position = int.Parse(CurrentObject.tag);
            toDrag.transform.position = toDrag.transform.position - new Vector3(0,2.7f,0);
            if(position == 0){
                toDrag.transform.position =  ActiveChainLinks[position].transform.position + new Vector3(2.5f,0,0);
            }
            else{
                toDrag.transform.position =  ActiveChainLinks[position-1].transform.position + new Vector3(2.5f,0,0);
            }
        }
    }
    
     void CheckForSnip(){
        if(ChainLinks[1]){
            NearVal = 3.5f;
            if( RangeX() < NearVal ){
                Snipping();
            }
        }
        if(ChainLinks[2]||ChainLinks[3]){
            NearVal = 2.5f;
            if( RangeX() < NearVal ){
                Snipping();
            }
        }
    }

    float RangeX(){
        if (CurrentObject != null)
        {   
            int position = int.Parse(CurrentObject.tag);
            if(position==0){
                RangeVarX = CurrentObject.transform.position.x - ActiveChainLinks[position].transform.position.x; 
            }
            else{
                RangeVarX = CurrentObject.transform.position.x - ActiveChainLinks[position-1].transform.position.x; 
            }
            return RangeVarX;
        }
        else{
            return RangeVarX;
        } 
    }
     int  CheckForNearbyLinks(){   
        if(toDrag!=null){
            if(( toDrag.position.x >-0.5  & toDrag.position.x < 1.5 )&& (toDrag.position.y > -3.5 & toDrag.position.y < -1.8 )){
                return 1;    
            }
            if(( toDrag.position.x > 2.1  & toDrag.position.x < 3.2 )&& (toDrag.position.y > -3.5 & toDrag.position.y < -1.8)){
                return 2;
            }   
            if(( toDrag.position.x > 4.5  & toDrag.position.x < 5.5)&& (toDrag.position.y > -3.5 & toDrag.position.y < -1.8)){
                return 3;
            }   
            if(( toDrag.position.x > 6.5  & toDrag.position.x < 7.6 )&& (toDrag.position.y > -3.5 & toDrag.position.y < -1.8)){
                return 4;
            }   
            if(( toDrag.position.x > 9.1  & toDrag.position.x < 10.4 )&& (toDrag.position.y > -3.5 & toDrag.position.y < -1.8)){
                return 5;
            }   
            getIndex(CurrentObject);
        }
    return 0;
    }
    void AddingNodeBetween(){
        for (int i = 0; i < 6; i++)
        {
            if(CheckForNearbyLinks()==i+1 && CheckForNearbyLinks()!= 0){
                ANB.SetActive(true);
                if(ANE.activeSelf == true || DNB.activeSelf == true || DNE.activeSelf == true){
                    ANE.SetActive(false); 
                    DNB.SetActive(false); 
                    DNE.SetActive(false); 
                }
                CurrentObject.tag  = CheckForNearbyLinks().ToString();
                ChainLinks[getIndex(CurrentObject)] = null;
                for (int z =ChainLinks.Count-1; z > i; z--)
                {
                    ChainLinks[z] = ChainLinks[z-1];
                    if(ChainLinks[z] == null){
                        for (int s = 1; s < ChainLinks.Count; s++)
                        {
                            if(ChainLinks.Contains(GameObject.FindGameObjectWithTag(""+s))){
                                Debug.Log("");
                            }
                            else{
                                ChainLinks[z] = GameObject.FindGameObjectWithTag(""+s);
                            }
                        }
                    }

                }
                for (int x = i+2; x < ChainLinks.Count; x++)
                {
                    ChainLinks[i+1]=CurrentObject;
                }

                for (int w = 0; w < ChainLinks.Count; w++)
                {
                    ChainLinks[w].tag = w.ToString();
                }
                for (int s = 0; s < ActiveChainLinks.Count; s++)
                {
                    ActiveChainLinks[s] = ChainLinks[s]; 
                }
                for (int j = i+2; j < ActiveChainLinks.Count; j++)
                {
                     ActiveChainLinks[j].transform.position = ActiveChainLinks[j].transform.position +   new Vector3(2.5f,0,0);
                }
            }   
        }
        if(CheckBoxSnip.isOn){
            CheckBoxSnip.isOn = false;
        }
        
    }
    int getIndex(GameObject CurrentObject)
    {
	for(int i=0;i<ChainLinks.Count;i++)
    {
    	if(ChainLinks[i] == CurrentObject){
            return i;
        } 
    }
    return -1;
    }
}

