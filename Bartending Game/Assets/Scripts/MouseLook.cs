using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 3f;
    public float sensitivityY = 3f;
    public float minimumX = -360f;
    public float maximumX = 360f;
    public float minimumY = -85f;
    public float maximumY = 90f;
    private float rotationX = 0f;
    private float rotationY = 0f;
    Quaternion originalRotation;
    public bool lockCursor = true;
    public bool working = true;
    public bool cutSceneMode = false; //used in the reference code for the camera logic

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <=360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }

    void Start()
    {
        originalRotation = transform.localRotation;
        if (lockCursor)
        {
            #if UNITY_STANDALONE
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            #endif
            #if UNITY_EDITOR
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            #endif
        }
    }

    void LateUpdate()
    {
        if (lockCursor)
        {
            #if UNITY_STANDALONE
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            #endif
            #if UNITY_EDITOR
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            #endif
        }

        if (working)
        {
            if (cutSceneMode)
            {
                rotationX += Input.GetAxis("Mouse X") * sensitivityX * 0.5f;
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY * 0.5f;
                rotationX = Mathf.Lerp(rotationX, transform.root.localRotation.x, Time.deltaTime * 3);
                rotationY = Mathf.Lerp(rotationY, transform.root.localRotation.y, Time.deltaTime * 3);

                rotationX = ClampAngle(rotationY, minimumX * 0.8f, maximumX * 0.8f);
                rotationY = ClampAngle(rotationY, minimumY * 0.8f, maximumY * 0.8f);
                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
            }
            else if (!cutSceneMode)
            {
                if (axes == RotationAxes.MouseXAndY)
                {
                    // Read the mouse input axis
                    rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

                    rotationX = ClampAngle(rotationX, minimumX, maximumX);
                    rotationY = ClampAngle(rotationY, minimumY, maximumY);
                    Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                    Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                    transform.localRotation = originalRotation * xQuaternion * yQuaternion;
                }
                else if (axes == RotationAxes.MouseX)
                {
                    rotationX += Input.GetAxis("Mouse X") * sensitivityX;
                    
                    rotationX = ClampAngle(rotationX, minimumX, maximumX);
                    Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                    transform.localRotation = originalRotation * xQuaternion;
                }
                else
                {
                    rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    
                    rotationY = ClampAngle(rotationY, minimumY, maximumY);
                    Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                    transform.localRotation = originalRotation * yQuaternion;
                }
            }
        }
    }
}
