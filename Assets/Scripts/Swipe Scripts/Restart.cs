using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    public int sceneToLoad;


    public void RestartGame()
    {

       // Destroy(PlayerScript.instance);
       // Destroy(CPU.instance);
        Time.timeScale = 1;
        
        SceneManager.LoadScene(sceneToLoad);
    }

}
