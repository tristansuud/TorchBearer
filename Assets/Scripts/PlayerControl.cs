using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
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
    [SerializeField] float RotationSpeed;
    [SerializeField] float Acceleration;
    [SerializeField] float Gravity = -9.8f;
    [SerializeField] float PushForceMultiplier;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckOffset = 0.1f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [Header("External")]
    [SerializeField] PushAbility pushChecker;

    CharacterController controller;
    Animator animator;
    
    private bool isGrounded;
    float verticalVelocity;
    float JumpInitialVelocity;
    bool isJump;

    public bool CanMove = true;
    public InteractTrigger currentTrigger;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void Awake()
    {
        CanMove = true;
        LoggerInstance = new Logger("PlayerControl");
        LoggerInstance.Enable();
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        if (controller == null)
        {
            LoggerInstance.Error($"{nameof(controller)} is required on {gameObject.name}");
            enabled = false;
        }
        if (animator == null)
        {
            LoggerInstance.Error($"{nameof(animator)} is required on {gameObject.name}");
            enabled = false;
        }
        if (pushChecker == null)
        {
            LoggerInstance.Error($"{nameof(pushChecker)} is required on {gameObject.name}");
            enabled = false;
        }
        JumpInitialVelocity = JumpVelocityForHeight(JumpHeight, JumpTimeToPeak, Gravity);
    }
    // Start is called before the first frame update
    void Start()
    {
        //JumpInitialVelocity = JumpVelocityForHeight(JumpHeight, JumpTimeToPeak, Gravity);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        isJump = Input.GetKeyDown(JumpKey);

        if (MovementPredicate()) HandleMovement(x, z, isJump);
        if (MovementPredicate()) HandlePushing(x, z);
        HandleAnimation();
        HandleInteraction();


    }

    bool MovementPredicate()
    {
        return CanMove;
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
            verticalVelocity = -0.1f; // reset when touching ground
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
                RotationSpeed
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
    void HandlePushing(float x, float z)
    {
        if (pushChecker == null) return;
        Rigidbody pushTarget = pushChecker.getPushTarget();
        
        if (pushTarget == null) return;
        pushTarget.AddForce(new Vector3(x,0,z).normalized * PushForceMultiplier * Time.deltaTime);
    }
    void HandleInteraction()
    {
        if (currentTrigger == null) return;
        if (Input.GetKeyDown(InteractKey))
        {
            EventBus.Raise(new GameEvents.TriggerInteracted()); // for sounds, ui and other stuff
            currentTrigger.DoTrigger();
        }
    }
    public void SetInteractTrigger(InteractTrigger trigger)
    {
        EventBus.Raise(new GameEvents.TriggerSelected()); // to show UI, set glow on objects, dont forget to nullcheck
        currentTrigger = trigger;
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
