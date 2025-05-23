using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void StackFunc(){
        SceneManager.LoadScene("StackScene");
    }
    public void QueueFunc(){
        SceneManager.LoadScene("Queue");
    }
    public void Exit(){
        Application.Quit();
    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void LinkedList(){
        SceneManager.LoadScene("LinkedList");
    }
}
