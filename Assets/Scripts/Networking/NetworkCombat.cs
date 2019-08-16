using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkCombat : NetworkBehaviour {


    public Text Counter;

    public GameObject[] Players;
    public GameObject HostPlayer, ClientPlayer, Drawer, Detector;
    [SyncVar] public int Countdown = 3;
    [SyncVar] public int randomInt, TurnOver = 0;
    public bool MatchWon, FightReady, MatchOver = false, StopWhile = false;
    [SyncVar]public string HostInputREF, ClientInputREF;
    [SyncVar] public float HostHealthREF, HostDamageREF, ClientHealthREF, ClientDamageREF;
    public Image HostHealthBarREF, ClientHealthBarREF;
    public Text  FightNotes;
    public bool isAlive = true, Off,ReferencesFound, HostAttacking;
    public Animator HostAnimREF, ClientAnimREF;
    bool once, GameOver;



    void Start () {
        StartCoroutine(FindPlayers());


        if (ReferencesFound == true)
        {
           

           
            HostHealthREF = HostPlayer.GetComponent<HostCombat>().HostHealth;
            HostDamageREF = HostPlayer.GetComponent<HostCombat>().HostDamage;
          


            ClientHealthREF = ClientPlayer.GetComponent<ClientCombat>().ClientHealth;
            ClientDamageREF = ClientPlayer.GetComponent<ClientCombat>().ClientDamage;
            RpcDude();
        }



    }


    void AnimDelay()
    {
        ClientAnimREF.SetBool("UpBool", false);
        ClientAnimREF.SetBool("LeftBool", false);
        ClientAnimREF.SetBool("RightBool", false);

        HostAnimREF.SetBool("UpBool", false);
        HostAnimREF.SetBool("LeftBool", false);
        HostAnimREF.SetBool("RightBool", false);


        if (HostInputREF == ClientInputREF)
        {
            if(HostInputREF == null && ClientInputREF == null)
            {
                TurnOver++;
                return;
            }

            if(HostAttacking ==  true )
            {
                Debug.Log("Blocked!");
                TurnOver++;
                FightNotes.text = "Blocked !";
                HostAnimREF.SetTrigger("ATTBlockedTrigger");
                ClientAnimREF.SetTrigger("DEFBlockTrigger");
            }
            else
            {
                Debug.Log(HostInputREF);
                Debug.Log(ClientInputREF);
             
                TurnOver++;
                FightNotes.text = "Blocked !";
                HostAnimREF.SetTrigger("DEFBlockTrigger");
                ClientAnimREF.SetTrigger("ATTBlockedTrigger");
            }


        }

        if(HostInputREF != ClientInputREF)
        {

            if (HostAttacking == true && HostInputREF == "void" || HostAttacking == true && HostInputREF == "" || HostAttacking == true && HostInputREF == null)
            {
               
                TurnOver++;
                ClientAnimREF.SetBool("DrawBool", true);
                Invoke("UncheckBools", 1f);
                
            }

            if (HostAttacking == true && HostInputREF != "void" && HostInputREF != null && HostInputREF != "")
            {
                ClientAnimREF.SetTrigger("DEFBrokenTrigger");
                ClientHealthREF -= HostDamageREF;
                ClientHealthBarREF.fillAmount = ClientHealthREF / 100f;
                FightNotes.text = "A Clean Hit !";
                //return;
            }

            if (HostAttacking == false && ClientInputREF == "void" || HostAttacking == false && ClientInputREF == "" || HostAttacking == false && ClientInputREF == null)
            {
                TurnOver++;
                Debug.Log("Blocked!");
               
                ClientAnimREF.SetTrigger("DEFDrawTrigger");
                if(HostAttacking == false)
                {
                    HostAnimREF.SetBool("DrawBool", true);

                }
                Invoke("UncheckBools", 1f);
                

            }

            if (HostAttacking == false && ClientInputREF != "void" && ClientInputREF != null && ClientInputREF != "")
            {
           

                Debug.Log(ClientInputREF);
                HostAnimREF.SetTrigger("DEFBrokenTrigger");
                HostHealthREF -= ClientDamageREF;
                HostHealthBarREF.fillAmount = HostHealthREF / 100f;
                FightNotes.text = "A Clean Hit !";

                //return;
            }

        }

    }

    void UncheckBools()
    {
        HostAnimREF.SetBool("DrawBool", false);
        ClientAnimREF.SetBool("DrawBool", false);

    }

    void CombatCheck()
    {

        
        if (HostAttacking == false)
        {

            switch (ClientInputREF)
            {
                case "Right":
                    // GameObject.FindGameObjectWithTag("Host").GetComponent<NetworkAnimator>().SetTrigger("RightTrigger");
                    ClientAnimREF.SetBool("UpBool", false);
                    ClientAnimREF.SetBool("LeftBool", false);
                    ClientAnimREF.SetBool("RightBool", true);
                    ClientAnimREF.SetTrigger("RightTrigger");
                    Invoke("AnimDelay", 3f);

                    break;

                case "Left":

                    // GameObject.FindGameObjectWithTag("Host").GetComponent<NetworkAnimator>().SetTrigger("LeftTrigger");
                    ClientAnimREF.SetBool("UpBool", false);
                    ClientAnimREF.SetBool("RightBool", false);
                    ClientAnimREF.SetBool("LeftBool", true);
                    ClientAnimREF.SetTrigger("LeftTrigger");
                    Invoke("AnimDelay", 3f);

                    break;

                case "Up":
                    ClientAnimREF.SetBool("LeftBool", false);
                    ClientAnimREF.SetBool("RightBool", false);
                    ClientAnimREF.SetBool("UpBool", true);
                    //GameObject.FindGameObjectWithTag("Host").GetComponent<NetworkAnimator>().SetTrigger("UpTrigger");
                    ClientAnimREF.SetTrigger("UpTrigger");
                    Invoke("AnimDelay", 3f);

                    break;

                default:
                    ClientInputREF = "void";
                    ClientAnimREF.SetBool("LeftBool", false);
                    ClientAnimREF.SetBool("RightBool", false);
                    ClientAnimREF.SetBool("UpBool", false);
                    Invoke("AnimDelay", 3f);
                    break;
            }
        }

            if (HostAttacking == true)
            {
                switch (HostInputREF)
                {
                case "Right":
                    // GameObject.FindGameObjectWithTag("Host").GetComponent<NetworkAnimator>().SetTrigger("RightTrigger");
                    HostAnimREF.SetBool("UpBool", false);
                    HostAnimREF.SetBool("LeftBool", false);
                    HostAnimREF.SetBool("RightBool", true);
                    HostAnimREF.SetTrigger("RightTrigger");
                    Invoke("AnimDelay", 3f);

                    break;

                case "Left":

                    // GameObject.FindGameObjectWithTag("Host").GetComponent<NetworkAnimator>().SetTrigger("LeftTrigger");
                    HostAnimREF.SetBool("UpBool", false);
                    HostAnimREF.SetBool("RightBool", false);
                    HostAnimREF.SetBool("LeftBool", true);
                    HostAnimREF.SetTrigger("LeftTrigger");
                    Invoke("AnimDelay", 3f);

                    break;

                case "Up":
                    HostAnimREF.SetBool("LeftBool", false);
                    HostAnimREF.SetBool("RightBool", false);
                    HostAnimREF.SetBool("UpBool", true);
                    //GameObject.FindGameObjectWithTag("Host").GetComponent<NetworkAnimator>().SetTrigger("UpTrigger");
                    HostAnimREF.SetTrigger("UpTrigger");
                    Invoke("AnimDelay", 3f);

                    break;

                default:
                    HostInputREF = "void";
                    HostAnimREF.SetBool("LeftBool", false);
                    HostAnimREF.SetBool("RightBool", false);
                    HostAnimREF.SetBool("UpBool", false);
                    Invoke("AnimDelay", 3f);
                    break;

                }

            }

    }


    [ClientRpc]
    public void RpcDude()
    {
        ClientInputREF = ClientPlayer.GetComponent<ClientCombat>().SyncedClientInput;

    }

    IEnumerator ChooseTime1()
    {
       // if (isServer)
       // {
            while (Countdown > 0)
            {
                yield return new WaitForSecondsRealtime(1);
                Countdown--;
            }

            StartCoroutine(CombatTimer1());

        //}


        //Debug.Log("Times UP!");
        //FightNotes.text = "Times UP!";
    }


    IEnumerator CombatTimer1()
    {
        CombatCheck();
        yield return new WaitForSecondsRealtime(4f);

        //if (isServer)
       // {
            if (isAlive == true)
            {


                Countdown = 3;
                StartCoroutine(ChooseTime1());

            }
        //}


    }

    // Update is called once per frame
    void Update ()
        {
       // Debug.Log(" Currernt Status : : " + HostAttacking);
        if (TurnOver >= 3)
        {
            HostAttacking = true;
        }
        else
        {
            HostAttacking = false;
        }

        if (GameObject.Find("NetworkPlayer(Clone)") != null)
        {


            if (GameObject.FindGameObjectWithTag("Client") == null)
            {
                if(GameObject.FindGameObjectWithTag("Fighter") != null)
                {
                    Debug.Log("1");
                   ClientPlayer = GameObject.FindGameObjectWithTag("Fighter");
                    ClientPlayer.tag = "Client";
                    ClientPlayer.AddComponent<ClientCombat>();
                }

            }
            else
            if (GameObject.FindGameObjectWithTag("Host") == null)
            {
                if(GameObject.FindGameObjectWithTag("Fighter") != null)
                {
                    Debug.Log("2");

                    HostPlayer = GameObject.FindGameObjectWithTag("Fighter");
                    HostPlayer.tag = "Host";
                    HostPlayer.AddComponent<HostCombat>();
                    HostPlayer.AddComponent<NetworkCombat>();

                }

            }
        }


        if (ReferencesFound == true)
        {
            
            if (once == false && HostPlayer.GetComponent<HostCombat>().HostHealth >= 0f)
            {
                HostHealthREF = HostPlayer.GetComponent<HostCombat>().HostHealth;
                HostDamageREF = HostPlayer.GetComponent<HostCombat>().HostDamage;



                ClientHealthREF = ClientPlayer.GetComponent<ClientCombat>().ClientHealth;
                ClientDamageREF = ClientPlayer.GetComponent<ClientCombat>().ClientDamage;
                once = true;
            }


            Counter = GameObject.FindGameObjectWithTag("Counter").GetComponent<Text>();
            FightNotes = GameObject.Find("FightNotes").GetComponent<Text>();

            HostInputREF = HostPlayer.GetComponent<NetworkPlayer>().HostInputREF;

            ClientInputREF = ClientPlayer.GetComponent<NetworkPlayer>().ClientInputREF;

            HostHealthBarREF = HostPlayer.GetComponent<HostCombat>().HostHealthBar;
            HostAnimREF = HostPlayer.GetComponent<HostCombat>().HostAnim;


            ClientHealthBarREF = ClientPlayer.GetComponent<ClientCombat>().ClientHealthBar;
            ClientAnimREF = ClientPlayer.GetComponent<ClientCombat>().ClientAnim;


            if (NetworkFightReady.fightReady == true && Off == false)
            {
                Off = true;
                StartCoroutine(ChooseTime1());
            }


            //FightReadyREF = NetworkFightReady.fightReady;

            if (TurnOver >= 6)
            {
                TurnOver = 0;
            }
            Counter.text = Countdown.ToString();

            randomInt = Random.Range(1, 4);

            if (HostHealthREF <= 0 && MatchOver == false && once == true)
            {
                MatchOver = true;
                Time.timeScale = 0.3f;
                StopAllCoroutines();
                HostAnimREF.SetTrigger("DeathTrigger");
                ClientAnimREF.SetTrigger("VictoryTrigger");
                MatchWon = false;
                Camera.main.GetComponent<SetCharPosition>().fightReady = false;
                if (isServer)
                {
                    FightNotes.text = "You Lost !";

                }
                else
                {
                    FightNotes.text = "You Won !";

                }
                Invoke("Gameloop", 7f);
            }

            if (ClientHealthREF <= 0 && MatchOver == false && once == true)
            {
                MatchOver = true;
                Time.timeScale = 0.3f;
                StopAllCoroutines();
                HostAnimREF.SetTrigger("VictoryTrigger");
                ClientAnimREF.SetTrigger("DeathTrigger");
                MatchWon = true;
                Camera.main.GetComponent<SetCharPosition>().fightReady = false;
                if (isServer)
                {
                    FightNotes.text = "You Won !";

                }
                else
                {
                    FightNotes.text = "You Lost !";

                }
                Invoke("Gameloop", 7f);
            }

           
        }
        
    }
    public void Gameloop()
    {
        //PlayerScript.GameOver = true;
        // Destroy(Drawer);
        // Destroy(Detector);
        NetworkFightReady.fightReady = false;
        Off = false;
        MatchOver = false;
        once = false;
        HostPlayer.tag = "Fighter";
        Destroy(HostPlayer.GetComponent<HostCombat>());
        ClientPlayer.tag = "Fighter";
        Destroy(ClientPlayer.GetComponent<ClientCombat>());

        if (!isServer)
        {
            NetworkManager.singleton.StopClient();
            NetworkLobbyManager.singleton.StopClient();
            SceneManager.LoadScene(1);
        }
        
        NetworkManager.singleton.StopHost();
        
        
        NetworkLobbyManager.singleton.StopServer();

        NetworkServer.DisconnectAll();
        //Network.Disconnect ();
        
        StartCoroutine(ExitDelay());

    } 






IEnumerator ExitDelay()
{
     
        yield return new WaitForSeconds(0.1f);
       // NetworkFightReady.OfflineCharREF.SetActive(true);
        SceneManager.LoadScene(1);
        
}
IEnumerator FindPlayers()
    {

        HostPlayer = GameObject.FindGameObjectWithTag("Host");
        ClientPlayer = GameObject.FindGameObjectWithTag("Client");

        Players = GameObject.FindGameObjectsWithTag("Fighter");

            if (HostPlayer && ClientPlayer != null)
            {
               // ReferencesFound = true;
               // FightReady = true;
                StopWhile = true;
            }
        
        if(StopWhile == false)
        {
            Debug.Log("No Player Could be Found");

            yield return new WaitForSecondsRealtime(1);

            StartCoroutine(FindPlayers());
        }
    
    }

}
