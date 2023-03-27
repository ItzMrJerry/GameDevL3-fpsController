using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{
    [SerializeField] private FireBall fireball;
    public AudioClip explosion;
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = AudioManager.instance.PlaySound(explosion, 1 * AudioManager.instance.GameVolume, "Explosion");
        go.transform.position = transform.position;
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, fireball.radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player") || colliders[i].CompareTag("Terrain")) continue;
            if (!colliders[i].gameObject.GetComponent<Rigidbody>())
            {
            colliders[i].gameObject.AddComponent<Rigidbody>();
                GameManager.instance.AddToDestructionMeter(0.05f);
            }
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
            GameManager.instance.rigidodies.Add(rb);
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
