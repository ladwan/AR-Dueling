using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XpScript : MonoBehaviour {

    public Text CurrentLvlTEXT, NextLvlTEXT, CurrentXpTEXT, XpToLvlTEXT;
    public Slider XpBar;
    public GameObject HostREF;
    public float var;
    public static float XP;

/*
	// Use this for initialization
	void Start () {
        UpdateXpBoi();
         //HostREF = GameObject.FindGameObjectWithTag("Host");

    }

    public void UpdateXpBoi()
    {
        HostREF.GetComponent<PlayerScript>().CheckStats();


       /*
        PlayerPrefs.SetInt("CurrentLevel", 0);
        PlayerPrefs.SetInt("NextLvl", 0);
        PlayerPrefs.SetFloat("CurrentXP", 0);
        PlayerPrefs.SetFloat("XpToLvl", 50);
        PlayerPrefs.Save();
        

    
        
        //CurrentLvlTEXT.text = PlayerPrefs.GetFloat("CurrentLevel").ToString();
        //NextLvlTEXT.text = PlayerPrefs.GetFloat("NextLvl").ToString();
       // CurrentXpTEXT.text = PlayerPrefs.GetFloat("CurrentXP").ToString();
       // XpToLvlTEXT.text = PlayerPrefs.GetFloat("XpToLvl").ToString();
    }

    public void DoStuff()
    {
        // var = GameObject.Find("NewPlayer").GetComponent<PlayerScript>().currentXP / 100f;
        var = XP / 100f;

        //HostREF.GetComponent<PlayerScript>().UpdateXP();

        XpBar.value = var;

        PlayerPrefs.SetFloat("XP", XP);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update () {
		
	}*/
}
