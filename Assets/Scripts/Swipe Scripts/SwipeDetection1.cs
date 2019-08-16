using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SwipeDetection1 : MonoBehaviour {

    private Vector2 _fingerDownPosition;
    private Vector2 _fingerUpPosition;
    public ParticleSystem AttackParticles;
    public LineRenderer Line;

    [SerializeField]
    private bool DectectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float _minDistanceForSwipe = 20f;

    public static event Action<SwipeData1> OnSwipe1 = delegate { };


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    private void Update()
    {
         if (HostCombat1.HostTurn == true)
        //if(isLocalPlayer)
         {
            if (VerticalMovementDistance() > 100 && VerticalMovementDistance() < 300 || HorizontalMovementDistance() > 100 && HorizontalMovementDistance() < 300)
            {
                Line.material.color = Color.green;
            }

            if (VerticalMovementDistance() > 310 || HorizontalMovementDistance() > 310)
            {
                 Line.material.color = Color.red;
            }

             foreach (Touch touch in Input.touches)
             {
                    if (touch.phase == TouchPhase.Began)
                    {
                         _fingerUpPosition = touch.position;
                        _fingerDownPosition = touch.position;
                        // AttackParticles.transform.position = touch.position;
                    }

                    if (!DectectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
                    {
                        _fingerDownPosition = touch.position;
                        DetectSwipe();
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                         if (Line.material.color == Color.red)
                         {
                            GameObject.Find("Directional Light").GetComponent<CombatManager>().HostAttPower = "Heavy";
                         }
                         else 

                        if(Line.material.color == Color.green)
                        {
                            GameObject.Find("Directional Light").GetComponent<CombatManager>().HostAttPower = "Light";
                        }

                         _fingerDownPosition = touch.position;
                         Line.material.color = Color.white;
                         DetectSwipe();
                    }
             }
          //}

         }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = _fingerDownPosition.y - _fingerUpPosition.y > 0 ? SwipeDirection1.Up : SwipeDirection1.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = _fingerDownPosition.x - _fingerUpPosition.x > 0 ? SwipeDirection1.Right : SwipeDirection1.Left;
                SendSwipe(direction);
            }
        }


    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }


    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > _minDistanceForSwipe || HorizontalMovementDistance() > _minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x);
    }

    public void SendSwipe(SwipeDirection1 direction)
    {
        Debug.Log(direction);

        SwipeData1 _swipeData = new SwipeData1()
        {
            Direction = direction,
            StartPosition = _fingerDownPosition,
            EndPosition = _fingerUpPosition
        };



        if (direction == SwipeDetection1.SwipeDirection1.Down)
        {
            if (HostCombat1.HostTurn == true)
            {
                GameObject.FindGameObjectWithTag("Host").GetComponent<HostCombat1>().HostInput = "Up";


            }


        }

        if (direction == SwipeDetection1.SwipeDirection1.Right)
        {
            if (HostCombat1.HostTurn == true)
            {
                GameObject.FindGameObjectWithTag("Host").GetComponent<HostCombat1>().HostInput = "Right";


            }
        }

        if (direction == SwipeDetection1.SwipeDirection1.Left)
        {
            if (HostCombat1.HostTurn == true)
            {
                GameObject.FindGameObjectWithTag("Host").GetComponent<HostCombat1>().HostInput = "Left";

            }


        }

        OnSwipe1(_swipeData);
    }


    
    

    public struct SwipeData1
    {
        public Vector2 StartPosition;
        public Vector2 EndPosition;
        public SwipeDirection1 Direction;
    }

    public enum SwipeDirection1
    {
        Up,
        Down,
        Left,
        Right
    };



     
}
