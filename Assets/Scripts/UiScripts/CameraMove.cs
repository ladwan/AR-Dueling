using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour {

    public GameObject GravePoint, MarketPoint, FightPoint;
    public float Speed = 2;
    Vector3 StartingPoint;
    public Canvas Overview, Grave, Colosseum, Market;

    bool isMarket, isGrave, isFight, isReset;

	// Use this for initialization
	void Start () {

        StartingPoint = transform.position;
       
    }



public void MarketClick()
    {
        Overview.enabled = false;
        Market.enabled = true;

        if (transform.position != MarketPoint.transform.position)
        {
            isMarket = true;
        }
    }

    public void GraveClick()
    {
        Overview.enabled = false;
        Grave.enabled = true;

        if (transform.position != GravePoint.transform.position)
        {
            isGrave = true;
        }
    }

    public void FightClick()
    {
        Overview.enabled = false;
        Colosseum.enabled = true;

        if (transform.position != FightPoint.transform.position)
        {
            isFight = true;
        }
    }

    public void GoBack()
    {
        Grave.enabled = false;
        Colosseum.enabled = false;
        Market.enabled = false;

        isFight = false;
        isGrave = false;
        isMarket = false;

        isReset = true;
        //transform.position = StartingPoint;
    }

    // Update is called once per frame
    void Update () {

        if (isMarket)
        {
            transform.position = Vector3.Lerp(transform.position, MarketPoint.transform.position, Time.deltaTime * Speed);
            if(transform.position.Round(1) == MarketPoint.transform.position.Round(1))
            {
                isMarket = false;
                Debug.Log("Market Reached");
            }
        }

        if (isGrave)
        {
            transform.position = Vector3.Lerp(transform.position, GravePoint.transform.position, Time.deltaTime * Speed);
            if (transform.position.Round(1) == GravePoint.transform.position.Round(1))
            {
                isGrave = false;
                Debug.Log("Grave Reached");
            }
        }

        if (isFight)
        {
            transform.position = Vector3.Lerp(transform.position, FightPoint.transform.position, Time.deltaTime * Speed);
            if (transform.position.Round(1) == FightPoint.transform.position.Round(1))
            {
                isFight = false;
                Debug.Log("Colleseum Reached");
            }
        }

        if (isReset)
        {
            transform.position = Vector3.Lerp(transform.position, StartingPoint, Time.deltaTime * Speed);
            if ( transform.position.Round(1) ==  StartingPoint.Round(1))
            {
                isReset = false;
                Overview.enabled = true;
                Debug.Log("Reset Reached");
            }
        }
    }
}
