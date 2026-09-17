using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            float pickupDistance = 2f;
            if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickUpLayerMask))
            {
                Debug.Log(raycastHit.transform);
                if(raycastHit.transform.TryGetComponent(out ObjectGrabbable objectGrabbable))
                {
                    objectGrabbable.Grab(objectGrabPointTransform);
                    Debug.Log(objectGrabbable);
                }
            }
        }
    }
}
