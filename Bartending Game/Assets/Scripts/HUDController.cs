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

    private void Start()
    {
        DisableInteractionText();
    }

    public void EnableInteractionText(string text)
    {
        if(interactionText == null)
        {
            return;
        }
        interactionText.text = text;
        interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
}
