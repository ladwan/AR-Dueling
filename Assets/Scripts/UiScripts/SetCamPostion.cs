using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCamPostion : MonoBehaviour {

    public Camera m_Camera;
    public Canvas PlayerCan;
	// Use this for initialization
	void Start () {
        
	}

    public void Update()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        PlayerCan.transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                m_Camera.transform.rotation * Vector3.up);
        
    }
	
	// Update is called once per frame

}
