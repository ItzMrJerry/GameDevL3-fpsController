using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    [SerializeField] private FireBall fireball;
    private void OnTriggerEnter(Collider other)
    {
        
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, fireball.radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player") || colliders[i].CompareTag("Terrain")) continue;
            if (!colliders[i].gameObject.GetComponent<Rigidbody>())
            {
            colliders[i].gameObject.AddComponent<Rigidbody>();
                GameManager.instance.AddToDestructionMeter(0.01f);
            }
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(fireball.power, gameObject.transform.position, fireball.radius, 3.0F);
           
        }
        gameObject.SetActive(false);
    }

    IEnumerator DisableOverTime()
    {
        yield return new WaitForSeconds(.5f);
    }
}
