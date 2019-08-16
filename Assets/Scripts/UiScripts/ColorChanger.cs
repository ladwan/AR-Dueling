using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    public Color[] bodyColors;
    public Material bodyMat;
    public int Body, Hands, Feet;
    //public Color Hand, Feet, Body;

    public void ChangeBodyColor(int colorindex)
    {
        bodyMat.color = bodyColors[colorindex];
        Body = colorindex;
        PlayerPrefs.SetInt("BodyColor", Body);

    }

    public Color[] handColors;
    public Material handMat;

    public void ChangeHandColor(int colorindex)
    {
        handMat.color = handColors[colorindex];
        Hands = colorindex;
        PlayerPrefs.SetInt("HandColor", Hands);
    }


    public Color[] feetColors;
    public Material feetMat;

    public void ChangeFeetColor(int colorindex)
    {
        feetMat.color = feetColors[colorindex];
        Feet = colorindex;
        PlayerPrefs.SetInt("FeetColor", Feet);
    }





}