using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class HostCombat : NetworkBehaviour {

    public GameObject[] Players;
    public GameObject HostRef, ClientRef;
    public float HostHealth = 1000000000000000f;
    public float HostDamage = 10f;
    public static bool HostTurn;
    public string HostInput;
    public Image HostHealthBar;
    public Animator HostAnim;
    public Slider HostSlider, HostSliderUP;
    public Canvas HostAttCanvas, HostDefCanvas;
    bool canvasOff, canvasOff2;
    public bool isFighting;
    public Button right, left, up;
    // Use this for initialization
    void Start() {
        //GameObject.Find("HealthO").GetComponent<Image>();

        //ClientCombat.ClientTurn = true;
        //HostTurn = false;

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

            var leftButt = gameObject.GetComponentsInChildren<Transform>();
            foreach (var child in leftButt)
                if (child.name == "Left Att Button")
                {
                    left = child.GetComponent<Button>();
                }

            var rightButt = gameObject.GetComponentsInChildren<Transform>();
            foreach (var child in rightButt)
                if (child.name == "Right Att Button ")
                {
                    right = child.GetComponent<Button>();
                }

            var upButt = gameObject.GetComponentsInChildren<Transform>();
            foreach (var child in upButt)
                if (child.name == "Up Att Button ")
                {
                    up = child.GetComponent<Button>();


                }
        HostAnim = gameObject.GetComponent<Animator>();

        if (isLocalPlayer)
        {
            right.onClick.AddListener(() => RightAttack());

            left.onClick.AddListener(() => LeftAttack());

            up.onClick.AddListener(() => UpAttack());
       


            HostSliderUP = HostDefCanvas.transform.GetChild(0).GetComponent<Slider>();
            HostSlider = HostDefCanvas.transform.GetChild(1).GetComponent<Slider>();

            HostSlider.onValueChanged.AddListener(delegate { Guard(); });
            HostSlider.onValueChanged.AddListener(delegate { HostSideDelay(); });

            HostSliderUP.onValueChanged.AddListener(delegate { Guard(); });
            HostSliderUP.onValueChanged.AddListener(delegate { HostUpDelay(); });


            HostDefCanvas.enabled = false;
        }



    }

    public void LeftAttack()
    {
        HostInput = "Left";
        Debug.Log("Host Attacking Left");
    }
    public void RightAttack()
    {
        HostInput = "Right";
        Debug.Log("Host Attacking Right");

    }
    public void UpAttack()
    {
        HostInput = "Up";
        Debug.Log("Host Attacking Up");

    }

    public void Guard()
    {
        if (HostSliderUP.value == 1)
        {
            HostInput = "Up";

            Debug.Log("Guarding Up !");

        }

        if (HostSlider.value == 1)
        {
            HostInput = "Left";
            Debug.Log("Guarding Left !");
        }
        else if (HostSlider.value == 0)
        {
            HostInput = "Right";
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

        if (NetworkFightReady.fightReady == true)
        {
            if (GameObject.FindGameObjectWithTag("Host").GetComponent<NetworkCombat>().TurnOver >= 3)
            {
               //HostTurn = true;
                //ClientCombat.ClientTurn = false;
                if (isLocalPlayer)
                {
                    //Debug.Log("Host On The Attack");
                    HostDefCanvas.enabled = false;
                    HostAttCanvas.enabled = true;
                }
            

            }
            else
            {
               // HostTurn = false;
                if (isLocalPlayer)
                {
                    if (HostDefCanvas && HostAttCanvas != null)
                    {
                        //Debug.Log("Host On Defense");

                        //HostTurn = false;
                        HostDefCanvas.enabled = true;
                        HostAttCanvas.enabled = false;

                    }
                }

            }

        }


    }
}
