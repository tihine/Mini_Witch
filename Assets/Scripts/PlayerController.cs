using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";
    public float speed = 5f;
    float lookRotationSpeed = 8f;
    private Vector3 moveInput;
    private Rigidbody rb;
    PlayerControls controls;
    //NavMeshAgent agent;
    Animator animator;
    public InteractBehaviour interactBehaviour;

    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
        controls.Player.CollectItem.performed += OnCollectItem;
        controls.Player.Harvest.performed += OnHarvest;

    }
    void OnDisable()
    {
        controls.Disable();
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector3>();
    }
    private void OnCollectItem(InputAction.CallbackContext context)
    {
        interactBehaviour.CollectItem();
    }
    private void OnHarvest(InputAction.CallbackContext context)
    {
        interactBehaviour.Harvest();
    }

    private void FixedUpdate()
    {
        Vector3 movement = moveInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
        //SetAnimation();
    }
    /*void SetAnimation()
    {
        if (agent.velocity == Vector3.zero)
        {
            animator.Play(IDLE);
        }
        else
        {
            animator.Play(WALK);
        }
    }
    */
    

}
