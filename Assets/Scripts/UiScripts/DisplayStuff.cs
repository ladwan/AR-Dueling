using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStuff : MonoBehaviour {

    // Use this for initialization
    string DisplayedName;
    public void Start()
    {
        Text Name = GameObject.Find("DisplayName").GetComponent<Text>();
        DisplayedName = PlayerPrefs.GetString("SavedName");
        Name.text = "Name: " + DisplayedName ;
    }
}
