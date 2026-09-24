using UnityEngine;


public class ObjectOutline : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameObject[] grabbableObjects = GameObject.FindGameObjectsWithTag("Grabbable");
        foreach(GameObject obj in grabbableObjects)
        {
            Outline outline = obj.AddComponent<Outline>();
            outline.enabled = false;
            outline.OutlineColor = new Color (1f, 1f, 1f);
            outline.OutlineWidth = 4f;
            
            if(obj.GetComponent<OutlineSelect>() == null)
            {
                obj.AddComponent<OutlineSelect>();
            }
            if(obj.GetComponent<BoxCollider>() == null)
            {
                obj.AddComponent<BoxCollider>();
            }
        }   
    }
        
}
