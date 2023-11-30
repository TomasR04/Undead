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
        if (animator.GetBool("IsRunningUnarmed"))
        {
            camera.transform.parent = head.transform;
            camera.transform.localPosition = new Vector3(-3.778934e-05f, 0, 0.158994f);
        }
        else if (animator.GetBool("IsWalkingUnarmed"))
        {
            camera.transform.parent = gameObject.transform;
            camera.transform.localPosition = new Vector3(-3.780331e-05f, 1.663589f, 0.2768766f);
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

                animator.SetBool("IsRunningUnarmed", true);
                animator.SetBool("IsWalkingUnarmed", false);
            }
            else
            {
                if (!(animator.GetBool("IsRunningUnarmed")))
                {
                    animator.SetBool("IsWalkingUnarmed", true);
                }

                //animator.SetBool("IsRunningUnarmed", false);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                animator.SetBool("IsRunningUnarmed", false);
            }
        }
        else
        {
            animator.SetBool("IsRunningUnarmed", false);
            animator.SetBool("IsWalkingUnarmed", false);

        }
        RaycastHit hit;
        if (!Physics.Raycast(head.transform.position, head.transform.forward, 0.5f, layer) && canWalk)
        {

            if (animator.GetBool("IsRunningUnarmed"))
            {
                controller.Move(move * (speed * 2) * Time.deltaTime);
            }
            else
            {
                controller.Move(move * speed * Time.deltaTime);
            }
        }




        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
