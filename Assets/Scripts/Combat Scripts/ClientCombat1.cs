using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientCombat1 : MonoBehaviour {

    public GameObject[] Players;
    public GameObject HostRef, ClientRef;
    public float ClientHealth = 100f;
    public float ClientDamage = 10f;
    public bool HostTurnREF;
    public string ClientInput;
    public Image ClientHealthBar;
    public Animator ClientAnim;
    public Slider ClientSlider, ClientSliderUP;


    // Use this for initialization
    void Start () {

        gameObject.transform.position = new Vector3(0, 10000, 0);

        var children = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
            if (child.name == "Health")
            {
                ClientHealthBar = child.GetComponent<Image>();
            }

        ClientAnim = gameObject.GetComponent<Animator>();

    }

    public void ClientAttackLeft()
    {
        ClientInput = "Left";
    }
    public void ClientAttackRight()
    {
        ClientInput = "Right";

    }
    public void ClientAttackUp()
    {
        ClientInput = "Up";

    }

    public void Guard()
    {
        if (ClientSliderUP.value == 1)
        {
            ClientInput = "Up";

            Debug.Log("Guarding Up !");

        }

        if (ClientSlider.value == 0)
        {
            ClientInput = "Left";
            Debug.Log("Guarding Left !");
        }
        else if (ClientSlider.value == 1)
        {
            ClientInput = "Right";
            Debug.Log("Guarding Right !");
        }
    }

    public void ClientUpDelay()
    {
        ClientSlider.interactable = false;
        Invoke("ClientUpDelay2", 2f);
    }

    void ClientUpDelay2()
    {
        ClientSliderUP.value = 0;
        ClientSlider.interactable = true;


    }
    public void ClientSideDelay()
    {
        ClientSliderUP.interactable = false;
        Invoke("ClientSideDelay2", 2f);
    }

    void HostSideDelay2()
    {
        ClientSlider.value = 0.5f;
        ClientSliderUP.interactable = true;


    }



    // Update is called once per frame
    void Update () {
		
	}
}
