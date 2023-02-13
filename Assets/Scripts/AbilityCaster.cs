using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCaster : MonoBehaviour
{
    [SerializeField] private KeyCode castKey = KeyCode.F;
    [SerializeField] private Ability _ability;
    [SerializeField] private float fireRate = 0.5f;
    private float nextFire;
    void Update()
    {
        if (Input.GetKey(castKey) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            _ability.Use(CameraController.camController.cameraHolder.transform);
        }
    }
}