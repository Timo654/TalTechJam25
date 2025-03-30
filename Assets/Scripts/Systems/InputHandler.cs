using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static event Action<Vector2> OnMoveInput;
    public static event Action OnPauseInput; // bool is if paused or not, true is paused

    private bool isPaused = false;
    private bool inputEnabled = false;
    InputAction movement;
    InputAction pause;
    private void Awake()
    {
        movement = InputSystem.actions.FindAction("Movement");
        pause = InputSystem.actions.FindAction("Pause");
    }

    private void OnEnable()
    {
        movement.performed += OnMovement;
        pause.performed += OnPause;
        movement.Enable();
        pause.Enable();
        PauseMenu.OnPauseGame += SetPaused;
        GameManager.GameActive += ToggleInput;
        CreditsHandler.AllowInput += ToggleInput;
        SwipeControl.SwipeRight += MoveRight;
        SwipeControl.SwipeLeft += MoveLeft;
    }

    private void OnDisable()
    {
        movement.performed -= OnMovement;
        pause.performed -= OnPause;
        movement.Disable();
        pause.Disable();
        PauseMenu.OnPauseGame -= SetPaused;
        GameManager.GameActive -= ToggleInput;
        CreditsHandler.AllowInput -= ToggleInput;
        SwipeControl.SwipeRight -= MoveRight;
        SwipeControl.SwipeLeft -= MoveLeft;
    }

    private void MoveRight()
    {
        OnMoveInput?.Invoke(Vector2.right);
    }

    private void MoveLeft()
    {
        OnMoveInput?.Invoke(Vector2.left);
    }

    private void ToggleInput(bool obj)
    {
        inputEnabled = obj;
    }

    private void SetPaused(bool val)
    {
        isPaused = val;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (!inputEnabled) return;
        OnPauseInput?.Invoke();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        if (isPaused || !inputEnabled) return;
        var moveDir = context.ReadValue<Vector2>();
        OnMoveInput?.Invoke(moveDir);
    }
}
