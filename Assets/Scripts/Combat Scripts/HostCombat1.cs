using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostCombat1 : MonoBehaviour {

    public GameObject[] Players;
    public GameObject HostRef, ClientRef;
    public float HostHealth = 100f;
    public float HostDamage = 10f;
    public static bool HostTurn;
    public string HostInput;
    public Image HostHealthBar;
    public Animator HostAnim;
    public Slider HostSlider, HostSliderUP;
    public Canvas HostAttCanvas, HostDefCanvas;
    bool canvasOff, canvasOff2;
    public bool isFighting;

    public int BlockableREF;

    // Use this for initialization
    void Start() {
        //GameObject.Find("HealthO").GetComponent<Image>();
        
        
        var def = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in def)
            if (child.name == "DefenseUI")
            {
                HostDefCanvas = child.GetComponent<Canvas>();
            }

        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
            if (child.name == "HealthO")
            {
                HostHealthBar = child.GetComponent<Image>();
            }


        var att = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in att)
            if (child.name == "AttackUI")
            {
                HostAttCanvas = child.GetComponent<Canvas>();
            }

        HostAnim = gameObject.GetComponent<Animator>();


        HostSliderUP = HostDefCanvas.transform.GetChild(0).GetComponent<Slider>();
        HostSlider = HostDefCanvas.transform.GetChild(1).GetComponent<Slider>();

        HostSlider.onValueChanged.AddListener(delegate { Guard(); });
        HostSlider.onValueChanged.AddListener(delegate { HostSideDelay(); });

        HostSliderUP.onValueChanged.AddListener(delegate { Guard(); });
        HostSliderUP.onValueChanged.AddListener(delegate { HostUpDelay(); });


        HostDefCanvas.enabled = false;
        HostAttCanvas.enabled = false;

    }

    public void Guard()
    {
        if (HostSliderUP.value == 1)
        {
            HostInput = "Up";
            BlockableREF = GameObject.Find("Directional Light").GetComponent<CombatManager>().Blockable;
            Debug.Log("Guarding Up !");

        }

        if (HostSlider.value == 1)
        {
            HostInput = "Left";
            BlockableREF = GameObject.Find("Directional Light").GetComponent<CombatManager>().Blockable;

            Debug.Log("Guarding Left !");
        }
        else if (HostSlider.value == 0)
        {
            HostInput = "Right";

            BlockableREF = GameObject.Find("Directional Light").GetComponent<CombatManager>().Blockable;
            Debug.Log("Guarding Right !");
        }
    }
    
    public void HostUpDelay()
    {
        HostSlider.interactable = false;
        Invoke("HostUpDelay2", 2f);
    }

    void HostUpDelay2()
    {
        HostSliderUP.value = 0;
        HostSlider.interactable = true;


    }
    public void HostSideDelay()
    {
        HostSliderUP.interactable = false;
        Invoke("HostSideDelay2", 2f);
    }

    void HostSideDelay2()
    {
        HostSlider.value = 0.5f;
        HostSliderUP.interactable = true;
        
        
    }

    
    // Update is called once per frame
    void Update () {

        if (Camera.main.GetComponent<SetCharPosition>().fightReady == true)
        {
            if (GameObject.Find("Directional Light").GetComponent<CombatManager>().TurnOver >= 3)
            {
                HostTurn = true;
                HostDefCanvas.enabled = false;
                HostAttCanvas.enabled = true;

            }
            else

            if (HostDefCanvas && HostAttCanvas != null)
            {
                HostTurn = false;
                HostDefCanvas.enabled = true;
                HostAttCanvas.enabled = false;

            }
        }


    }

}
