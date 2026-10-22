using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float crouchSpeed = 1.5f;
    public float crouchYScale;
    public float StartYScale;

    void Start()
    {
        StartYScale = transform.localScale.y;    
    }
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // transform.right takes player's direction & moves right
        // transform.forward takes player's driection & moves forward
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            controller.height = Mathf.Lerp(controller.height, 0.65f, Time.deltaTime * 3);
            controller.center = Vector3.Lerp(controller.center, new Vector3(0, .4f, 0), Time.deltaTime * 3);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, StartYScale, transform.localScale.z);
            controller.height = Mathf.Lerp(controller.height, 1.65f, Time.deltaTime * 6);
            controller.center = Vector3.Lerp(controller.center, new Vector3(0, 0, 0), Time.deltaTime * 6);
        }
    }

    
}
