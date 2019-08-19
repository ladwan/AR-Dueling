using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClassSelect : MonoBehaviour {

    public GameObject Panel;

    public void ClassSelected()
    {
        Panel.SetActive(true);
    }

    public void NotSure()
    {
        Panel.SetActive(false);

    }

    public void Sure()
    {
        FadeManager.instance.FadeOut(6);
    }
}
