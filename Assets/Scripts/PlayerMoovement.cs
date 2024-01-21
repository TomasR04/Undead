using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoovement : MonoBehaviour
{
    private Animator animator;
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -10f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3f;
    public Camera camera;
    public Transform firstPersonCam;
    public GameObject head;
    public LayerMask layer;
    public bool canWalk;

    void Start()
    {
        animator = GetComponent<Animator>();
        canWalk = true;

    }
    void Update()
    {
        if (animator.GetBool("IsRunning") && animator.GetBool("HasRifle"))
        {
            firstPersonCam.localPosition = new Vector3(0f, 1.542f, 0.42f);
        }
        else
        {
            if (!animator.GetBool("IsAiming"))
            {
                firstPersonCam.localPosition = new Vector3(0f, 1.658f, 0.255f);
            }
            
        }
        if (Input.GetKeyUp(KeyCode.W)&&animator.GetBool("IsWalking"))
        {
            animator.Play("Unarmed Idle");
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (move != Vector3.zero)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {

                animator.SetBool("IsRunning", true);
                animator.SetBool("IsWalking", false);
            }
            else
            {
                if (!(animator.GetBool("IsRunning")))
                {
                    animator.SetBool("IsWalking", true);
                }

                //animator.SetBool("IsRunningUnarmed", false);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                animator.SetBool("IsRunning", false);
            }
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsWalking", false);

        }
        RaycastHit hit;
        if (!Physics.Raycast(firstPersonCam.position, firstPersonCam.forward, 0.5f, layer) && canWalk)
        {

            if (animator.GetBool("IsRunning"))
            {
                controller.Move(move * (speed * 2) * Time.deltaTime);
            }
            else
            {
                controller.Move(move * speed * Time.deltaTime);
            }
        }
        else
        {
           transform.position = Vector3.zero;
        }




        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
