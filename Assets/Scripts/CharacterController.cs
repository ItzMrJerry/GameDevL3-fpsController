using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] float sprintSpeed;
    [SerializeField] float walkSpeed, jumpForce, smoothTime;

    public bool isgrounded;
    Vector3 SmoothMoveVelocity;
    Vector3 moveAmount;
    Vector3 moveDir;

    Rigidbody rb;

    private void Start()
    {
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
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref SmoothMoveVelocity, smoothTime);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
    public void SetGroundedSate(bool _grounded)
    {
        isgrounded = _grounded;
    }
}
