using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerControl : MonoBehaviour
{
    private Logger LoggerInstance;

    [Header("Controls")]
    [SerializeField] KeyCode ForwardKey;
    [SerializeField] KeyCode BackwardKey;
    [SerializeField] KeyCode LeftKey;
    [SerializeField] KeyCode RightKey;
    [SerializeField] KeyCode JumpKey;
    [SerializeField] KeyCode InteractKey;
    [Header("Kinetics")]
    [SerializeField] float moveSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float JumpHeight;
    [SerializeField] float JumpTimeToPeak;
    [SerializeField] float rotationSpeed;
    [SerializeField] float Acceleration;
    [SerializeField] float Gravity = -9.8f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckOffset = 0.1f;
    [SerializeField] private float groundCheckDistance = 0.1f;

    CharacterController controller;
    Animator animator;
    private bool isGrounded;
    float verticalVelocity;
    float JumpInitialVelocity;
    bool isJump;

    private void OnEnable()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    private void OnDisable()
    {
        
    }
    private void Awake()
    {
        LoggerInstance = new Logger("PlayerControl");
        LoggerInstance.Enable();
        if (controller == null)
        {
            //throw new 
        }
        if (animator == null)
        {

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //JumpInitialVelocity = JumpVelocityForHeight(JumpHeight, JumpTimeToPeak, Gravity);
    }

    // Update is called once per frame
    void Update()
    {
        JumpInitialVelocity = JumpVelocityForHeight(JumpHeight, JumpTimeToPeak, Gravity);
        isGrounded = CheckGrounded();
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        isJump = Input.GetKeyDown(JumpKey);
        HandleMovement(x, z, isJump);
        HandleAnimation();
    }

    void HandleMovement(float x, float z, bool isJump)
    {
        float horizontal = x;  // A-D
        float vertical = z;       // W-S
        
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection.Normalize();

        if (isJump && isGrounded)
        {
            verticalVelocity = JumpInitialVelocity;
            
        } else if (isGrounded)
        {
            verticalVelocity = 0f; // reset when touching ground
        }
        else
        {
            verticalVelocity += Gravity * Time.deltaTime;
        }

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed
            );
        }
        
        moveDirection *= moveSpeed;
        moveDirection.y = verticalVelocity;
        controller.Move(moveDirection * Time.deltaTime);
    }
    void HandleAnimation()
    {
        animator.SetFloat(AnimParams.Speed, controller.velocity.magnitude);
        if (isJump) animator.SetTrigger(AnimParams.Jump);
    }

    float JumpVelocityForHeight(float height, float timeToPeak, float gravity)
    {
        return (height + 0.5f * gravity * timeToPeak * timeToPeak) / timeToPeak;
    }
    bool CheckGrounded()
    {
        Vector3 rayOrigin = transform.position + controller.center;

        rayOrigin.y -= controller.height * 0.5f - controller.radius + groundCheckOffset;

        return Physics.Raycast(rayOrigin, Vector3.down, groundCheckDistance, groundMask);
    }
}
