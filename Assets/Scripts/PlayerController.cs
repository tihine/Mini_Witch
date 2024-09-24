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
    private Vector3 moveInput;
    private Rigidbody rb;
    private GameObject collectibleItem;
    PlayerControls controls;
    //NavMeshAgent agent;
    Animator animator;

    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 8f;

    public Inventory inventory;

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
        controls.Player.CollectItem.performed += ctx => CollectItem();
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
    private void CollectItem()
    {
        if (collectibleItem != null)
        {
            Debug.Log("item" + collectibleItem.name);
            inventory.content.Add(collectibleItem.GetComponent<Item>().item);
            Destroy(collectibleItem);
        }
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            collectibleItem = other.gameObject;
            Debug.Log("dans la zone");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            if (collectibleItem == other.gameObject)
            {
                collectibleItem = null;
                Debug.Log("hors zone");
            }
        }
    }

}
