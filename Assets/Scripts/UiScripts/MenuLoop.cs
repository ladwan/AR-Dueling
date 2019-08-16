using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoop : MonoBehaviour {

    public GameObject PlayerRef;
	// Use this for initialization
	void Start ()
    {
        if (GameObject.FindGameObjectWithTag("Fighter") != null)
            PlayerRef = GameObject.FindGameObjectWithTag("Fighter");
       // PlayerRef.transform.position = new Vector3(-.2f, 0f, 0f);

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(GameObject.FindGameObjectWithTag("Fighter") != null)
        PlayerRef = GameObject.FindGameObjectWithTag("Fighter");

        if (PlayerRef != null)
        {
            PlayerRef.transform.position = new Vector3(-.2f, 0f, 0f);
            PlayerRef.transform.rotation = new Quaternion(0f,180f,0f,0f);
        }

 
    }
}
