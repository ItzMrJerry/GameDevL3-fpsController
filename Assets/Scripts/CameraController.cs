using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController camController;
    [Header("Camera Settings")]
    [SerializeField] public GameObject cameraHolder;
    [Range(0, 2f)]
    [SerializeField] private float mouseSens = 1f;
    private float verticalLookRotation;


    private void Awake()
    {
        if (camController == null)
            camController = this;
        else
            Destroy(this);
    }
    private void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Look();
    }
    private void Look()
    {
        float sens = (mouseSens * 100f) * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * sens);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * sens;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);


        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
}
