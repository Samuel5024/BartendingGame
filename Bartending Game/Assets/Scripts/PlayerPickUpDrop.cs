using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    private ObjectGrabbable objectGrabbable;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(objectGrabbable == null) // Not carrying an object, try to grab
            {
                float pickupDistance = 2f;
                if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickUpLayerMask))
                {
                    Debug.Log(raycastHit.transform);
                    if(raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        Debug.Log(objectGrabbable);
                    }
                }
            }
            else // Currently carrying something, drop
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }
    }
}
