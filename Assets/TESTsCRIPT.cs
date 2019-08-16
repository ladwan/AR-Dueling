using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class TESTsCRIPT : NetworkBehaviour {
    bool on;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && on == false)
        {
            on = true; 
            NetworkManager.singleton.StopClient();
            NetworkManager.singleton.StopHost();

            NetworkLobbyManager.singleton.StopClient();
            NetworkLobbyManager.singleton.StopServer();
            
           // DestroyImmediate(GameObject.FindObjectOfType<NetworkManager>());
            NetworkServer.DisconnectAll();
            //Destroy( GameObject.FindGameObjectWithTag("Host"));
            SceneManager.LoadScene(1);
            //NetworkManager.Shutdown();
            //NetworkManager.singleton.ServerChangeScene("Hel");
            // StartCoroutine(load());
        }
	}


    IEnumerator load()
    {
        yield return new WaitForSeconds(1);
        on = false;
        Debug.Log("1    fefes");

        SceneManager.LoadScene(1);
        Debug.Log("2    fes");
    }
}
