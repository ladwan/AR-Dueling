using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwipeLog : MonoBehaviour {

    private void Awake()
    {
        SwipeDetection1.OnSwipe1 += SwipeDetector_OnSwipe;
    }
    private void SwipeDetector_OnSwipe(SwipeDetection1.SwipeData1 data)
    {
        Debug.Log("Swipe In Direction: " + data.Direction);
    }
}
