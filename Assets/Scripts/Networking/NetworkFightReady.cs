using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkFightReady : MonoBehaviour {
    public static bool fightReady;
    //[SerializeField] public static GameObject OfflineCharREF;

	// Use this for initialization
	void Start () {
        StartCoroutine(FightReady());

        if (GameObject.Find("NewPlayer") != null)
        {
      //      OfflineCharREF = GameObject.Find("NewPlayer");
            GameObject.Find("NewPlayer").SetActive(false);

        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator FightReady()
    {
        if (GameObject.FindGameObjectWithTag("Host") != null && GameObject.FindGameObjectWithTag("Client") != null)
        {
           
            fightReady = true;
            GameObject.FindGameObjectWithTag("Host").GetComponent<NetworkCombat>().ReferencesFound = true;
            Debug.Log("Fight Ready !");
        }

        yield return new WaitForSecondsRealtime(0.5f);

        if(fightReady == false)
        {
            StartCoroutine(FightReady());
        }
    }
}
