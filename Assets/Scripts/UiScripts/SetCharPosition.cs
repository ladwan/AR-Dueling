using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetCharPosition : MonoBehaviour {
    GameObject player;
    public GameObject CPU ;
    bool spawnOn = false;
    bool thing = false;
    bool Off = false;
    public bool fightReady = false;
    public GameObject FightUI;
    public Canvas HealthDisplay;
    public Camera MainCam;
    GameObject CanvasStuff;
    //public GameObject PlayerCanvasREF;
    
 
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("PlayerChar01");

        player.transform.position = new Vector3(0, 1000,0);
        CPU.transform.position = new Vector3(0, 1000, 0);
        //CPU.SetActive(false);
        //player.SetActive(false);
        FightUI.SetActive(false);
        
//      CPU = GameObject.Find("CPU");
        //Instantiate(CPU, GameObject.Find("Spawn02").transform.position, GameObject.Find("Spawn02").transform.rotation);
        Debug.Log("Cpu has been set to active");
    }
	
	// Update is called once per frame
	void Update ()
    {
    
         if (spawnOn == true && Input.touchCount > 0  || spawnOn == true && Input.GetKey(KeyCode.Space))
        {

            player.SetActive(true);
            player.transform.rotation = GameObject.Find("Spawn02").transform.rotation;
            player.transform.position = GameObject.Find("Spawn02").transform.position;
            
            StartCoroutine(WaitAgain());

        }

         if(thing == true && Off == false)
        {
            //Instantiate(CPU, GameObject.Find("Spawn02").transform.position, GameObject.Find("Spawn02").transform.rotation);
            CPU.SetActive(true);
            CPU.transform.rotation = GameObject.Find("Spawn01").transform.rotation;
            CPU.transform.position = GameObject.Find("Spawn01").transform.position;
            Debug.Log("Enemy has been spawned");
            Off = true;
            FightUI.SetActive(true);
            HealthDisplay.enabled = true;
            HealthDisplay.worldCamera = MainCam;
            CanvasStuff = GameObject.Find("PlayerCanvas");
            //CanvasStuff = PlayerCanvasREF;

            if (CanvasStuff != null)
            {
                CanvasStuff.GetComponent<Canvas>().enabled = true;
                fightReady = true;
            }
            else
                Debug.Log("CanvasStuff Was Equal to NULL");
        }

        if (Input.GetKey(KeyCode.Space) && spawnOn == false || Input.touchCount > 0 && spawnOn == false)
        {
            Debug.Log("Input Registered");
            StartCoroutine(Wait());
        }

        else

            return;
            
 

    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1);
        spawnOn = true;
        Debug.Log("You have Waited");
        StopCoroutine(Wait());
       
    }
    IEnumerator WaitAgain()
    {
        yield return new WaitForSecondsRealtime(1);
        thing = true;
        StopCoroutine(WaitAgain());

    }

}

