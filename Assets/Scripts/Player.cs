using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    [SerializeField]
    private CapsuleCollider2D mainCollider;

    public ContactFilter2D contactFilter;

    public bool isGrounded;

    [SerializeField]
    public Animator animator { get; private set; }

    [SerializeField]
    public StateMachine stateMachine { get; private set; }


    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    public float jumpForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        mainCollider = GetComponent<CapsuleCollider2D>();
        
        stateMachine = new StateMachine(this);
        stateMachine.Initialize(stateMachine.idleState);
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        stateMachine.Update();
    }

    void GroundCheck()
    {
        isGrounded = rb.IsTouching(contactFilter);
    }
}
