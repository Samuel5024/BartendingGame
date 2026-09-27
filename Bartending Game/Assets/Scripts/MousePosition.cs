using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private Outline currentHoveredOutline;  // Track the outline component we're currently hovering over 

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Camera cam = mainCamera != null ? mainCamera : Camera.main; // Use primary main camera in case not assinged
        if(cam == null)
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
            Outline hitOutline = raycastHit.collider.GetComponent<Outline>(); // Check if object has Outline component

            if(currentHoveredOutline != null) // Clear old outline
            {
                currentHoveredOutline.enabled = false;
            }
            currentHoveredOutline = hitOutline;
            if(currentHoveredOutline != null)
            {
                currentHoveredOutline.enabled = true;
            }
        }
        else
        {
            if(currentHoveredOutline != null)
            {
                currentHoveredOutline.enabled = false;
                currentHoveredOutline = null;
            }
            
        }
    }
}
