using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthHide : MonoBehaviour {

    public GameObject PlayerRef;
    bool doOnce;
	// Use this for initialization
	void Start () {
        //Health.enabled = false;
        doOnce = false;
        if(PlayerScript.GameOver == true)
        {
            GameObject.FindGameObjectWithTag("Fighter").GetComponent<Animator>().SetTrigger("AfterFight");
        }

	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (GameObject.FindGameObjectWithTag("Fighter") != null)
        {
            while (PlayerRef == null)
            {
                PlayerRef = GameObject.FindGameObjectWithTag("Fighter");

            }

        }

        if (PlayerRef != null && doOnce == false)
        {
            doOnce = true;
           //PlayerScript.PlayerCanvas =  GameObject.FindGameObjectWithTag("PlayerCanvas");
            PlayerScript.PlayerCanvas.SetActive(false);
            
        }
        if(GameObject.Find("NetworkManager")!= null)
        {
            Destroy(GameObject.Find("NetworkManager"));
        }
    }
}
