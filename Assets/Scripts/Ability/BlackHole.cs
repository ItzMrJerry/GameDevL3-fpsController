using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/BlackHole")]
public class BlackHole : Ability
{
    public float radius = 5.0F;
    public float MaxPower = 10.0F;
    public GameObject prefab;
    public float lifeTime = 15;
    public Vector3 size;
    public float scaleTime = 4;
    public int RbSizeUpdate = 20;
    public AudioClip sound;
    public List<Rigidbody> GetRigidBodies(Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, radius);

        List<Rigidbody> rigidbodys = new List<Rigidbody>();
        foreach (var col in colliders)
        {
            if (col.GetComponent<Rigidbody>())
            {
                if (!col.CompareTag("Player"))
                rigidbodys.Add(col.GetComponent<Rigidbody>());
            }
        }
        return rigidbodys;
    }

    public override bool Use(Transform transform)
    {

            if (GameManager.instance.blackHoleIsActive)
            {
                Debug.Log("There is already an active blackhole!");
                return false;
            }
            GameObject go = AudioManager.instance.PlaySound(sound, 1 * AudioManager.instance.GameVolume, "BlackHole");
            go.transform.position = transform.position;
            AudioManager.instance.StartCoroutine(AudioManager.instance.WaitForClipToEnd(23, go));
            Instantiate(prefab, new Vector3(
            CharacterController.instance.transform.position.x,
            CharacterController.instance.transform.position.y + 10,
            CharacterController.instance.transform.position.z)
            , Quaternion.identity);
            GameManager.instance.blackHoleIsActive = true;
        return true;
    }


}
