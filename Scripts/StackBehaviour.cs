using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StackBehaviour : MonoBehaviour
{
     public GameObject[] Shoes;
     public GameObject[] Shelf1;
     public GameObject[] Shelf2;
    public GameObject Arrow;
    float horizontalAxis;
    float verticalAxis;
    int i= 0;
    public GameObject push;
    public GameObject pop;
    public GameObject peek;
     public void Push(){
        Shoes[i].SetActive(true);
        Shelf1[i].SetActive(true);
        Shelf2[i].SetActive(true);
        // Arrow.transform.position = new Vector3(193,5,35+((i+1)*15));
        if(i!=0){
        Debug.Log("Arrow positions = "+Arrow.transform.localPosition);
        Arrow.transform.localPosition = Arrow.transform.localPosition + new Vector3(0,0,15);
        }
        else if(i==0){
            Arrow.SetActive(true);
        }
        // Arrow.transform.SetPositionAndRotation(new Vector3(193,5,50), Quaternion.identity);
        
        
        // Debug.Log("Arrow positions after transform = "+Arrow.transform.position);
        if(pop.activeSelf == true){
            pop.SetActive(false);
        }
        peek.SetActive(false);
        push.SetActive(true);
        i++;
     }
     public void Pop(){
        Debug.Log("On pop value"+i);
        Shoes[i-1].SetActive(false);
        Shelf1[i-1].SetActive(false);
        Shelf2[i-1].SetActive(false);
        if(i!=1){
        Arrow.transform.localPosition = Arrow.transform.localPosition - new Vector3(0,0,15);
        }
        else if(i==1){
            Arrow.SetActive(false);
        }
        i--;
        if(push.activeSelf == true){
            push.SetActive(false);
        }
        peek.SetActive(false);
        pop.SetActive(true);
     }
    private void Start() {
        for (int i = 0; i < Shoes.Length; i++)
        {
            Shoes[i].SetActive(false);
            Shelf1[i].SetActive(false);
            Shelf2[i].SetActive(false);
            Arrow.SetActive(false);
        }
    }
   

}
