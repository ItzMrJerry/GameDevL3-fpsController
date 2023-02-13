using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    [SerializeField] private FireBall fireball;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, fireball.radius);
        foreach (Collider hit in colliders)
        {
            Debug.Log(hit.transform.name);
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(fireball.power, gameObject.transform.position, fireball.radius, 3.0F);
        }
        gameObject.SetActive(false);
    }
}
