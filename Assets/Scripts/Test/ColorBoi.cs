using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
public class ColorBoi : MonoBehaviour {

    public GameObject Ref;
    public DynamicCharacterAvatar Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Player.SetColor("Skin", Ref.GetComponent<MeshRenderer>().material.color);
           // Player.BuildCharacter();
        }

	}
}
