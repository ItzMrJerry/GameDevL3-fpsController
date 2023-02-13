using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] float jumpForce = 250f;
    [SerializeField] float walkSpeed = 6f, sprintSpeed = 8f;

    [Space]
    public MovementTypeEnum MovementType = new MovementTypeEnum();
    [Header("Only effects movement while using Force")]
    [SerializeField] float maximumSpeed = 5f;
    [Header("Only effects movement while using Move Position")]
    [SerializeField]
    float smoothTimeWhileInAir = 0.6f;
    [Range(0, 5f)]
    [SerializeField] float smoothTime = 0.15f;

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
        Move();
        Jump();


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
                Vector3 movedir = transform.TransformDirection(moveDir) * ((Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed) * 2) * Time.fixedDeltaTime;
                rb.AddForce(movedir, ForceMode.Impulse);
                
                break;
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * rb.mass * jumpForce);
        }
    }
    public void SetGroundedSate(bool _grounded)
    {
        if (isGrounded == _grounded) return;
        isGrounded = _grounded;
        if (MovementType != MovementTypeEnum.MovePosition) return;
        if (isGrounded) smoothTime = smoothtimeSave;
        else smoothTime = smoothTimeWhileInAir;

    }
}


public enum MovementTypeEnum
{
    MovePosition,
    Force
};
