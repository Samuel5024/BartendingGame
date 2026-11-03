using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CocktailShaker : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private string actionButton = "Fire1";
    [SerializeField] private float movementSensitivity = 0.15f;
    [SerializeField] private float returnSpeed = 8f;

    [Header("Gameplay Thresholds")]
    [SerializeField] private float requiredVelocityThreshold = 4f;
    [SerializeField] private float targetShakeScore = 100f;

    [Header("Camera Control References")]
    private MouseLook playerXLook;
    private MouseLook cameraYLook;

    private Vector3 restingLocalPosition;
    private Vector3 lastLocalPosition;
    private bool isShakingActive = false;
    private float currentShakeScore = 0f;
    private bool isGrabbed = false;
    private Interact interact;

    void Start()
    {
        // Shaker defaults to zero offset relative to your child Grab Point
        //restingLocalPosition = transform.localPosition;
        lastLocalPosition = restingLocalPosition;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerXLook = playerObj.GetComponent<MouseLook>();
            interact = playerObj.GetComponent<Interact>();
        }

        GameObject cameraObj = GameObject.FindWithTag("MainCamera");
        if (cameraObj != null)
        {
            cameraYLook = cameraObj.GetComponent<MouseLook>();
            interact = cameraObj.GetComponent<Interact>();
        }

        if (cameraYLook == null || playerXLook == null || interact == null)
        {
            Debug.LogWarning($"[CocktailShaker] Setup warning! Player: {playerXLook != null}, Camera: {cameraYLook != null}, Interact: {interact != null}");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // Check if the shaker is currently attached to your camera's child Grab
        isGrabbed = transform.parent != null;

        if(!isGrabbed)
        {
            // Fail-safe protection: if dropped mid-shake, always restore the camera!
            if (isShakingActive)
            {
                ForceStopShaking();
            }
        }

        HandleShakingStateToggle();

        if (isShakingActive)
        {
            ProcessShakingInput();
            CalculateShakeVelocity();
        }
        else
        {
            ReturnToRestingPosition();
        }
    }

    private void HandleShakingStateToggle()
    {
        // Start Shaking
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown(actionButton))
        {
            if (interact != null)
            {
                interact.CrosshairUI.SetActive(false);
            }
            
            isShakingActive = true;
            if (playerXLook != null)
            {
                playerXLook.working = false; 
            }
            if (cameraYLook != null)
            {
                cameraYLook.working = false;
                
            }
        }

        // Stop Shaking
        if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetButtonUp(actionButton))
        {
            ForceStopShaking();
        }
    }

    private void ProcessShakingInput()
    {
        // Read active inputs from the Input Manager
        float moveX = (Input.GetAxis("Mouse X") %10) + Input.GetAxis("Horizontal");
        float moveY = (Input.GetAxis("Mouse Y") %10) + Input.GetAxis("Vertical");

        Vector3 targetOffset = new Vector3(moveX, moveY, 0f) * movementSensitivity;

        // Apply spatial movement relative to the grab point child coordinate space
        transform.localPosition = Vector3.Lerp(transform.localPosition, lastLocalPosition + targetOffset, Time.deltaTime * 25f);
    }

    private void CalculateShakeVelocity()
    {
   
        // Determine physical movement velocity inside local space
        float frameVelocity = ((transform.localPosition - lastLocalPosition).magnitude % 10) / Time.deltaTime;
        lastLocalPosition = transform.localPosition;

        if (frameVelocity > requiredVelocityThreshold)
        {
            currentShakeScore += frameVelocity * Time.deltaTime;
            Debug.Log($"Shaking active! Progress: {currentShakeScore:F1} / {targetShakeScore}");

            if (currentShakeScore >= targetShakeScore)
            {
                OnDrinkFinishedShaking();
            }
        }
    }

    private void ReturnToRestingPosition()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, lastLocalPosition, Time.deltaTime * returnSpeed);
        lastLocalPosition = transform.localPosition;
    }

    private void ForceStopShaking()
    {
        if (interact != null && interact.CrosshairUI != null)
        {
            interact.CrosshairUI.SetActive(true);
        }
        isShakingActive = false;

        if (playerXLook != null)
            {
                playerXLook.working = true; 
            }
        if (cameraYLook != null)
            {
                cameraYLook.working = true;
            }
    }

    private void OnDrinkFinishedShaking()
    {
        Debug.Log("Drink is ready!");
        ForceStopShaking();
        currentShakeScore = 0f;
        // Additional logic for when the drink is finished can be added here
    }


}
