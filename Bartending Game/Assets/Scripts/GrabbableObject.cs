using System.Collections;
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
    public string[] prompts = { " Grab Object", "" };
    private bool touched = false;
    private bool rotating = false;
    private Quaternion rotateBy;
    private Vector3 originPos;
    private Quaternion originRot;
    public bool canReturn = true;
    private bool objectReset = true;
    private float distToOrigin;

    /*********************************************

    Still need to write these other scripts:

    private Interact interactable;
    private MouseLook[] mouseLook;

    Until then, any reference to them is  
    commented out to avoid compile errors
    ******************************************/

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

    // Update is called once per frame
    void Update()
    {
        
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
