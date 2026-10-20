using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public bool beingCarried = false;
    private Rigidbody rb;
    public Transform HoldPos;
    public float throwSpeed = 200;
    public Renderer rend;
    private Color originColor;
    private bool over = false;
    public Color targetColor = Color.yellow;
    private GameObject MainCam;
    private Interact interactable;
    public string[] prompts = { " Grab Object", "" };
    private bool touched = false;
    private bool rotating = false;
    private Quaternion rotateBy;
    public MouseLook[] mouseLook;
    private Vector3 originPos;
    private Quaternion originRot;
    public bool canReturn = true;
    private bool objectReset = true;
    private float distToOrigin;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        MainCam = GameObject.FindWithTag("MainCamera");
        if (MainCam == null)
        {
            MainCam = GameObject.FindObjectOfType<Camera>().gameObject;
        }
        //interactable = MainCam.GetComponent<Interact>();
        originColor = rend.material.color;
        StartCoroutine(SetOriginTrans());
    }

    public void Hovering()
    {
        over = true;
        StartCoroutine(Fadeout());
        if (!beingCarried)
        {
            //interactable.message = prompts[0];
        }
    }

    public void Interacting()
    {
        beingCarried = !beingCarried;
        if (!beingCarried)
        {
            rb.isKinematic = false;
            transform.parent = null;
            rb.AddForce(MainCam.transform.forward * throwSpeed);
            rotating = false;
            //for (int i = 0; i < mouseLook.Length; i++) 
            //{
            //    mouseLook[i].working = true;   
            //}
            touched = false;
        }
        else if (beingCarried)
        {
            rb.isKinematic = true;
            transform.position = HoldPos.position;
            transform.parent = MainCam.transform;
            objectReset = false;
        }
        //interactable.message = prompts[1];
        //interactable.CrosshairUI.SetActive(false);
    }

    public void RelativeRotate(float rotateLeftRight, float rotateUpDown)
    {
        float sensitivity = 5f;
        Vector3 relativeUp = MainCam.transform.TransformDirection(Vector3.up); //Gets the world vector space for cameras up vector
        Vector3 relativeRight = MainCam.transform.TransformDirection(Vector3.right); //Gets the world vector space for cameras right vector
        Vector3 objectRelativeUp = transform.InverseTransformDirection(relativeUp); //Turns relativeUp vector from world to objects local space
        Vector3 objectRelativeRight = transform.InverseTransformDirection(relativeRight); //Turns relativeRight vector from world to objects local space

        rotateBy = Quaternion.AngleAxis(rotateLeftRight / gameObject.transform.localScale.x * sensitivity, objectRelativeUp)
            * Quaternion.AngleAxis(-rotateUpDown / gameObject.transform.localScale.x * sensitivity, objectRelativeRight);
    }

    void FixedUpdate()
    {
        if (rotating)
        {
            gameObject.transform.localRotation *= rotateBy;
        }

        if (!beingCarried)
        {
            if (over)
            {
                rend.material.color = Color.Lerp(rend.material.color, targetColor, Time.deltaTime * 4);
            }
            else if (!over)
            {
                rend.material.color = Color.Lerp(rend.material.color, originColor, Time.deltaTime * 4);
            }
        }
        else
        {
            rend.material.color = originColor;
        }

        if (touched)
        {
            rb.isKinematic = false;
            transform.parent = null;
            beingCarried = false;
            rotating = false;
            for (int i = 0; i < mouseLook.Length; i++)
            {
                mouseLook[i].working = true;
            }
            touched = false;
        }
    }

    void Update()
    {
        if (beingCarried)
        {
            if (Input.GetButtonDown("Squint") && !rotating)
            {
                for (int i = 0; i < mouseLook.Length; i++)
                {
                    mouseLook[i].working = false;
                }
                rotating = true;
            }
            if (Input.GetButton("Squint"))
            {
                RelativeRotate(-Input.GetAxis("Mouse X") * 5, -Input.GetAxis("Mouse Y") * 5);
            }
            if(Input.GetButtonUp("Squint") && rotating)
            {
                for (int i = 0; i < mouseLook.Length; i++)
                {
                    mouseLook[i].working = true;
                }
                rotating = false;
            }
        }
        else
        {
            if (canReturn)
            {
                if (!objectReset)
                {
                    distToOrigin = Vector3.Distance(originPos, transform.position);
                    if (distToOrigin <= .25f)
                    {
                        // Disable physics
                        rb.isKinematic = true;
                        transform.position = originPos;
                        transform.rotation = originRot;
                        rb.isKinematic = false;
                        objectReset = true;
                    }
                }
            }
        }    
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!beingCarried)
        {
            touched = true;
        }
    }

    private IEnumerator Fadeout()
    {
        yield return new WaitForSeconds(1);
        over = false;
    }

    private IEnumerator SetOriginTrans()
    {
        yield return new WaitForSeconds(.25f);
        originPos = transform.position;
        originRot = transform.rotation;
        yield return new WaitForSeconds(.75f);
    }
}
