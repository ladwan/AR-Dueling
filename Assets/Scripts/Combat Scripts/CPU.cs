using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CPU : MonoBehaviour
{


    public static CPU instance;
    public Animator CpuAnim;


    void Awake()
    {
       if (instance == null)
        {
            instance = this;

           // if (PlayerScript.GameOver == false)
           // {
                DontDestroyOnLoad(instance);
           // }

       }
       else
        {
            Destroy(gameObject);
        }

        if (PlayerScript.GameOver == true)
        {
            Destroy(instance);
        }
    }

}