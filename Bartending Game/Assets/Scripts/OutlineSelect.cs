using UnityEngine;

public class OutlineSelect : MonoBehaviour
{
    private Outline outlineComp;
    
    private void Start()
    {
        outlineComp = GetComponent<Outline>();    
    }

    private void OnMouseEnter()
    {
        if(outlineComp == null)
        {
            outlineComp = GetComponent<Outline>();
        }
        if(outlineComp != null)
        {
            outlineComp.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        if(outlineComp != null)
        {
            outlineComp.enabled = false;
        }
    }
}
