using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement settings")]
    [SetColor(0,250,233)]
    [SerializeField] float jumpForce = 250f;

    [SetColor(0, 250, 233)]
    [SerializeField] 
    private float jumpRate = 0.5f;

    private float nextFire;

    [SetColor(0, 250, 233)]
    [SerializeField] float walkSpeed = 6f, sprintSpeed = 8f;

    [Space]
    public MovementTypeEnum MovementType = new MovementTypeEnum();
    [Header("Only effects movement while using Force")]

    [SetColor(0, 250, 233)]
    [SerializeField] 
    float maximumSpeed = 5f;

    [SetColor(0, 250, 233)]
    [SerializeField] 
    float drag = 7f;

    [Header("Only effects movement while using Move Position")]

    [SerializeField]
    [SetColor(0, 250, 233)]
    float smoothTimeWhileInAir = 0.6f;

    [SetColor(0, 250, 233)]
    [Range(0, 5f)]
    [SerializeField] float smoothTime = 0.15f;

    [SetColor(255, 124, 107)]
    [ReadOnlyInspector]
    public bool isGrounded;

    Vector3 SmoothMoveVelocity;
    Vector3 moveAmount;
    Vector3 moveDir;

    Rigidbody rb;
    float smoothtimeSave;

    private void Start()
    {
        
        smoothtimeSave = smoothTime;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Jump();
        Move();
    }

    

    private void Move()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            
        switch (MovementType)
        {
            case MovementTypeEnum.MovePosition:
                moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref SmoothMoveVelocity, smoothTime);
                break;
            case MovementTypeEnum.Force:

                    float speed = Vector3.Magnitude(rb.velocity);  
                    float brakeSpeed = speed - maximumSpeed;

                    Vector3 normalisedVelocity = rb.velocity.normalized;
                    Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;

                if (speed > maximumSpeed)
                {
                    rb.AddForce(-brakeVelocity);
                }

                //Reduce sliding when not being controlled.
                //if (isGrounded && speed > 0 && speed != 0 && moveDir.z == 0 && moveDir.x == 0)
                //{
                //    rb.velocity = rb.velocity * 0.9f;
                   
                //}
                if (isGrounded && speed > 0 && speed != 0 && moveDir.z == 0 && moveDir.x == 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x * 0.9f, rb.velocity.y, rb.velocity.z * 0.9f);
                }
                //if (isGrounded && speed > 0 && speed != 0 && moveDir.x == 0)
                //{
                //    rb.velocity = new Vector3(rb.velocity.x * 0.9f, rb.velocity.y, rb.velocity.z * 0.9f);
                //}
                break;
        }
    }

    private void FixedUpdate()
    {
        switch(MovementType)
        {
            case MovementTypeEnum.MovePosition:
                rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
                break;
            case MovementTypeEnum.Force:
                Vector3 movedir = (transform.TransformDirection(moveDir) * ((Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed) * 2) * Time.fixedDeltaTime);
                
                if (isGrounded)
                rb.AddForce(movedir * rb.mass, ForceMode.Impulse);
                else
                rb.AddForce(((movedir * rb.mass) / drag) / 2 , ForceMode.Impulse);
                break;
        }

    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && Time.time > nextFire)
        {
            nextFire = Time.time + jumpRate;
            rb.AddForce(transform.up * rb.mass * jumpForce);
        }
    }
    public void SetGroundedSate(bool _grounded)
    {
        if (isGrounded == _grounded) return;
        isGrounded = _grounded;
        //if (MovementType != MovementTypeEnum.MovePosition) return;
        if (isGrounded) { smoothTime = smoothtimeSave; rb.drag = drag; }
        else { smoothTime = smoothTimeWhileInAir; rb.drag = 0; }

    }
}


public enum MovementTypeEnum
{
    MovePosition,
    Force
};
