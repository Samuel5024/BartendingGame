using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] PlayerPickUpDrop pickUpDrop;
    public float playerReach = 3f;
    private Interactable currentInteractable;
    private bool isTrackingHeldObject = false; // state tracker that forces UI changes when switching from floor to hand

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
                if(heldInteractable != currentInteractable || !isTrackingHeldObject) // If brand new object, set it up
                {
                    if(currentInteractable == null) // Moved the "else" return condition to inside the inverted if statement
                    {
                        return;
                    }
                    
                    currentInteractable.DisableOutline();
                    isTrackingHeldObject = true; // Lock into held UI state
                    string fullText = heldInteractable.heldMessage + " (F)"; // display the heldMessage
                    SetNewCurrentInteractable(heldInteractable, fullText);
                }            
            }
        }

        // Case 2: Empty hands & looking for a ground object
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        if(Physics.Raycast(ray, out hit, playerReach))
        {
            if(hit.collider.CompareTag("Interactable"))
            {
                Interactable groundInteractable = hit.collider.GetComponent<Interactable>();

                if(groundInteractable != null && groundInteractable.enabled) 
                {
                    if(groundInteractable != currentInteractable || isTrackingHeldObject) // Reset state if it's a new target or if we're holding something
                    {
                        if(currentInteractable == null)
                        {
                            return; // Only exit if a valid active component exists!
                        }
                        currentInteractable.DisableOutline();
                        isTrackingHeldObject = false; // Set to ground UI state
                        string fullText = groundInteractable.groundMessage + " (LMB)";
                        SetNewCurrentInteractable(groundInteractable, fullText);
                    }
                }
            }
        }        
        DisableCurrentInteractable(); // Now safely disables outlines/UI if you look away or hit a bad object
    }

    void SetNewCurrentInteractable(Interactable newInteractable, string formattedText)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HUDController.instance.EnableInteractionText(formattedText);
    }

    void DisableCurrentInteractable()
    {
        if(HUDController.instance != null)
        {
            HUDController.instance.DisableInteractionText();
        }
        if(currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}