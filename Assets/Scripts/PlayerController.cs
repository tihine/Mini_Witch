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
    private GameObject collectibleItem;
    PlayerControls controls;
    //NavMeshAgent agent;
    Animator animator;

    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;
    [SerializeField] public Inventory inventory;
    [SerializeField] private GameObject pickupText;

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
        if (inventory.IsFull())
        {
            Debug.Log("Inventory is full");
            return;
        }
        if (collectibleItem != null)
        {
            Debug.Log("item" + collectibleItem.name);
            inventory.AddItem(collectibleItem.GetComponent<Item>().item);
            pickupText.SetActive(false);
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
        //look at use of Layer masks ?
        if (other.CompareTag("Item"))
        {
            collectibleItem = other.gameObject;
            pickupText.SetActive(true);
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
                pickupText.SetActive(false);
                Debug.Log("hors zone");
            }
        }
    }

}
