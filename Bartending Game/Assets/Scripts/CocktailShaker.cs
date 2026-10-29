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

    private MouseLook playerMouseLook;
    private bool isGrabbed = false;

    void Start()
    {
        // Shaker defaults to zero offset relative to your child Grab Point
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
