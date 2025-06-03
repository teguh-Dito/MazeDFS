using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    private Rigidbody rb;
    public float walkspeed = 0.5f, runSpeed = 1f, jumpPower = 10f, fallSpeed = 5f;
    private Transform PlayerOrientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerOrientation = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }
    private void Movement(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = PlayerOrientation.forward * verticalInput + PlayerOrientation.right * horizontalInput;

        if(grounded){
            if(Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("Run!");
                rb.AddForce(moveDirection.normalized * runSpeed * 10f, ForceMode.Force);
            }
            else
            {
                Debug.Log("Walk");
                rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force);
            }
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            grounded = false;
            Debug.Log("Jump and Ground become false");
        }
        else if (!grounded)
        {
            rb.AddForce(Vector3.down * fallSpeed * rb.mass, ForceMode.Force);
            // grounded = true;
        }
    }
    public void groundedChanger(){
        grounded = true;
        Debug.Log("Change Ground to True");
    }
}
