using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveStuff : MonoBehaviour {


    public InputField PlayerName;
    string NameText;
	// Use this for initialization
	void Start () {
        PlayerName.characterLimit = 10;
        NameText = PlayerPrefs.GetString("SavedName");
        PlayerName.text = NameText;
	}
	
    public void SaveName()
    {
        NameText = PlayerName.text;
        PlayerPrefs.SetString("SavedName", NameText);
    }
}
