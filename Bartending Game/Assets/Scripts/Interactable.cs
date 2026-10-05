using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private MousePosition mouseTracker;
    Outline outline;
    private Rigidbody rb;
    private bool isBeingDragged = false;
    public string groundMessage = "Pick up";
    public string heldMessage = "Interact";
    public UnityEvent onInteraction;


    void Start()
    {
        outline = GetComponent<Outline>();
        rb = GetComponent<Rigidbody>();
        DisableOutline();
    }

    public void Interact()
    {
        mouseTracker = Object.FindFirstObjectByType<MousePosition>();

        if (mouseTracker == null || rb == null)
        {
            Debug.LogError("MousePosition.cs is missing from the scene!");
            Debug.LogError($"{gameObject.name} is missing a Rigidbody component!");
            return;
        }

        if (!isBeingDragged)
        {
            isBeingDragged = true;
            mouseTracker.StartDragging(rb);
        }
        else
        {
            isBeingDragged = false;
            mouseTracker.StopDragging();
        }
    }

    public void DisableOutline()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    public void EnableOutline()
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
    }


}
