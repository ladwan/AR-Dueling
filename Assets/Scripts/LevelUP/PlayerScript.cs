using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UMA;
using UMA.CharacterSystem;
public class PlayerScript : MonoBehaviour
{

    public static bool GameOver = false;
    public static PlayerScript instance;
    public static GameObject PlayerCanvas;
    public GameObject FightUI;
    bool YouWon;
    public static bool DoOnce;
    public float currentlvl, XpToLVL = 50f, NextLVL;
    public float currentXP, XpRollOver;
    Transform _playerSnapPoint;
    GameObject Player;

    
    public DynamicCharacterAvatar UmaRef;
    


    public void Start()
    {
        PlayerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas");
        FightUI = GameObject.Find("FightUI Holder");



        Debug.Log("\n Current Lvl :" +currentlvl + " Current XP :" + currentXP + "XP To Lvl :" + XpToLVL + " Next Lvl :" + NextLVL + " XP Roll Over :" + XpRollOver);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;

        if (GameObject.FindGameObjectWithTag("PlayerSnap") != null)
        {
            _playerSnapPoint = GameObject.FindGameObjectWithTag("PlayerSnap").transform;
        }

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }
    public void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {




    }
    public void CreateChar()
    {
        //FadeManager.instance.FadeOut(9);
        SceneManager.LoadScene("MainCity");
    }

    public void CheckStats()
    {

        currentXP += 10f;
        XpScript.XP += 10f;

        
    }

    public void UpdateXP()
    {
       

        if (currentlvl <= 1f && currentXP >= XpToLVL)
        {

            currentXP -= XpToLVL = XpRollOver;
            XpRollOver = currentXP;
            currentlvl = 2f;
            NextLVL = currentlvl + 1;


        }

        if (currentlvl == 2f && currentXP >= XpToLVL )
        {
     
            currentXP -= XpToLVL = XpRollOver;
            XpRollOver = currentXP;
            currentlvl = 3f;
            NextLVL = currentlvl + 1;

        }



    }


 
}
