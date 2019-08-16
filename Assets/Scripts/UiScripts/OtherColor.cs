using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherColor : MonoBehaviour
{

    public Color[] bodyColors;
    public Material bodyMat;
    public int Body, Hands, Feet;
    //public Color Hand, Feet, Body;
    

    public void ChangeBodyColor()
    {
        Debug.Log(Body + "OtherBodybody");

        bodyMat.color = bodyColors[Body];
      
    }

    public Color[] handColors;
    public Material handMat;

    public void ChangeHandColor()
    {
        handMat.color = handColors[Hands];
      
    }


    public Color[] feetColors;
    public Material feetMat;

    public void ChangeFeetColor()
    {
        feetMat.color = feetColors[Feet];
       
    }





}