using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]PlayerPickUpDrop pickUpDrop;
    private Interactable currentInteractable;

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
                    SetNewCurrentInteractable(heldInteractable);
                }
                return;            
            }
        }
        DisableCurrentInteractable(); // Disable outlines if we aren't holding an interactable object
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HUDController.instance.EnableInteractionText(currentInteractable.message);
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
