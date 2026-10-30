using System.Runtime.CompilerServices;
using UnityEngine;

public class CocktailShaker : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private string actionButton = "Fire1";
    [SerializeField] private float momentSensitivity = 0.15f;
    [SerializeField] private float returnSpeed = 8f;

    [Header("Gameplay Thresholds")]
    [SerializeField] private float requiredVelocitythreshold = 4f;
    [SerializeField] private float targetShakeScore = 100f;

    private Vector3 restingLocalPosition;
    private Vector3 lastLocalPosition;
    private bool isShakingActive = false;
    private float currentShakeScore = 0f;

    private MouseLook playerMouseLook;
    private bool isGrabbed = false;

    void Start()
    {
        // Shaker defaults to zero offset relative to your child Grab Point
        restingLocalPosition = transform.position;
        lastLocalPosition = restingLocalPosition;
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

        // Try to bind your look script if we don't have it cached yet
        if (playerMouseLook == null)
        {
            playerMouseLook = GetComponentInParent<MouseLook>();
        }

        HandleShakingStateToggle();

        if (isShakingActive)
        {
            ProcessshakingInput();
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
            isShakingActive = true;
            if (playerMouseLook != null)
            {
                playerMouseLook.working = false; // Freeze camera look
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
        float moveX = Input.GetAxis("Mouse X") + Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Mouse Y") + Input.GetAxis("Vertical");

        Vector3 targetOffset = new Vector3(moveX, moveY, of) * movementSensitivity;

        // Apply spatial movement relative to the grab point child coordinate space
        transform.localPosition = Vector3.Lerp(transform.localPosition, restingLocalPosition + targetOffset, Time.deltaTime * 25f);
    }

    private void CalculateShakeVelocity()
    {
        // Determine physical movement velocity inside local space
        float frameVelocity = (transform.localPosition - lastLocalPosition).magnitude / Time.deltaTime;
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
        transform.localPosition = Vector3.Lerp(transform.localPosition, restingLocalPosition, Time.deltaTime * returnSpeed);
        lastLocalPosition = transform.localPosition;
    }

    private void ForceStopShaking()
    {
        isShakingActive = false;
        if (playerMouseLook != null)
        {
            playerMouseLook.working = true;
        }
    }

    
}
