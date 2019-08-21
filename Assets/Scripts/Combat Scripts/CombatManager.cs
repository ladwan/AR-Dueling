using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour {
    
    
    public GameObject[] Players;
    public GameObject HostPlayer, ClientPlayer, Drawer, Detector, CpuAggroREF;
    public int Countdown = 3, randomInt, TurnOver = 0, randomInt2, FeintChance;
    public bool  MatchWon, FightReady, MatchOver = false, doOnce;
    public string HostInputREF, ClientInputREF, PowerOfAtt, HostAttPower;
    public float HostHealthREF, HostDamageREF, ClientHealthREF, ClientDamageREF;
    public Image HostHealthBarREF, ClientHealthBarREF;
    public Text  FightNotes, Counter;
    bool isAlive = true, FightReadyREF, Off,ReferencesFound;
    public Animator HostAnimREF, ClientAnimREF, ClientAnimBallREF;
    public Color Red, Yellow, Black;
    public Material Thing;

    bool LightCheck1, LightCheck2, LightCheck3;
    bool HeavyCheck1, HeavyCheck2, HeavyCheck3;

    public int Blockable = 0;
    public bool GoodBlock;
    // Use this for initialization
    void Start () {

        /*
        GameObject.FindGameObjectWithTag("Drawer").AddComponent<SwipeDrawing>();

        Detector = new GameObject("Swipe Detector");
        Detector.AddComponent<SwipeDetection>();

        Drawer = new GameObject("Swipe Drawer");
        Drawer.AddComponent<LineRenderer>();
        Drawer.AddComponent<SwipeDrawing>();
        */

        Players = GameObject.FindGameObjectsWithTag("Fighter");


        //Find Players in Scene
        if (Players.Length != 0)
        {
            if(Players[0].name == "PlayerChar01")
            {
                HostPlayer = Players[0];
                HostPlayer.tag = "Host";
                HostPlayer.AddComponent<HostCombat1>();

                ClientPlayer = Players[1];
                ClientPlayer.tag = "Client";
                ClientPlayer.AddComponent<ClientCombatOg>();
            }
            else
            {
                HostPlayer = Players[1];
                HostPlayer.tag = "Host";
                HostPlayer.AddComponent<HostCombat1>();

                ClientPlayer = Players[0];
                ClientPlayer.tag = "Client";
                ClientPlayer.AddComponent<ClientCombatOg>();
            }
            



            Debug.Log("Host Found !");
            Debug.Log("Client Found !");
            ReferencesFound = true;
            FightReady = true;
        }
        else
        {
            Debug.Log("No Players Could Be Found !");

        }

        if (ReferencesFound == true)
        {




            HostHealthREF = HostPlayer.GetComponent<HostCombat1>().HostHealth;
            HostDamageREF = HostPlayer.GetComponent<HostCombat1>().HostDamage;
          


            ClientHealthREF = ClientPlayer.GetComponent<ClientCombatOg>().ClientHealth;
            ClientDamageREF = ClientPlayer.GetComponent<ClientCombatOg>().ClientDamage;



            /* for(int i = 0; i < 15; i++)
             {
                 Debug.Log(ClientPlayer.transform.GetChild(i).name + "Num:  " + i);
             }  */


            CpuAggroREF = ClientPlayer.transform.GetChild(1).transform.GetChild(0).gameObject;
            Thing = CpuAggroREF.GetComponent<Renderer>().material;


            if (GameObject.Find("CpuAgroDirection") != null)
            {
                if (GameObject.Find("CpuAgroDirection").GetComponent<Animator>() != null)
                {

                    ClientAnimBallREF = GameObject.Find("CpuAgroDirection").GetComponent<Animator>();


                }

            }
        }



    }


    void AnimDelay()
    {
        if(HostCombat1.HostTurn == false)
        {
            ClientAnimBallREF.SetBool("FeintBool", false);

            switch (FindObjectOfType<HostCombat1>().BlockableREF)
            {
                case 0:
                    HostHealthREF -= ClientDamageREF;
                    HostHealthBarREF.fillAmount = HostHealthREF / 100f;
                    FightNotes.text = "A Clean Hit !";
                    HostAnimREF.SetTrigger("DEFBrokenTrigger");
                    ClientAnimREF.SetTrigger("ATTAttackTrigger");

                    HostPlayer.GetComponent<HostCombat1>().HostInput = null;
                    FindObjectOfType<HostCombat1>().BlockableREF = 0;

                    break;

                case 1:
                    if (HostInputREF == ClientInputREF)
                    {
                        TurnOver++;
                        FightNotes.text = "Blocked !";
                        HostAnimREF.SetTrigger("DEFBlockTrigger");
                        ClientAnimREF.SetTrigger("ATTBlockedTrigger");

                        HostPlayer.GetComponent<HostCombat1>().HostInput = null;
                        FindObjectOfType<HostCombat1>().BlockableREF = 0;
                    }
                    else
                    {
                        HostHealthREF -= ClientDamageREF;
                        HostHealthBarREF.fillAmount = HostHealthREF / 100f;
                        FightNotes.text = "A Clean Hit !";
                        HostAnimREF.SetTrigger("DEFBrokenTrigger");
                        ClientAnimREF.SetTrigger("ATTAttackTrigger");

                        HostPlayer.GetComponent<HostCombat1>().HostInput = null;
                        FindObjectOfType<HostCombat1>().BlockableREF = 0;
                    }


                    break;

                case 2:
                    if (HostInputREF == ClientInputREF)
                    {
                        TurnOver = 4;
                        FightNotes.text = "Blocked !";
                        HostAnimREF.SetTrigger("DEFBlockTrigger");
                        ClientAnimREF.SetTrigger("ATTBlockedTrigger");

                        HostPlayer.GetComponent<HostCombat1>().HostInput = null;
                        FindObjectOfType<HostCombat1>().BlockableREF = 0;
                    }
                    else
                    {
                        HostHealthREF -= ClientDamageREF;
                        HostHealthBarREF.fillAmount = HostHealthREF / 100f;
                        FightNotes.text = "A Clean Hit !";
                        HostAnimREF.SetTrigger("DEFBrokenTrigger");
                        ClientAnimREF.SetTrigger("ATTAttackTrigger");

                        HostPlayer.GetComponent<HostCombat1>().HostInput = null;
                        FindObjectOfType<HostCombat1>().BlockableREF = 0;
                    }

                    break;
            }
        }

        if (HostCombat1.HostTurn == true)
        {
            if (HostInputREF == null)
            {
                TurnOver++;
                return;
            }

            if (HostInputREF == ClientInputREF)
            {
                TurnOver++;
                FightNotes.text = "Blocked !";
                HostAnimREF.SetTrigger("ATTBlockedTrigger");
                ClientAnimREF.SetTrigger("DEFBlockTrigger");

                return;
            }

            else

            if (HostInputREF == "Up")
            {
                if (HostInputREF != ClientInputREF)
                {
                    HostAnimREF.SetTrigger("ATTUpTrigger");

                    ClientAnimREF.SetTrigger("DEFBrokenTrigger");
                    HostAttPowerCheck();
                    ClientHealthBarREF.fillAmount = ClientHealthREF / 100f;

                    return;


                }
            }

            else

            if (HostInputREF == "Right")
            {
                if (HostInputREF != ClientInputREF)
                {
                    HostAnimREF.SetTrigger("ATTRightTrigger");

                    ClientAnimREF.SetTrigger("DEFBrokenTrigger");
                    HostAttPowerCheck();
                    ClientHealthBarREF.fillAmount = ClientHealthREF / 100f;

                    return;

                }
            }

            else

            if (HostInputREF == "Left")
            {
                if (HostInputREF != ClientInputREF)
                {
                    HostAnimREF.SetTrigger("ATTAttackTrigger");

                    ClientAnimREF.SetTrigger("DEFBrokenTrigger");
                    HostAttPowerCheck();
                    ClientHealthBarREF.fillAmount = ClientHealthREF / 100f;

                    return;
                }
            }




            

            


        }
    }

    void HostAttPowerCheck()
    {
        if(HostAttPower == "Light")
        {
            ClientHealthREF -= HostDamageREF;
            FightNotes.text = "A Clean Hit !";
            Debug.Log("LIGHTTTTT");
        }

        if (HostAttPower == "Heavy")
        {
            ClientHealthREF -= HostDamageREF + 5;
            FightNotes.text = "A Clean Hit !";
            Debug.Log("HEAVYYYYY");

        }
    }

    void CombatCheck()
    {
        Debug.Log(FeintChance);
        if (FeintChance == 1 )
        {
            ClientAnimBallREF.SetBool("FeintBool", true);
        }
        if (ClientAnimBallREF.GetBool("FeintBool") == false)
        {
            switch (randomInt2)
            {
                case 1:
                    PowerOfAtt = "Light";

                    break;

                case 2:
                    PowerOfAtt = "Heavy";

                    break;

            }
        }
        if (HostCombat1.HostTurn == false)
        {


            switch (randomInt)
            {
                case 1:
                    ClientInputREF = "Left";
                    ClientAnimBallREF.SetTrigger("LeftTrigger");
                    if(ClientAnimBallREF.GetBool("FeintBool") == true)
                    {
                        Blockable = 0;
                        Thing.SetColor("_EmissionColor", Black);
                        CpuAggroREF.GetComponent<Renderer>().material.color = Black;

                        ClientAnimBallREF.SetInteger("FeintInt", Random.Range(0, 2));
                        ActiveFeint1();

                    }

                    Invoke("AnimDelay", 5f);
                  


                    break;

                case 2:
                    ClientInputREF = "Right";
                    ClientAnimBallREF.SetTrigger("RightTrigger");
                    if (ClientAnimBallREF.GetBool("FeintBool") == true)
                    {
                        Blockable = 0;
                        Thing.SetColor("_EmissionColor", Black);
                        CpuAggroREF.GetComponent<Renderer>().material.color = Black;

                        ClientAnimBallREF.SetInteger("FeintInt", Random.Range(0, 2));
                        ActiveFeint1();

                    }
                    Invoke("AnimDelay", 5f);
                 

                    break;

                case 3:
                    ClientInputREF = "Up";
                    ClientAnimBallREF.SetTrigger("UpTrigger");
                    if (ClientAnimBallREF.GetBool("FeintBool") == true)
                    {
                        Blockable = 0;
                        Thing.SetColor("_EmissionColor", Black);
                        CpuAggroREF.GetComponent<Renderer>().material.color = Black;

                        ClientAnimBallREF.SetInteger("FeintInt", Random.Range(0, 2));
                        ActiveFeint1();

                    }
                    Invoke("AnimDelay", 5f);
     
                    break;

            }


        }
        else
        {


            switch (randomInt)
            {
                case 1:
                    ClientInputREF = "Left";
           
                    Invoke("AnimDelay", 3f);
                   

                    break;

                case 2:
                    ClientInputREF = "Right";
               
                    Invoke("AnimDelay", 3f);


                    break;

                case 3:
                    ClientInputREF = "Up";
               
                    Invoke("AnimDelay", 3f);

                    break;

            }
        }





    }

    public void ActiveFeint1()
    {

        Invoke("ActiveFeint", 2.3f);

    }

    public void ActiveFeint()
    {
        Invoke("ActiveFeint2", .3f);

        ClientAnimBallREF.SetTrigger("FeintTrigger");

    }

    public void ActiveFeint2()
    {
        Blockable = 2;
        Thing.SetColor("_EmissionColor", Color.yellow);
        CpuAggroREF.GetComponent<Renderer>().material.color = Yellow;

    }

    IEnumerator ChooseTime1()
    {
        while (Countdown > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            Countdown--;
        }

        //Debug.Log("Times UP!");
        //FightNotes.text = "Times UP!";
        Counter.enabled = false;
        StartCoroutine(CombatTimer1());
    }


    IEnumerator CombatTimer1()
    {
        yield return new WaitForSecondsRealtime(0f);


        if (isAlive == true)
        {
            CombatCheck();
            
            
            yield return new WaitForSecondsRealtime(7f);
            Counter.enabled = true;
            Countdown = 3;
            StartCoroutine(ChooseTime1());

        }

    }


    IEnumerator CpuLightAtt()
    {

        yield return new WaitForSeconds(3);
        doOnce = false;
        LightCheck1 = false;
        LightCheck2 = false;
        LightCheck3 = false;
        HeavyCheck1 = false;
        HeavyCheck2 = false;
        HeavyCheck3 = false;
    }
    // Update is called once per frame



        public void BlockCheck()
    {
        GoodBlock = true;
        
    }


    void Update ()
    {


        if (ReferencesFound == true)
        {
           

            HostInputREF = HostPlayer.GetComponent<HostCombat1>().HostInput;

            HostHealthBarREF = HostPlayer.GetComponent<HostCombat1>().HostHealthBar;
            HostAnimREF = HostPlayer.GetComponent<HostCombat1>().HostAnim;


            ClientHealthBarREF = ClientPlayer.GetComponent<ClientCombatOg>().ClientHealthBar;
            ClientAnimREF = ClientPlayer.GetComponent<ClientCombatOg>().ClientAnim;
            HostAnimREF.SetBool("isFight", true);
            ClientAnimREF.SetBool("isFight", true);

        }

        if (FightReadyREF == true && Off == false)
        {
            Off = true;
            StartCoroutine(ChooseTime1());
        }


        FightReadyREF = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SetCharPosition>().fightReady;

        if (TurnOver >= 6)
        {
            TurnOver = 0;
        }
        Counter.text = Countdown.ToString();

        randomInt = Random.Range(1, 4);
        randomInt2 = Random.Range(1, 3);

        FeintChance = Random.Range(1, 6);


        if (ClientAnimBallREF != null)
        {
            if (HostCombat1.HostTurn == false)
            {

                if (PowerOfAtt == "Light")
                {

                    if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).IsName("CpuAggroLeft 0") || ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).IsName("CpuAggroRight 0") || ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).IsName("CpuAggroUp 0"))
                    {

                        //Debug.Log(ClientAnimREF.GetCurrentAnimatorStateInfo(0).normalizedTime + "   EEEMMMMMMMMMMMM");
                        if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime < .13f)
                        {
                            Blockable = 0;

                            CpuAggroREF.GetComponent<Renderer>().material.color = Black;

                            Thing.SetColor("_EmissionColor", Black);

                            
                        }

                        if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime > .14f && ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime < .19f)
                        {
                            Blockable = 1;



                            LightCheck1 = true;
                            Thing.SetColor("_EmissionColor", Color.red);
                            CpuAggroREF.GetComponent<Renderer>().material.color = Red;
                            //yield return new WaitForSeconds(.16f);
                            //StartCoroutine(CpuLightAtt());
                        }

                        if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime >= .20f && LightCheck1 == true && LightCheck2 == false)
                        {
                            Blockable = 2;




                            LightCheck2 = true;
                            Thing.SetColor("_EmissionColor", Color.yellow);
                            CpuAggroREF.GetComponent<Renderer>().material.color = Yellow;
                            //yield return new WaitForSeconds(.12f);
                            //StartCoroutine(CpuLightAtt());
                        }

                        if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime >= .25f && LightCheck2 == true && LightCheck3 == false)
                        {

                            Blockable = 1;




                            LightCheck3 = true;
                            Thing.SetColor("_EmissionColor", Color.red);
                            CpuAggroREF.GetComponent<Renderer>().material.color = Red;

                            //StopCoroutine(CpuLightAtt());
                        }

                        if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime > .30f && LightCheck3 == true)
                        {

                            Blockable = 0;

                            Thing.SetColor("_EmissionColor", Black);
                            CpuAggroREF.GetComponent<Renderer>().material.color = Black;
                            if (doOnce == false)
                            {
                                doOnce = true;
                                StartCoroutine(CpuLightAtt());
                            }
                        }


                    }
                }











                if (PowerOfAtt == "Heavy")
                {

                    if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).IsName("CpuAggroLeft 0") || ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).IsName("CpuAggroRight 0") || ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).IsName("CpuAggroUp 0"))
                    {

                        //Debug.Log(ClientAnimREF.GetCurrentAnimatorStateInfo(0).normalizedTime + "   EEEMMMMMMMMMMMM");
                        if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime < .20f)
                        {
                            Blockable = 0;

                            CpuAggroREF.GetComponent<Renderer>().material.color = Black;

                            Thing.SetColor("_EmissionColor", Black);


                        }

                        if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime > .21f && ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime < .29f)
                        {

                            Blockable = 1;

    

                            HeavyCheck1 = true;
                            Thing.SetColor("_EmissionColor", Color.red);
                            CpuAggroREF.GetComponent<Renderer>().material.color = Red;
                            //yield return new WaitForSeconds(.16f);
                            //StartCoroutine(CpuLightAtt());
                        }

                        if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime >= .30f && HeavyCheck1 == true && HeavyCheck2 == false)
                        {

                            Blockable = 2;



                            HeavyCheck2 = true;
                            Thing.SetColor("_EmissionColor", Color.yellow);
                            CpuAggroREF.GetComponent<Renderer>().material.color = Yellow;
                            //yield return new WaitForSeconds(.12f);
                            //StartCoroutine(CpuLightAtt());
                        }

                        if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime >= .40f && HeavyCheck2 == true && HeavyCheck3 == false)
                        {


                            HeavyCheck3 = true;
                            Thing.SetColor("_EmissionColor", Color.red);
                            CpuAggroREF.GetComponent<Renderer>().material.color = Red;

                            //StopCoroutine(CpuLightAtt());
                        }

                        if (ClientAnimBallREF.GetCurrentAnimatorStateInfo(0).normalizedTime > .50f && HeavyCheck3 == true)
                        {
                            Blockable = 0;


                            Thing.SetColor("_EmissionColor", Black);
                            CpuAggroREF.GetComponent<Renderer>().material.color = Black;
                            if (doOnce == false)
                            {
                                doOnce = true;
                                StartCoroutine(CpuLightAtt());
                            }
                        }


                    }
                }
            }
        }




        if (HostHealthREF <= 0 && MatchOver == false)
        {
            MatchOver = true;
            Time.timeScale = 0.3f;
            StopAllCoroutines();
            FightNotes.text = "You Lost !";
            HostAnimREF.SetTrigger("DeathTrigger");
            ClientAnimREF.SetTrigger("CpuVictoryTrigger");
            MatchWon = false;
            Camera.main.GetComponent<SetCharPosition>().fightReady = false;
            Invoke("Gameloop", 7f);
        }

        if (ClientHealthREF <= 0 && MatchOver == false)
        {
            MatchOver = true;
            Time.timeScale = 0.3f;
            StopAllCoroutines();
            FightNotes.text = "You Won !";
            HostAnimREF.SetTrigger("VictoryTrigger");
            ClientAnimREF.SetTrigger("CpuDeathTrigger");
            MatchWon = true;
            Camera.main.GetComponent<SetCharPosition>().fightReady = false;
            Invoke("Gameloop", 7f);
        }

        
    }
    public void Gameloop()
    {
        PlayerScript.GameOver = true;
       // Destroy(Drawer);
       // Destroy(Detector);
        HostAnimREF.SetTrigger("AfterFight");
        ClientAnimREF.SetTrigger("AfterFight");
        HostPlayer.GetComponent<HostCombat1>().HostHealth = 100f;
        ClientPlayer.GetComponent<ClientCombatOg>().ClientHealth = 100f;
        HostHealthREF = HostPlayer.GetComponent<HostCombat1>().HostHealth;
        ClientHealthREF = ClientPlayer.GetComponent<ClientCombatOg>().ClientHealth;
        HostHealthBarREF.fillAmount = HostHealthREF / 100f;
        ClientHealthBarREF.fillAmount = ClientHealthREF / 100f;
        //Destroy(PlayerScript.instance);
        Destroy(CPU.instance);
        //Destroy(HostPlayer);
        Destroy(ClientPlayer);
        HostPlayer.tag = "Fighter";
        Destroy(HostPlayer.GetComponent<HostCombat1>());
        ClientPlayer = Players[1];
        ClientPlayer.tag = "Fighter";
        Destroy(ClientPlayer.GetComponent<ClientCombatOg>());
        //Destroy(SwipeHolder);
        HostPlayer.GetComponent<PlayerScript>().FightUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(6);
    }
}
