using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable currentInteractable;

    void Update()
    {
        CheckInteraction();
        if(Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        if(Physics.Raycast(ray, out hit, playerReach)) // If collides with anything within player reach
        {
            if(hit.collider.tag == "Interactable") // If looking at an interactable object
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                if(currentInteractable && newInteractable != currentInteractable) // If there is a currentInteractable & is not the newInteractable
                {
                    currentInteractable.DisableOutline();
                }
                if(newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else // If new interactable is not valid
                {
                    DisableCurrentInteractable();
                }
            }
            else // If not an interactable
            {
                DisableCurrentInteractable();
            }
        }
        else // If nothing in reach
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
    }

    void DisableCurrentInteractable()
    {
        if(currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}
