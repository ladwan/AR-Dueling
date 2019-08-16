using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class SwipeDetection : NetworkBehaviour {

    private Vector2 _fingerDownPosition;
    private Vector2 _fingerUpPosition;
    public ParticleSystem AttackParticles;
    private bool Delay;

    [SerializeField]
    private bool DectectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float _minDistanceForSwipe = 20f;

    public static event Action<SwipeData> OnSwipe = delegate { };

    
	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    private void Update()
    {
        //if (HostCombat.HostTurn == true)
        //if(isLocalPlayer)
        //{
            
           // if(HostCombat.HostTurn == true || ClientCombat.ClientTurn == true)
            //{
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
                        _fingerDownPosition = touch.position;
                        DetectSwipe();
                    }
                }
            //}

        //}
	}

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = _fingerDownPosition.y - _fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = _fingerDownPosition.x - _fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
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

    public void SendSwipe(SwipeDirection direction)
    {
        SwipeData _swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = _fingerDownPosition,
            EndPosition = _fingerUpPosition
        };



        if (direction == SwipeDetection.SwipeDirection.Down)
        {
            if (HostCombat.HostTurn == true)
            {
                GameObject.FindGameObjectWithTag("Host").GetComponent<HostCombat>().HostInput = "Up";


            }
            else
            {
                GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientInput = "Up";
            }
        }

        if (direction == SwipeDetection.SwipeDirection.Right)
        {
            if (HostCombat.HostTurn == true)
            {
                GameObject.FindGameObjectWithTag("Host").GetComponent<HostCombat>().HostInput = "Right";


            }
            else
            {
                GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientInput = "Right";
            }

            if (direction == SwipeDetection.SwipeDirection.Left)
            {
                if (HostCombat.HostTurn == true)
                {
                    GameObject.FindGameObjectWithTag("Host").GetComponent<HostCombat>().HostInput = "Left";


                }
                else
                {
                    GameObject.FindGameObjectWithTag("Client").GetComponent<ClientCombat>().ClientInput = "Left";
                }

            }
        }
        OnSwipe(_swipeData);
    }
    

    public struct SwipeData
    {
        public Vector2 StartPosition;
        public Vector2 EndPosition;
        public SwipeDirection Direction;
    }

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    };

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3);
        Delay = false;
    }

     
}
