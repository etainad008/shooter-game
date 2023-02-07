using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float climbingSpeed;
    
    [Header("Health")]
    public float playerHealth;

    [Header("Movement")]
    public float playerSpeed;

    public float dragForce;

    [Header("Grounded Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    
    [Header("Jump")]
    public float jumpCooldown;
    public float jumpForce;
    public float airResistance;
    bool readyToJump = true;
    readonly KeyCode jump = KeyCode.Space;


    public Transform orientation;
    Rigidbody rb;

    Vector3 moveDirection;

    float HorizontalInput;
    float VercitalInput;

    [Header("Events")]
    public GameEvent OnPlayerAwake;
    public GameEvent OnPlayerTakeDamage;
    public GameEvent OnPlayerDeath;

    private void Awake()
    {
        OnPlayerAwake.Raise(playerHealth);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight*0.5f + 0.5f, whatIsGround);

        //handle drag
        if (grounded)
        {
            rb.drag = dragForce;
        }

        else
        {
            rb.drag = 0;
        }
        
        MyInput();

        if (playerHealth <= 0)
        {
            OnPlayerDeath.Raise();
            //player death animation
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            int d = other.gameObject.GetComponent<Enemy>().enemyDamage;
            playerHealth -= d;
            OnPlayerTakeDamage.Raise(this, playerHealth);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            if (Input.GetKey(KeyCode.W))
            {

                rb.velocity = Vector3.zero;
                transform.position += Vector3.up * climbingSpeed * Time.deltaTime;
            }
        }
    }

    void MyInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VercitalInput = Input.GetAxisRaw("Vertical");

        //if jump
        if(Input.GetKeyDown(jump) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();

            // Reset Jump
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * VercitalInput + orientation.right * HorizontalInput;

        rb.AddForce(moveDirection.normalized * playerSpeed*10f, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 surfaceVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //Limit velocity if needed
        if(surfaceVelocity.magnitude > playerSpeed)
        {
            Vector3 limitedVelocity = surfaceVelocity.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    void Jump()
    {
        //Reset yVelocity so jump height is constant
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        // Add force jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;  
    }
}
