using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    private int scene = -1;
    public void Setup(int currentScene = -1){
        scene = currentScene;
        gameObject.SetActive(true);
    }

    public void RestartButton(){
        if(scene > -1){
            SceneManager.LoadScene(scene);
        }else{
            SceneManager.LoadScene(2);
        }

    }

    public void ExitButton(){
        SceneManager.LoadScene(1);
    }
}
