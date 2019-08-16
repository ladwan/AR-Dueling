using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject[] things;
    public GameObject FightUiREf;
    public void Start()
    {
      things = Resources.FindObjectsOfTypeAll<GameObject>();
        if(things.Length > 0)
        {
            foreach (var thing in things)
            {
                if (thing.name == "NewPlayer")
                {
                    thing.SetActive(true);

                }
                if (thing.name == "FightUI Holder")
                {
                    FightUiREf = thing;
                    //thing.SetActive(false);

                }

                //Debug.Log(thing);
            }
        }
        Time.timeScale = 1f;
        FightUiREf.SetActive(false);
    }
    public void PlayGame()
    {
        FightUiREf.SetActive(true);
        PlayerScript.GameOver = false;
        //Destroy(PlayerScript.instance);
        //Destroy(CPU.instance);
        GameObject.FindGameObjectWithTag("Fighter").GetComponent<PlayerScript>().FightUI.SetActive(true);
        PlayerScript.PlayerCanvas.SetActive(true);
        SceneManager.LoadScene("HelloAR");  
    }
    public void PlayOnline()
    {
        FightUiREf.SetActive(true);
        PlayerScript.GameOver = false;
        //Destroy(PlayerScript.instance);
        //Destroy(CPU.instance);
        GameObject.FindGameObjectWithTag("Fighter").GetComponent<PlayerScript>().FightUI.SetActive(true);
        PlayerScript.PlayerCanvas.SetActive(true);
        SceneManager.LoadScene(4);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Update()
    {

        if (GameObject.Find("Cpu_Enemy") != null)
        {
            Destroy(GameObject.Find("Cpu_Enemy"));
        }
    }
}
