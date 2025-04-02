using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class SwipeControl : MonoBehaviour
{
    public static event Action SwipeLeft;
    public static event Action SwipeRight;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;


    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += OnFingerDown;
        Touch.onFingerUp += OnFingerUp;
    }

    private void OnDisable()
    {
        Touch.onFingerDown -= OnFingerDown;
        Touch.onFingerUp -= OnFingerUp;
        EnhancedTouchSupport.Disable();
    }
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

    private void OnFingerDown(Finger finger)
    {
        startTouchPosition = finger.screenPosition;
    }

    private void OnFingerUp(Finger finger)
    {
        endTouchPosition = finger.screenPosition;
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

    private void LeftSwipe()
    {
        SwipeLeft?.Invoke();
    }
    private void RightSwipe()
    {
        SwipeRight?.Invoke();
    }

}