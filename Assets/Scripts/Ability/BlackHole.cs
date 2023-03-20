using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/BlackHole")]
public class BlackHole : Ability
{
    public float radius = 5.0F;
    public float MaxPower = 10.0F;

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

    public override void Use(Transform transform)
    {
        throw new System.NotImplementedException();
    }


}
