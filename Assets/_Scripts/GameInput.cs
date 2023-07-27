using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnSkipTextActionStart;
    public event EventHandler OnSkipTextActionEnd;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There are more than one GameInput");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playerInputActions = new PlayerInputActions();

        _playerInputActions.Player.Enable();

        _playerInputActions.Player.SkipText.started += SkipText_started;
        _playerInputActions.Player.SkipText.canceled += SkipText_canceled;
    }

    private void SkipText_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSkipTextActionStart?.Invoke(this, EventArgs.Empty);
    }

    private void SkipText_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSkipTextActionEnd?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        _playerInputActions.Dispose();
    }

    public Vector2 GetPlayerMovement()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector;
    }
}
