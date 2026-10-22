using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 12f;
    public float speed = 12f;
    public float crouchSpeed = 1.5f;
    public bool crouching = false;
    public bool toggleCrouch = false;

    // Update is called once per frame
    void Update()
    {
        CrouchToggle();
        Crouch();
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // transform.right takes player's direction & moves right
        // transform.forward takes player's driection & moves forward
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    public void CrouchToggle()
    {
        if (toggleCrouch && Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = !crouching;
        }
        else
        {
            // hold ctrl to crouch
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                crouching = true;
            }
            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                crouching = false;
            }

        }
    }

    public void Crouch()
    {
        if (crouching)
        {
            controller.height = Mathf.Lerp(controller.height, 0.65f, Time.deltaTime * 3);
            controller.center = Vector3.Lerp(controller.center, new Vector3(0, 0.4f, 0), Time.deltaTime * 3);

            if (speed > crouchSpeed)
            {
                speed = crouchSpeed;
            }
        }
        else
        {
            controller.height = Mathf.Lerp(controller.height, 1.65f, Time.deltaTime * 6);
            controller.center = Vector3.Lerp(controller.center, new Vector3(0, 0, 0), Time.deltaTime * 6);
            speed = walkSpeed;
        }
    }
}
