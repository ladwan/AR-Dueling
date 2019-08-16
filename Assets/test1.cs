using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test1 : MonoBehaviour {
    bool on;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && on == false)
            {
            on = true;
            
            SceneManager.LoadScene(1);
            }
        }
}
