using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalPlayerControl : MonoBehaviour
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

    Rigidbody rb;
    Animator animator;
    private bool isGrounded;
    float verticalVelocity;
    float JumpInitialVelocity;
    bool isJump;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
    private void OnDisable()
    {

    }
    private void Awake()
    {
        LoggerInstance = new Logger("PlayerControl");
        LoggerInstance.Enable();
        if (rb == null)
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

        
    }
    void HandleAnimation()
    {
        animator.SetFloat(AnimParams.Speed, rb.velocity.magnitude);
        if (isJump) animator.SetTrigger(AnimParams.Jump);
    }

    float JumpVelocityForHeight(float height, float timeToPeak, float gravity)
    {
        return (height + 0.5f * gravity * timeToPeak * timeToPeak) / timeToPeak;
    }
    float TargetAccelerationToForceMultiplier (float moveSpeed)
    {
        return 5f; //TO IMPLEMENT
    }
    bool CheckGrounded()
    {
        Vector3 rayOrigin = transform.position + new Vector3(0f,0.5f,0f);

        rayOrigin.y -= groundCheckOffset;

        return Physics.Raycast(rayOrigin, Vector3.down, groundCheckDistance, groundMask);
    }
}
