using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 300f;
    public Transform playerBody;
    private float xRotation = 0f;
    private MousePosition mouseTracker;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseTracker = Object.FindFirstObjectByType<MousePosition>();
    }

    void Update()
    {
        if (mouseTracker == null)
        {
            mouseTracker = Object.FindFirstObjectByType<MousePosition>();
        }

        if(mouseTracker != null && mouseTracker.IsHoldingObject)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limit camera roatation

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);  // Use the mouse X rotation to look left to right
    }
}
