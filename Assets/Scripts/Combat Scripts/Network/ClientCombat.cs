using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ClientCombat : NetworkBehaviour {

    public GameObject[] Players;
    public GameObject HostRef, ClientRef;
    public float ClientHealth = 10f;
    public float ClientDamage = 10f;
    public static bool ClientTurn;
    public string ClientInput, SyncedClientInput;
    public Image ClientHealthBar;
    public Animator ClientAnim;
    public Slider ClientSlider, ClientSliderUp;
    public Canvas ClientAttCanvas, ClientDefCanvas;
    bool canvasOff, canvasOff2;
    public bool isFighting;
    public Button left, right, up;

    // Use this for initialization

    void Start() {

        //GameObject.Find("HealthO").GetComponent<Image>();
        Debug.Log(netId +"NetId");
       // ClientTurn = true;
        HostCombat.HostTurn = false;

        var def = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in def)
            if (child.name == "DefenseUI")
            {
                ClientDefCanvas = child.GetComponent<Canvas>();
            }

        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
            if (child.name == "HealthO")
            {
                ClientHealthBar = child.GetComponent<Image>();
            }


        var att = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in att)
            if (child.name == "AttackUI")
            {
                ClientAttCanvas = child.GetComponent<Canvas>();
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
                Debug.Log(up.name);

            }

        ClientAnim = gameObject.GetComponent<Animator>();


        if (isLocalPlayer)
        {

            right.onClick.AddListener(() => RightAttack());

            left.onClick.AddListener(() => LeftAttack());

            up.onClick.AddListener(() => UpAttack());

            ClientSliderUp = ClientDefCanvas.transform.GetChild(0).GetComponent<Slider>();
            ClientSlider = ClientDefCanvas.transform.GetChild(1).GetComponent<Slider>();

            ClientSlider.onValueChanged.AddListener(delegate { Guard(); });
            ClientSlider.onValueChanged.AddListener(delegate { HostSideDelay(); });

            ClientSliderUp.onValueChanged.AddListener(delegate { Guard(); });
            ClientSliderUp.onValueChanged.AddListener(delegate { HostUpDelay(); });


            ClientDefCanvas.enabled = false;
        }



    }

    public void LeftAttack()
    {
        ClientInput = "Left";
        Debug.Log("Client Attacking Left");

        if (!isServer)
        {
            //CmdSyncInput(ClientInput);

        }
    }
    public void RightAttack()
    {
        ClientInput = "Right";
        Debug.Log("Client Attacking Right");
        if (!isServer)
        {
           // CmdSyncInput(ClientInput);

        }

    }
    public void UpAttack()
    {
        ClientInput = "Up";
        Debug.Log(" Client Attacking Up");
        if (!isServer)
        {
           // CmdSyncInput(ClientInput);

        }

    }

    public void Guard()
    {
        if (ClientSliderUp.value == 1)
        {
            ClientInput = "Up";

            Debug.Log("Guarding Up !");
            if (!isServer)
            {
               // CmdSyncInput(ClientInput);

            }

        }

        if (ClientSlider.value == 1)
        {
            ClientInput = "Left";
            Debug.Log("Guarding Left !");
            if (!isServer)
            {
               // CmdSyncInput(ClientInput);

            }
        }
        else if (ClientSlider.value == 0)
        {
            ClientInput = "Right";
            Debug.Log("Guarding Right !");
            if (!isServer)
            {
              //  CmdSyncInput(ClientInput);

            }
        }
    }

    public void HostUpDelay()
    {
        ClientSlider.interactable = false;
        Invoke("HostUpDelay2", 2f);
    }

    void HostUpDelay2()
    {
        ClientSliderUp.value = 0;
        ClientSlider.interactable = true;


    }
    public void HostSideDelay()
    {
        ClientSliderUp.interactable = false;
        Invoke("HostSideDelay2", 2f);
    }

    void HostSideDelay2()
    {
        ClientSlider.value = 0.5f;
        ClientSliderUp.interactable = true;


    }

    [Command]
    public void CmdSyncInput(string input)
    {
        RpcClientInput(input);
        Debug.Log(SyncedClientInput);

    }

    [ClientRpc]
    public void RpcClientInput(string rpcInput)
    {
        SyncedClientInput = rpcInput;

    }


    void Update () {

        if (NetworkFightReady.fightReady == true)
        {
            if (GameObject.FindGameObjectWithTag("Host").GetComponent<NetworkCombat>().TurnOver <= 2)
            {
                //ClientTurn = true;
               // HostCombat.HostTurn = false;
                //ClientDefCanvas.enabled = false;
                if (isLocalPlayer)
                {
                    //Debug.Log("Client On The Attack");

                   // ClientAttCanvas.enabled = true;
                  // ClientDefCanvas.enabled = false;
                }

            }
            else
            {
                //ClientTurn = false;
                if (isLocalPlayer)
                {
                    if (ClientDefCanvas && ClientAttCanvas != null)
                    {
                        //Debug.Log("Client On Defense");

                        //ClientTurn = false;
                        //ClientDefCanvas.enabled = true;
                       // ClientAttCanvas.enabled = false;

                    }
                }
            }


        }


    }
}
