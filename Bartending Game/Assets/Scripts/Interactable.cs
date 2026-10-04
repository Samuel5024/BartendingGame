using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string groundMessage = "Pick up";
    public string heldMessage = "Interact";
    public UnityEvent onInteraction;

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        Debug.Log("test");
    }

    public void DisableOutline()
    {
        if(outline != null)
        {
            outline.enabled = false;
        }
    }

    public void EnableOutline()
    {
        if(outline != null)
        {
            outline.enabled = true;
        }
    }


}
