using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkPlayer : NetworkBehaviour {
    public static bool HostIsAttacking;
    public int TurnOverFight = 0;
    GameObject LocalPlayerFightUiCanvas;
    GameObject[] Fighters;
    public string ClientInputREF, HostInputREF;
    bool once;
    public int ClientBodyREF, ClientHandsREF, ClientFeetREF;
    public int HostBodyREF, HostHandsREF, HostFeetREF;
    public GameObject Customization;

    public bool DoOnce, ready, DoOnce1, ready1, ready2, ready3;


    // Use this for initialization
    void Start ()
    {
        //Debug.Log(PlayerPrefs.GetInt("BodyColor") );
        Customization = GameObject.Find("OtherCustomization");



        for (int i = 0; i < 17; i++)
        {
            GetComponent<NetworkAnimator>().SetParameterAutoSend(i, true);
        }

        var NetworkedPlayer = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in NetworkedPlayer)
            if (child.name == "FightUI_Holder")
            {
                LocalPlayerFightUiCanvas = child.gameObject;
                LocalPlayerFightUiCanvas.SetActive(false);
                Debug.Log("Pass");
            }

        if (isLocalPlayer)
        {
            LocalPlayerFightUiCanvas.SetActive(true);
            Debug.Log("Pass02");

           Fighters =  GameObject.FindGameObjectsWithTag("Fighter");

            if (Fighters.Length == 1)
            {
                gameObject.tag = "Host";
                
                gameObject.AddComponent<HostCombat>();
                gameObject.AddComponent<NetworkCombat>();

                Debug.Log("Host Found !");

              
            }
            else
            {

                gameObject.tag = "Client";
                gameObject.AddComponent<ClientCombat>();
                Debug.Log("Client Found !");
               // gameObject.AddComponent<NetworkCombat>();


                GameObject.FindGameObjectWithTag("Fighter").tag = "Host";
                GameObject.FindGameObjectWithTag("Host").AddComponent<NetworkCombat>();
                GameObject.FindGameObjectWithTag("Host").AddComponent<HostCombat>();


            }

            if (isServer)
            {
                //Debug.Log("Error");
                HostBodyREF = PlayerPrefs.GetInt("BodyColor");
                HostHandsREF = PlayerPrefs.GetInt("HandColor");
                HostFeetREF = PlayerPrefs.GetInt("FeetColor");

                //RpcHostSendColors(HostBodyREF, HostHandsREF , HostFeetREF);
            }
            else if (!isServer)
            {
                ClientBodyREF = PlayerPrefs.GetInt("BodyColor");
                ClientHandsREF = PlayerPrefs.GetInt("HandColor");
                ClientFeetREF = PlayerPrefs.GetInt("FeetColor");
                CmdSendColors(ClientBodyREF, ClientHandsREF, ClientFeetREF);
            }
            
        }



    }
    public void ClientApplyColors()
    {
        if (!isServer)
        {
            //ready2 = true;
            //Debug.Log("3");
            Debug.Log(HostBodyREF + "Body");
            Debug.Log(HostHandsREF + "Hands");
            Debug.Log(HostFeetREF + "Feet");

            GameObject.FindGameObjectWithTag("Host").transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = Customization.GetComponent<OtherColor>().bodyMat;
            GameObject.FindGameObjectWithTag("Host").transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = Customization.GetComponent<OtherColor>().handMat;
            GameObject.FindGameObjectWithTag("Host").transform.GetChild(6).GetComponent<SkinnedMeshRenderer>().material = Customization.GetComponent<OtherColor>().feetMat;

            Customization.GetComponent<OtherColor>().Body = HostBodyREF;
            //Debug.Log(HostBodyREF + "Bodybody");
            Customization.GetComponent<OtherColor>().Hands = HostHandsREF;
            Customization.GetComponent<OtherColor>().Feet = HostFeetREF;

            Customization.GetComponent<OtherColor>().ChangeBodyColor();
            Customization.GetComponent<OtherColor>().ChangeHandColor();
            Customization.GetComponent<OtherColor>().ChangeFeetColor();

        }


    }

    public void HostApplyColors()
    {
        if (isServer)
        {
            Debug.Log("3");
            Debug.Log(ClientBodyREF + "ClientBody");
            Debug.Log(ClientHandsREF + "ClientHands");
            Debug.Log(ClientFeetREF + "ClientFeet");

            GameObject.FindGameObjectWithTag("Client").transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = Customization.GetComponent<OtherColor>().bodyMat;
            GameObject.FindGameObjectWithTag("Client").transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = Customization.GetComponent<OtherColor>().handMat;
            GameObject.FindGameObjectWithTag("Client").transform.GetChild(6).GetComponent<SkinnedMeshRenderer>().material = Customization.GetComponent<OtherColor>().feetMat;

            Customization.GetComponent<OtherColor>().Body = ClientBodyREF;
            Debug.Log(ClientBodyREF + "ClientBodybody");
            Customization.GetComponent<OtherColor>().Hands = ClientHandsREF;
            Customization.GetComponent<OtherColor>().Feet = ClientFeetREF;

            Customization.GetComponent<OtherColor>().ChangeBodyColor();
            Customization.GetComponent<OtherColor>().ChangeHandColor();
            Customization.GetComponent<OtherColor>().ChangeFeetColor();

        }


    }


    [ClientRpc]
    public void RpcHostSendColors(int Hostbody, int Hosthands, int Hostfeet)
    {
        if (!isServer)
        {
            Debug.Log(Hostbody + "Hosttest");
            HostBodyREF = Hostbody;
            HostHandsREF = Hosthands;
            HostFeetREF = Hostfeet;
        }


        if(!isServer && ready1 == false && GameObject.FindGameObjectWithTag("Host") != null)
        {
            ready1 = true;
            ClientApplyColors();
        }


    }
    

    [Command]
    public void CmdSendColors(int body, int hands, int feet)
    {
        //Debug.Log(input);
        RpcSendColors(body,hands,feet);
    }

    [ClientRpc]
    public void RpcSendColors(int RPCbody, int RPChands, int RPCfeet)
    {
        if (isServer)
        {
            ClientBodyREF = RPCbody;
            ClientHandsREF = RPChands;
            ClientFeetREF = RPCfeet;
            ready = true;
            Debug.Log(ClientBodyREF + "Client Test");
        }

        ready3 = true;

    }

    [Command]
    public void CmdDude(string input)
    {
        //Debug.Log(input);
        RpcClientInput(input);
    }

    [ClientRpc]
    public void RpcClientInput(string rpcInput)
    {
        ClientInputREF = rpcInput;

    }

   /* [Command]
    public void CmdHostSync(string input, int turnOver)
    {
  
        RpcUpdateHost(input, turnOver);
    } */

    [ClientRpc]
    public void RpcUpdateHost(string hostInput, int rpcTurnOver)
    {
        HostInputREF = hostInput;
        TurnOverFight = rpcTurnOver;
    }


    public override void PreStartClient()
    {
        for (int i = 0; i < 17; i++)
        {
            GetComponent<NetworkAnimator>().SetParameterAutoSend(i, true);
        }
    }


    // Update is called once per frame



    void Update ()
    {
        
        //if (!isServer)
          //  HostBodyREF = 5;


        Debug.Log(HostBodyREF);

        if (isServer && GameObject.FindGameObjectWithTag("Client") != null && DoOnce1 == false )
        {
            DoOnce1 = true;
            RpcHostSendColors(HostBodyREF, HostHandsREF, HostFeetREF);
        }
        

        if (isServer && GameObject.FindGameObjectWithTag("Client") != null && DoOnce == false && ready == true)
        {
            DoOnce = true;
            HostApplyColors();
        }

        if (TurnOverFight < 3)
        {
            HostIsAttacking = false;
        }
        else
        {
            HostIsAttacking = true;

        }

        if (!isServer)
        {
            if (GameObject.FindGameObjectWithTag("Client") != null)
            {
                ClientInputREF = GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientInput;
                if (once == false)
                {
                    once = true;
                    StartCoroutine(UpdateClientInput());
                }
                //Debug.Log(ClientInputREF);
            }

        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Host") != null)
            {
                TurnOverFight = GameObject.FindGameObjectWithTag("Host").GetComponent<NetworkCombat>().TurnOver;
                HostInputREF = GameObject.FindGameObjectWithTag("Host").GetComponent<HostCombat>().HostInput;
                if (once == false)
                {
                    once = true;
                    StartCoroutine(UpdateHostInput());
                }
                //Debug.Log(ClientInputREF);
            }
        }

    }
    IEnumerator UpdateClientInput()
    {
        CmdDude(ClientInputREF);
       // ClientApplyColors();
        Debug.Log(TurnOverFight + "TurnOver");

            if (isLocalPlayer && HostIsAttacking == false)
            {

                 if (GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientDefCanvas != null && GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientAttCanvas != null)
                 {
                GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientDefCanvas.enabled = false;
                GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientAttCanvas.enabled = true;
                 }
                //Debug.Log("Host On The Attack");

            }

           else
            
            if (GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientDefCanvas != null && GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientAttCanvas != null)
            {
            GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientDefCanvas.enabled = true;
            GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientAttCanvas.enabled = false;
            }

        
        
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(UpdateClientInput());
    }

    IEnumerator UpdateHostInput()
    {
        RpcUpdateHost(HostInputREF, TurnOverFight);
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(UpdateHostInput());
    }
}
