using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] PlayerPickUpDrop pickUpDrop;
    private Interactable currentInteractable;
    public float playerReach = 3f;

    void Start()
    {
        if(pickUpDrop == null)
        {
            pickUpDrop = GetComponent<PlayerPickUpDrop>();
        }
    }
 
    void Update()
    {
        CheckInteraction();
        if(Input.GetKeyDown(KeyCode.F) && currentInteractable != null) 
        {
            currentInteractable.Interact();
            pickUpDrop.objectGrabbable = null; // Clear reference since object despawns on Interact
            DisableCurrentInteractable();
        }
    }

    void CheckInteraction()
    {
        if(pickUpDrop != null && pickUpDrop.objectGrabbable != null) // Are we holding an object?
        {
            Interactable heldInteractable = pickUpDrop.objectGrabbable.GetComponent<Interactable>(); // Get Interactable component from held object

            if(heldInteractable != null && heldInteractable.enabled)
            {
                if(heldInteractable != currentInteractable) // If brand new object, set it up
                {
                    if(currentInteractable != null)
                    {
                        currentInteractable.DisableOutline();
                    }
                    string fullText = heldInteractable.heldMessage + " (F)"; // display the heldMessage
                    SetNewCurrentInteractable(heldInteractable, fullText);
                }
                return;            
            }
        }

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        if(Physics.Raycast(ray, out hit, playerReach))
        {
            if(hit.collider.tag == "Interactable") // If looking at an interactable object
            {
                Interactable groundInteractable = hit.collider.GetComponent<Interactable>();

                if(groundInteractable && groundInteractable != currentInteractable.enabled)
                {
                    if(groundInteractable != currentInteractable)
                    {
                        if(currentInteractable != null)
                        {
                            currentInteractable.DisableOutline();
                        }
                        string fullText = groundInteractable.groundMessage + " (LMB)";
                        SetNewCurrentInteractable(groundInteractable, fullText);
                    }
                    return;
                }
            }
        }        
        DisableCurrentInteractable(); // Disable outlines if we aren't holding an interactable object
    }

    void SetNewCurrentInteractable(Interactable newInteractable, string formattedText)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HUDController.instance.EnableInteractionText(formattedText);
    }

    void DisableCurrentInteractable()
    {
        HUDController.instance.DisableInteractionText();
        if(currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}
