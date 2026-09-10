using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Rigidbody rb;
    public float walkSpeed = 20f;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(walkSpeed * Time.deltaTime, 0, 0);
        }

        else
        {
            Debug.Log("nothing is being pressed");
        }
    }

    public void FixedUpdate()
    {
        
    }
}
