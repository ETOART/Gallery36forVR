using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Swipe : MonoBehaviour

{
    [SerializeField] public UnityEvent leftSwipeEvent;
    [SerializeField] public UnityEvent rightSwipeEvent;
    
    CameraManager cameraManager;
    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    // Start is called before the first frame update
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SwipeDetection();
    }

    

    public void SwipeDetection()
    {
       
        if (Input.touches.Length > 0)
        {
           
            Touch t = Input.GetTouch(0);
            Rect touchArea = new Rect(0, Screen.height * 0.20f, Screen.width, Screen.height*0.75f);
            Debug.Log(touchArea);
            if (!touchArea.Contains(t.position))
                return;
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                if (!(secondPressPos == firstPressPos))
                {
                    if (Mathf.Abs(currentSwipe.x) > Mathf.Abs(currentSwipe.y))
                    {
                        if (currentSwipe.x < 0)
                        {
                            Debug.Log("Right swipe");
                           rightSwipeEvent.Invoke();
                        }
                        else
                        {
                            Debug.Log("Left swipe");
                            leftSwipeEvent.Invoke();
                        }
                    }
                    else
                    {
                        if (currentSwipe.y < 0)
                        {
                            // Swipe Down
                        }
                        else
                        {
                            //Swipe Up
                        }
                    }
                }

            }
        }
    }
}

