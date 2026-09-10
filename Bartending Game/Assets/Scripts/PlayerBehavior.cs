using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Rigidbody rb;
    public float walkSpeed = 20f;
    

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(walkSpeed * Time.deltaTime, 0, 0);
        }
    }
}
