using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    PlayerInputs _playerInputs;

    bool isRunning;
    Vector2 movement;
    float defaultMoveSpeed = 102f;
    [SerializeField] float multiplier = 1.7f;
    Rigidbody2D rig;
    //[Header("Other Settings")]
    /// <summary>
    /// Level Controller
    /// </summary>
    void Awake()
    {
        _playerInputs = new PlayerInputs();
        // Game logic here
        rig = GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {
        _playerInputs?.Enable();
        _moveSpeed = defaultMoveSpeed;
    }
    void Start()
    {
        _playerInputs.Player.Enable();
        _playerInputs.Player.Sprint.performed += ctx => isRunning = true;
        _playerInputs.Player.Sprint.canceled += ctx => isRunning = false;
        _playerInputs.Player.Interact.performed += ctx => Debug.Log("Interacting");
        // Initialization logic here
    }

    


    void Update()
    {
        // Game logic here
    }

    private void FixedUpdate()
    {
        movement = _playerInputs.Player.Move.ReadValue<Vector2>();
        if (isRunning)
        {
            _moveSpeed = defaultMoveSpeed * multiplier;
            // rig.linearVelocity = movement * _moveSpeed * multiplier;
        }
        else
        {
            _moveSpeed = defaultMoveSpeed;
           // rig.linearVelocity = movement * _moveSpeed;
        }
        rig.linearVelocity = movement * _moveSpeed * Time.fixedDeltaTime;

    }
    private void OnDestroy()
    {
        _playerInputs.Dispose();
        _playerInputs.Disable();
    }

    public void CanMove()
    {
        _moveSpeed = defaultMoveSpeed;
    }

    public void StopMoving()
    {
       
        _moveSpeed = 0;
        rig.linearVelocity = Vector2.zero;
    }
}
