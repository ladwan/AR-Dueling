using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
    GameObject UI;

	// Use this for initialization
	void Start () {
        UI = GameObject.Find("Xp");
	}
	
	// Update is called once per frame
	void Update () {
        UI.GetComponent<Text>().text = XpScript.XP.ToString();
	}
}
