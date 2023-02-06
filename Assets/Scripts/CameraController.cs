using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private GameObject cameraHolder;
    [Range(0,2f)]
    [SerializeField] private float mouseSens = 1f;
    private float verticalLookRotation;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        Look();
    }
    private void Look()
    {
        float sens = (mouseSens * 100f) * Time.deltaTime;
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * sens);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * sens;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);


        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
}
