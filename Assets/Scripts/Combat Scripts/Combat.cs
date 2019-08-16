using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.UI;



public class Combat : MonoBehaviour {

    public string playerInput, cpuInput;
    int randomInt;
    bool Fight;
    bool OffCheck = false;
    bool Dead = false;
    int Countdown = 3, AttBlocked, PlayerAttacks;
    public Text Counter, FightNotes;
    public Image playerHealthBar, enemyHealthBar;
    public float playerHealth = 100f , enemyHealth = 100f;
    public float playerDamage = 10f, enemyDamage = 10f;
    Animator Anim;
    Animator CpuAnim;
    public ParticleSystem Sparks;
    GameObject ParticleSpawn;
    public bool MatchWon, CpuAttacking;

    public Canvas PlayerCanvas, AttCanvas, DefCanvas;

    public Slider ShieldSlider,ShieldUp;
    // Use this for initialization
    void Start()
    {
        CpuAttacking = true;
        //playerHealthBar = PlayerCanvas.GetComponent<Image>();
        Anim = GameObject.Find("PlayerChar").GetComponent<Animator>();
        CpuAnim = GetComponent<Animator>();
        Fight =  GameObject.Find("First Person Camera").GetComponent<SetCharPosition>().fightReady;
        ParticleSpawn = GameObject.Find("ParticleSpawn");

    }

    // Update is called once per frame


    void CombatCheck ()
    {
        //Anim = GameObject.Find("PlayerChar").GetComponent<Animator>();

        playerHealthBar = GameObject.Find("HealthO").GetComponent<Image>();
        if (CpuAttacking == true)
        {

            switch (randomInt)
            {
                case 1:
                    cpuInput = "Left";
                    CpuAnim.SetTrigger("LeftTrigger");
                    Invoke("AnimDelay", 3f);
                    /* if (playerInput == "Left")
                     {
                         Debug.Log("Blocked!");

                         FightNotes.text = "Blocked !";



                        // Anim.SetTrigger("ATTDrawTrigger");
                        // CpuAnim.SetTrigger("CpuATTDraw");
                         Instantiate(Sparks, ParticleSpawn.transform.position, Quaternion.identity);

                     }

                     else 
                     {
                         Debug.Log("The Player Took Too Long");

                         FightNotes.text = "The Player Took Too Long";

                     }*/
                    break;

                case 2:
                    cpuInput = "Right";
                    CpuAnim.SetTrigger("RightTrigger");
                    Invoke("AnimDelay", 3f);
                    /*if (playerInput == "Right")
                    {
                        Debug.Log("Blocked!");

                        FightNotes.text = "Blocked !";



                         Anim.SetTrigger("ATTDrawTrigger");
                         CpuAnim.SetTrigger("CpuATTDraw");
                         Instantiate(Sparks, ParticleSpawn.transform.position, Quaternion.identity);

                    }

                    else
                    {
                        Debug.Log("The Player Took Too Long");

                        FightNotes.text = "The Player Took Too Long";

                    }*/

                    break;

                case 3:
                    cpuInput = "Up";
                    CpuAnim.SetTrigger("UpTrigger");
                    Invoke("AnimDelay", 3f);
                    /*if (playerInput == "Up")
                    {
                        Debug.Log("Blocked!");

                        FightNotes.text = "Blocked !";



                        // Anim.SetTrigger("ATTDrawTrigger");
                        // CpuAnim.SetTrigger("CpuATTDraw");
                        Instantiate(Sparks, ParticleSpawn.transform.position, Quaternion.identity);

                    }

                    else
                    {
                        Debug.Log("The Player Took Too Long");

                        FightNotes.text = "The Player Took Too Long";

                    }*/
                    break;

            }


        }
        else
        {
            switch (randomInt)
            {
                case 1:
                    cpuInput = "Left";
                    //CpuAnim.SetTrigger("LeftTrigger");
                    Invoke("AnimDelay", 3f);

                    break;

                case 2:
                    cpuInput = "Right";
                    //CpuAnim.SetTrigger("RightTrigger");
                    Invoke("AnimDelay", 3f);


                    break;

                case 3:
                    cpuInput = "Up";
                    //CpuAnim.SetTrigger("UpTrigger");
                    Invoke("AnimDelay", 3f);

                    break;
            }
        }
    }

    public void Guard()
    {
        if(ShieldUp.value == 1)
        {
            playerInput = "Up";
           
            Debug.Log("Guarding Up !");

        }

        if (ShieldSlider.value == 0)
        {
            playerInput = "Left";
            Debug.Log("Guarding Left !");
        }
        else if (ShieldSlider.value == 1)
        {
            playerInput = "Right";
            Debug.Log("Guarding Right !");
        }
    }

    public void AttackLeft()
    {
        playerInput = "Left";
    }
    public void AttackRight()
    {
        playerInput = "Right";

    }
    public void AttackUp()
    {
        playerInput = "Up";

    }


    IEnumerator ChooseTime()
    {
        while (Countdown > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            Countdown--;
        }

        Debug.Log("Times UP!");
        FightNotes.text = "Times UP!";
        StartCoroutine(CombatTimer());

    

    }

    IEnumerator CombatTimer()
    {
        yield return new WaitForSecondsRealtime(.01f);
       

        if (Dead == false )
        {
            CombatCheck();
            Countdown = 3;
            StartCoroutine(ChooseTime());
        }
        
    }
    
    void AnimDelay()
    {
        if (playerInput == cpuInput)
        {
            Debug.Log("Blocked!");
            AttBlocked++;
            FightNotes.text = "Blocked !";

        }
        else if (CpuAttacking == true)
        {
            playerHealth -= enemyDamage;
            playerHealthBar.fillAmount = playerHealth / 100f;
        }
        else
        {
            enemyHealth -= playerDamage;
            enemyHealthBar.fillAmount = enemyHealth / 100f;
        }
    }

    void Update()
    {
        if (AttBlocked >= 3)
        {
            CpuAttacking = false;
            AttCanvas.enabled = true;
            DefCanvas.enabled = false;
            PlayerAttacks = 0;


        }
        else
        {
            AttCanvas.enabled = false;
            DefCanvas.enabled = true;
        }

        Counter.text = Countdown.ToString();

        randomInt = Random.Range(1, 4);

        if (playerHealth <= 0)
        {
            Time.timeScale = 0.3f;
            FightNotes.text = "You Lost !";
            MatchWon = false;
        }

        if (enemyHealth <= 0)
        {
            Time.timeScale = 0.3f;

            FightNotes.text = "You Won !";

            MatchWon = true;

        }

        if (OffCheck == true)
        {
            return;
        }

        if (Fight == true && OffCheck == false)
        {

            StartCoroutine(ChooseTime());
            Debug.Log("Timer Started");
            OffCheck = true;
        }
        else return;
    }



   public void UpDelay()
    {
        ShieldSlider.interactable = false;
        Invoke("UpDelay2", 2f);
    }

    void UpDelay2()
    {
        ShieldUp.value = 0;
        ShieldSlider.interactable = true;


    }
    public void SideDelay()
    {
        ShieldUp.interactable = false;
        Invoke("SideDelay2", 2f);
    }

    void SideDelay2()
    {
        ShieldSlider.value = 0.5f;
        ShieldUp.interactable = true;


    }



}
