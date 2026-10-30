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

        // Try to ind your look script if we don't have it cached yet
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

        if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetButtonUp(actionButton))
        {
            ForceStopShaking();
        }
    }


}
