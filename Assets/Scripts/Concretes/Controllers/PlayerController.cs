using System;
using Kitchen.Abstract.Controller;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IEntityController, IKitchenObjectsParent
{
   //public static PlayerController Instance { get; private set; }
    
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    public event Action OnPickupSomething; 
    [SerializeField] private float _speed = 7f;
    [SerializeField] private LayerMask _counterLayerMask;
    [SerializeField] private Transform _kitchenObjectHoldPoint; 
    private Transform _transform;
    //private IInputReader _input;
    private IMover _mover;
    private IAnimator _animator;
    
    private Vector3 _direction;
    private Vector3 _lastInteractDir;

    private BaseCounter _selectedCounter;
    public Vector3 Direction => _direction;
    public KitchenObjectController KitchenObject { get; set; }
    private void Awake()
    {
        //Instance = this;
        
        _transform = GetComponent<Transform>();
        //_input = GetComponent<InputReader>();
        _mover = new MoveWithCharCont(this);
        _animator = new AnimationController(this);
    }

    private void Start()
    {
        InputReader.Instance.OnInteraction += HandleInteractionOnClick;
        InputReader.Instance.OnInteractionAlternative += HandleInteractionAlternativeOnClick;
    }

    private void HandleInteractionAlternativeOnClick(object sender, EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying()) return;
        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlternate(this);
        }    
    }

    private void HandleInteractionOnClick(object sender, EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying()) return;
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }

    void Update()
    {
        _direction = InputReader.Instance.Direction;
    }

    private void FixedUpdate()
    {
        if(!IsOwner) return;
        _mover.MoveAction(_direction, _speed);
        HandleInteraction();
    }

    private void LateUpdate()
    {
        if(!IsOwner) return;
        _animator.MoveAnimation(InputReader.Instance.IsMoving);
    }
    

    private void HandleInteraction()
    {
        
        if (_direction != Vector3.zero)
        {
            _lastInteractDir = _direction; // Bare in mind last interacted object
        }

        float interactionDistance = 2f;
        if (Physics.Raycast(_transform.position, _lastInteractDir, out RaycastHit raycastHit, interactionDistance, _counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != _selectedCounter) 
                {
                    SetSelectedCounter(baseCounter);

                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs()
        {
            selectedCounter = _selectedCounter
        });
    }

    public Transform GetKitchenObjectControllerCounterTransform()
    {
        OnPickupSomething?.Invoke();
        return _kitchenObjectHoldPoint;
    }

    public void ClearKitchenObject()
    {
        KitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return KitchenObject != null;
    }
}
