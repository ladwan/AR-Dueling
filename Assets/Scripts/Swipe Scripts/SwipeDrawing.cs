using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwipeDrawing : MonoBehaviour {

    public LineRenderer lineRender;
    public ParticleSystem AttackParticles;
    private float zOffset = 0.5f;
    //public static bool LineOn = true;

    public void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        SwipeDetection1.OnSwipe1 += SwipeDetector_OnSwipe;
        lineRender.enabled = false;
    }


    public void SwipeDetector_OnSwipe (SwipeDetection1.SwipeData1 data)
    {
        // if (LineOn == true)
        //{
        lineRender = GetComponent<LineRenderer>();
        lineRender.enabled = true;
            Vector3[] positions = new Vector3[2];
            positions[0] = Camera.main.ScreenToWorldPoint(new Vector3(data.StartPosition.x, data.StartPosition.y, zOffset));
            positions[1] = Camera.main.ScreenToWorldPoint(new Vector3(data.EndPosition.x, data.EndPosition.y, zOffset));
            //AttackParticles.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(data.StartPosition.x, data.StartPosition.y, zOffset));
            lineRender.positionCount = 2;
            lineRender.SetPositions(positions);
            StartCoroutine(LineDelay());

        //}

        

    }

    private void OnDisable()
    {
        SwipeDetection1.OnSwipe1 -= SwipeDetector_OnSwipe;
    }
    IEnumerator LineDelay()
    {
        //LineOn = false;
        yield return new WaitForSecondsRealtime(1);
        lineRender.enabled = false;
        //yield return new WaitForSecondsRealtime(2);
       // LineOn = true;


    }
}
