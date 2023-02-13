using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponentInParent<CharacterController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        characterController.SetGroundedSate(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) return;
        characterController.SetGroundedSate(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) return;
        characterController.SetGroundedSate(true);
    }
}
