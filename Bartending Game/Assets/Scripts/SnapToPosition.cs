using UnityEngine;

public class SnapToPosition : MonoBehaviour
{
    public Vector3 snapPosition;
    private Quaternion originalRotationValue;
    float rotationResetSpeed = 1.0f;
    public bool canSnap = true;
    
    public ObjectGrabbable objectGrabbable;
    public PlayerPickUpDrop pickUpDrop;
    
    private void Start()
    {
        snapPosition = this.transform.position;
        originalRotationValue = transform.rotation;
    }



    private void SnapToStartingPosition()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && objectGrabbable != null && !canSnap) // if we click the LMB, not carrying an object & can't snap
        {
            pickUpDrop.Drop();
        }
        else
        {
            this.transform.position = snapPosition;
            transform.rotation = Quaternion.Slerp(transform.rotation. originalRotationValue, Time.time * rotationResetSpeed);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
