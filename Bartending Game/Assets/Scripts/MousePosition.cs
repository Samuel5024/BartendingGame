using UnityEngine;
using System.Collections;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float dragSpeed = 15f;

    private Outline currentHoveredOutline;  // Track the outline component we're currently hovering over 
    private Rigidbody draggedRigidbody;
    private Vector3 mOffset; // Difference b/w world position of the gameObject & cursor
    private Vector3 targetPhysicsPosition; // Where the object should be with the mouse
    private float mZCoord; // How far the gameObject is from your screen 


    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Camera cam = mainCamera != null ? mainCamera : Camera.main; // Use primary main camera in case not assinged
        if (cam == null)
        {
            return;
        }

        if (draggedRigidbody != null)
        {
            ClearHoveredOutline();
            targetPhysicsPosition = GetMouseAsWorldPoint(cam) + mOffset; // Calculate target position
            transform.position = targetPhysicsPosition;
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
            Outline hitOutline = raycastHit.collider.GetComponent<Outline>(); // Check if object has Outline component

            if (currentHoveredOutline != null) // Clear old outline
            {
                currentHoveredOutline.enabled = false;
            }
            currentHoveredOutline = hitOutline;
            if (currentHoveredOutline != null)
            {
                currentHoveredOutline.enabled = true;
            }
        }
        else
        {
            ClearHoveredOutline();
        }
    }

    private void FixedUpdate()
    {
        if (dragged)
    }

    private Vector3 GetMouseAsWorldPoint(Camera cam)
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord; // z coordinate of game object on screen
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void StartDragging(Rigidbody rbToDrag)
    {
        draggedRigidbody = rbToDrag;
        draggedRigidbody.useGravity = false
        Camera cam = mainCamera != null ? mainCamera : Camera.main;
    }

    private void ClearHoveredOutline()
    {
        if (currentHoveredOutline != null)
        {
            currentHoveredOutline.enabled = false;
            currentHoveredOutline = null;
        }
    }
}
