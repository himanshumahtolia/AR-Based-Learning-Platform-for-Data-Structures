using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueBehaviour : MonoBehaviour
{
     public GameObject[] Shoes;
     public GameObject Display;
     public GameObject Enqueue;
     public GameObject Dequeue;
     bool removeBtnClicked;
     public GameObject Arrow;
    int i =0;
    int f = 0;
    int n =0;
    int Count = 0;
    int CountShoes = 0;
    // Start is called before the first frame update
    public void Add(){
        Shoes[i].SetActive(true);
        CountShoes++;
        if(removeBtnClicked == true){
            Shoes[i].transform.localPosition = Shoes[i].transform.localPosition - new Vector3(0,0,Count*40);    
        }
        if(i!=0){
            Arrow.transform.localPosition = Arrow.transform.localPosition + new Vector3(0,0,38);
        }
        else if(i==0){
            Arrow.SetActive(true);
        }
         if(Dequeue.activeSelf == true){
            Dequeue.SetActive(false);
        }
        Display.SetActive(false);
        Enqueue.SetActive(true);
        i++;
        
        // Shoes[i].transform.localPosition.z = 

    }
    public void RemoveBtn(){
        removeBtnClicked = true;
        Count++;
    }
    public void Remove(){
        // Debug.Log("I value"+i);
        Shoes[f].SetActive(false);
        CountShoes--;
         if(Enqueue.activeSelf == true){
            Enqueue.SetActive(false);
        }
        Display.SetActive(false);
        Dequeue.SetActive(true);
        f++;
        Invoke("Shift" , 0.7f);
}
    void Shift(){
        if(CountShoes == 1 && Shoes[f].transform.localPosition.z == -75){
            Arrow.transform.localPosition = Arrow.transform.localPosition + new Vector3(0,0,(f*38));
            return;
        }
        else{
        for (int a = 0; a < 7; a++)
        {
            if(GameObject.FindGameObjectWithTag("Shoe"+(a)) == true){
                Debug.Log("Shoeno"+a);
                Shoes[a].transform.localPosition = Shoes[a].transform.localPosition - new Vector3(0,0,40);
            }
        }
    
    }
        Arrow.transform.localPosition = Arrow.transform.localPosition - new Vector3(0,0,38);
}    
    void Start()
    {
         for (int i = 0; i < Shoes.Length; i++)
        {
            Shoes[i].SetActive(false);
            Arrow.SetActive(false);
            
        }
    }
}
    

