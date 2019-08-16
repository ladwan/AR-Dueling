using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UMA.CharacterSystem;

public class CharacterColors : MonoBehaviour {

    public Button Apply, Cancel;
    public GameObject Ref;
    public DynamicCharacterAvatar Player;
    public Material SkinMat;


    public static bool isSkin, IsTorso, IsLegs;
    // Use this for initialization
    void Start () {
        Apply.gameObject.SetActive(false);
        Cancel.gameObject.SetActive(false);

    }
	
    public void ButtonPressed()
    {
        
        foreach(Button Buttons in GameObject.FindObjectsOfType<Button>())
        {
            if(Buttons.name == "ApplyButton" || Buttons.name == "CancelButton")
            {
                Buttons.gameObject.SetActive(false);
            }
        }

        Apply.gameObject.SetActive(true);
        Cancel.gameObject.SetActive(true);
       
        switch (gameObject.name)
        {
            case "SkinButton":

                Ref.GetComponent<MeshRenderer>().material.color = SkinMat.color;

                isSkin = true;
                IsTorso = false;
                IsLegs = false;
                Debug.Log("111");
                break;

            case "TorsoButton":
                isSkin = false;
                IsTorso = true;
                IsLegs = false;
                break;

            case "LegsButton":
                isSkin = false;
                IsTorso = false;
                IsLegs = true;
                break;
        }
        Ref.GetComponent<example>().StartPaint();
    }

    public void ApplyButton()
    {
        if (isSkin == true)
        {
            Player.SetColor("Skin", Ref.GetComponent<MeshRenderer>().material.color);
            Player.BuildCharacter();

        }
        Apply.gameObject.SetActive(false);
        Cancel.gameObject.SetActive(false);
        isSkin = false;
        IsTorso = false;
        IsLegs = false;
    }
    
    public void CancelButton()
    {
        Apply.gameObject.SetActive(false);
        Cancel.gameObject.SetActive(false);
        isSkin = false;
        IsTorso = false;
        IsLegs = false;
    }

}
