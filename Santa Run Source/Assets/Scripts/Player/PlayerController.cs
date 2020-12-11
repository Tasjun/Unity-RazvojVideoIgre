using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject shieldObject;
    [SerializeField]
    private int laneSwitchSpeed = 25;

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; // 0:left 1:middle 2:right
    public float laneDistance = 4; //the distance between two lanes

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;
    private bool isSliding = false;

    private Player player;

    void Start()
    {
        player = new Player();
        player.PlayerSpeed = forwardSpeed;

        controller = GetComponent<CharacterController>();
        player.pController = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if game should play
        if (!PlayerManager.isGameStarted)
            return;

        animator.SetBool("isGameStarted", true);

        player.Tick(Time.deltaTime);
        HandleInput();
        Move();

        //isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        //animator.SetBool("isGrounded", isGrounded);

        animator.SetBool("isGrounded", controller.isGrounded);

        // TODO maybe optimize this to be handled through event instead wasting processor time in update
        if (player.IsShielded)
        {
            shieldObject.SetActive(true);
        }
        else
        {
            shieldObject.SetActive(false);
        }

        //Increase speed
        if (forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;
    }

    private void Move()
    {
        direction.z = player.PlayerSpeed;

        if (!controller.isGrounded)
        {
            direction.y += Gravity * Time.deltaTime;
        }

        // Calculate where we should be in the future

        // Initialize to current location
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        // Time to swtich lanes
        if (transform.position != targetPosition)
        {
            Vector3 targetDir = targetPosition - transform.position;
            Vector3 moveDir = targetDir.normalized * laneSwitchSpeed * Time.deltaTime;

            if (moveDir.sqrMagnitude < targetDir.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(targetDir);
        }

        controller.Move(direction * Time.deltaTime);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
        {
            StartCoroutine(Slide());
        }

        // Gather the inputs on which lane we shoud be
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (controller.isGrounded)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }

        if (other.transform.tag == "Pickup")
        {
            Debug.Log("Hit a pickup object");

            Pickup pickup = other.gameObject.GetComponent<Pickup>();
            pickup.GetEffect().ApplyEffect(player);

            Destroy(other.gameObject);
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;

        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.1f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;

        animator.SetBool("isSliding", false);

        isSliding = false;
    }
}
