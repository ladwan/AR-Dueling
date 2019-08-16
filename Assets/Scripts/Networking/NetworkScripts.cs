using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkScripts : MonoBehaviour {
    GameObject PlayerRef;
	// Use this for initialization
	void Start () {
        PlayerRef = GameObject.FindGameObjectWithTag("Host");
        PlayerRef.AddComponent<NetworkIdentity>();

        this.GetComponent<NetworkManager>().playerPrefab = PlayerRef;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
