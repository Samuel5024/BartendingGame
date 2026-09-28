using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController instance; // Singleton
    [SerializeField] TMP_Text interactionText;
    
    private void Awake()
    {
        instance = this;
    }

    public void EnableInteractionText(string text)
    {
        interactionText.text = text + " (LMB)";
        interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
}
