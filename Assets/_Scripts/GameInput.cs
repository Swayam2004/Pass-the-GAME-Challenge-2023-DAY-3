using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

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
