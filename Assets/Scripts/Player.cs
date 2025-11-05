using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    private CapsuleCollider2D mainCollider;
    private BoxCollider2D groundCheckCollider;
    public Animator animator { get; private set; }

    private InputAction moveAction;
    private InputAction jumpAction;

    public float moveSpeed { get; }
    [SerializeField] private float jumpForce;

    [SerializeField] private bool grounded;

    // Initial value considering the sprite starts out facing right
    [SerializeField] private int xDirection = 1;

    public StateMachine stateMachine { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        groundCheckCollider = GetComponentInChildren<BoxCollider2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        stateMachine = new StateMachine(this);
        stateMachine.Initialize(stateMachine.idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();

        // Vector2 moveVector = moveAction.ReadValue<Vector2>();
        // rb.linearVelocityX = moveVector.x * moveSpeed;

        // xDirection = Mathf.CeilToInt(moveVector.x);
        // animator.SetInteger("x-direction", xDirection);

        // if (moveVector.x < 0.0f)
        // {
            // Vector3 localScale = transform.localScale;
            // localScale.x = -1.0f;
            // transform.localScale = localScale;
        // }
        // else if (moveVector.x > 0.0f)
        // {
            // Vector3 localScale = transform.localScale;
            // localScale.x = 1.0f;
            // transform.localScale = localScale;
        // }

        // LayerMask mask = LayerMask.GetMask("Ground");
        // if (groundCheckCollider.IsTouchingLayers(mask))
        // {
            // animator.SetBool("grounded", true);
            // grounded = true;
        // } 
        // else
        // {
            // animator.SetBool("grounded", false);
            // grounded = false;
        // }

        // if (grounded && jumpAction.WasPressedThisFrame())
        // {
            // animator.SetTrigger("jump");
            // rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        // }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
