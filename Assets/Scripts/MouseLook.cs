using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensetivity = 100f;
    public Transform playerBody;
    public GameObject thirdPerson;
    public GameObject firstPerson;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
            if (Input.GetKeyDown(KeyCode.H))
            {
                SwitchCams();
            }
        }
        
    }
    private void SwitchCams()
    {
        if (thirdPerson.active)
        {
            firstPerson.SetActive(true);
            thirdPerson.SetActive(false);
        }
        else
        {
            thirdPerson.SetActive(true);
            firstPerson.SetActive(false);
        }
    }
}
