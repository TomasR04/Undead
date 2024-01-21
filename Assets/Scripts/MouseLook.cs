using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MouseLook : MonoBehaviour
{
    public float bodyRotationSpeed = 100f;
    public float headRotationSpeed = 100f;
    public float bodyVerticalLimit = 90f;  
    public float headVerticalLimit = 90f;  
    public Transform playerBody;
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;

    float xRotation = 0f;

    public Animator animator;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = playerBody.GetComponent<Animator>();
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            float mouseX = Input.GetAxis("Mouse X") * bodyRotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * headRotationSpeed * Time.deltaTime;

            
            if (animator.GetBool("IsAiming"))
            {
                //RotatePlayerBody(mouseY);
                playerBody.Rotate(Vector3.up * mouseX);
                playerBody.Rotate(Vector3.left * mouseY);
            }
            else
            {


                if (playerBody.eulerAngles.x != 0 || playerBody.eulerAngles.z != 0)
                {
                    playerBody.eulerAngles = new Vector3(0, playerBody.eulerAngles.y, 0);
                    //playerBody.eulerAngles = new Vector3(0, 0, 0);
                }
                playerBody.Rotate(Vector3.up * mouseX);

                RotateCamera(thirdPersonCamera, mouseY);
                RotateCamera(firstPersonCamera, mouseY);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                SwitchCams();
            }
        }
    }

    private void SwitchCams()
    {
        thirdPersonCamera.Priority = thirdPersonCamera.Priority == 0 ? 1 : 0;
        firstPersonCamera.Priority = firstPersonCamera.Priority == 0 ? 1 : 0;
    }

    private void RotateCamera(CinemachineVirtualCamera virtualCamera, float mouseY)
    {
        Transform cameraTransform = virtualCamera.transform;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -headVerticalLimit, headVerticalLimit);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    void RotatePlayerBody(float mouseY)
    {
        
        // Get camera rotation
        Quaternion cameraRotation = firstPersonCamera.transform.rotation;

        // Convert camera rotation to Euler angles
        Vector3 cameraEulerAngles = cameraRotation.eulerAngles;

        // Modify player's rotation based on camera's Y-axis and mouse input
        playerBody.rotation = Quaternion.Euler(0, cameraEulerAngles.y + mouseY, 0);
        /*Vector3 cameraForward = firstPersonCamera.transform.forward;
        Vector3 cameraRight = firstPersonCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Quaternion verticalRotation = Quaternion.AngleAxis(-mouseY, cameraRight);

        playerBody.rotation *= verticalRotation;*/
    }
}