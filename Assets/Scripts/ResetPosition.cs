using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion rotation;
    void Start()
    {
        startPos = transform.position;
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.rotation = rotation;
            transform.position = startPos;
        }
    }
}
