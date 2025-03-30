using System;
using UnityEngine;
public class SwipeControl : MonoBehaviour
{
    public static event Action SwipeLeft;
    public static event Action SwipeRight;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;



    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            Vector2 inputVector = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
            {
                if (inputVector.x > 0)
                {
                    RightSwipe();
                }
                else
                {
                    LeftSwipe();
                }
            }
        }

    }
    private void LeftSwipe()
    {
        SwipeLeft?.Invoke();
    }
    private void RightSwipe()
    {
        SwipeRight?.Invoke();
    }

}