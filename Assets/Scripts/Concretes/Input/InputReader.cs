using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, IInputReader
{
    private const string PLAYER_PREFS_BINDINGS = "InputKeyBindings";
    public static InputReader Instance { get; private set; }
    public Vector3 Direction { get; private set; }
    public bool IsMoving { get; private set; }
    public event EventHandler OnInteraction;
    public event EventHandler OnInteractionAlternative;
    public event Action OnPause;
    public event Action OnBindingRebind;

    public enum Bindings
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interaction,
        InteractionAlternative,
        Pause,
        Gamepad_Interact,
        Gamepad_InteractAlternative,
        Gamepad_Pause
    }
    private Inputs _inputs;
    private void Awake()
    {
        Instance = this;
        _inputs = new Inputs();
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            _inputs.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
        
    }
    private void OnEnable()
    {
        _inputs.Player.Enable();
        _inputs.Player.Move.started += MovementAction;
        _inputs.Player.Move.performed += MovementAction;
        _inputs.Player.Move.canceled += MovementAction;
        _inputs.Player.Interact.performed += OnInteractionPerformed;
        _inputs.Player.InteractAlternative.performed += OnInteractionAlternativePerformed;
        _inputs.Player.Pause.performed += PauseOnPerformed;
    }

    private void PauseOnPerformed(InputAction.CallbackContext obj)
    {
        OnPause?.Invoke();
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
        _inputs.Dispose();
    }

    public string GetBindingText(Bindings bindings)
    {
        switch (bindings)
        {
            default:
            case Bindings.Interaction:
                return _inputs.Player.Interact.bindings[0].ToDisplayString();
            case Bindings.InteractionAlternative:
                return _inputs.Player.InteractAlternative.bindings[0].ToDisplayString();
            case Bindings.Pause:
                return _inputs.Player.Pause.bindings[0].ToDisplayString();
            case Bindings.Gamepad_Interact:
                return _inputs.Player.Interact.bindings[1].ToDisplayString();
            case Bindings.Gamepad_InteractAlternative:
                return _inputs.Player.InteractAlternative.bindings[1].ToDisplayString();
            case Bindings.Gamepad_Pause:
                return _inputs.Player.Pause.bindings[1].ToDisplayString();
            case Bindings.MoveUp:
                return _inputs.Player.Move.bindings[1].ToDisplayString();
            case Bindings.MoveDown:
                return _inputs.Player.Move.bindings[2].ToDisplayString();
            case Bindings.MoveLeft:
                return _inputs.Player.Move.bindings[3].ToDisplayString();
            case Bindings.MoveRight:
                return _inputs.Player.Move.bindings[4].ToDisplayString();

        }
    }

    public void RebindBinding(Bindings bindings, Action onActionRebound)
    {
        _inputs.Player.Disable();
        InputAction inputAction;
        int bindingIndex;
        switch (bindings)
        {
            default:
            case Bindings.MoveUp:
                inputAction = _inputs.Player.Move;
                bindingIndex = 1;
                break;
            case Bindings.MoveDown:
                inputAction = _inputs.Player.Move;
                bindingIndex = 2;
                break;
            case Bindings.MoveLeft:
                inputAction = _inputs.Player.Move;
                bindingIndex = 3;
                break;
            case Bindings.MoveRight:
                inputAction = _inputs.Player.Move;
                bindingIndex = 4;
                break;
            case Bindings.Interaction:
                inputAction = _inputs.Player.Interact;
                bindingIndex = 0;
                break;
            case Bindings.InteractionAlternative:
                inputAction = _inputs.Player.InteractAlternative;
                bindingIndex = 0;
                break;
            case Bindings.Pause:
                inputAction = _inputs.Player.Pause;
                bindingIndex = 0;
                break;
            case Bindings.Gamepad_Interact:
                inputAction = _inputs.Player.Interact;
                bindingIndex = 1;
                break;
            case Bindings.Gamepad_InteractAlternative:
                inputAction = _inputs.Player.InteractAlternative;
                bindingIndex = 1;
                break;
            case Bindings.Gamepad_Pause:
                inputAction = _inputs.Player.Pause;
                bindingIndex = 1;
                break;
        }
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            callback.Dispose();
            _inputs.Player.Enable();
            onActionRebound();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS,_inputs.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            
            OnBindingRebind?.Invoke();
        }).Start();
    }
}
