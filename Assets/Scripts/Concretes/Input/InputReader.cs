using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, IInputReader 
{
    public Vector3 Direction { get; private set; }
    public bool IsMoving { get; private set; }
    public event EventHandler OnInteraction;
    public event EventHandler OnInteractionAlternative;

    private Inputs _inputs;
    private void Awake()
    {
        _inputs = new Inputs();
        
    }
    private void OnEnable()
    {
        _inputs.Player.Move.started += MovementAction;
        _inputs.Player.Move.performed += MovementAction;
        _inputs.Player.Move.canceled += MovementAction;
        _inputs.Player.Interact.performed += OnInteractionPerformed;
        _inputs.Player.InteractAlternative.performed += OnInteractionAlternativePerformed;
        _inputs.Player.Enable();
    }

    private void OnInteractionAlternativePerformed(InputAction.CallbackContext context)
    {
        OnInteractionAlternative?.Invoke(this, EventArgs.Empty);
    }

    private void OnInteractionPerformed(InputAction.CallbackContext context)
    {
        OnInteraction?.Invoke(this, EventArgs.Empty);
    }

    private void MovementAction(InputAction.CallbackContext context)
    {
        Vector2 oldDirection = context.ReadValue<Vector2>();
        IsMoving = oldDirection.x != 0 || oldDirection.y != 0;
        Direction = new Vector3(oldDirection.x, 0f, oldDirection.y);
    }

    private void OnDisable()
    {
        _inputs.Player.Move.started -= MovementAction;
        _inputs.Player.Move.performed -= MovementAction;
        _inputs.Player.Move.canceled -= MovementAction;
        _inputs.Player.Interact.performed -= OnInteractionPerformed;
        _inputs.Player.InteractAlternative.performed -= OnInteractionAlternativePerformed;
    }
}
