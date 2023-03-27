using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/FireBall")]
public class FireBall : Ability
{
    
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] float startVelocity;
    public float radius = 5.0F;
    public float power = 10.0F;


    public override bool Use(Transform transform)
    {
      

        GameObject go = ObjectPool.objectpool.GetObjectFromPool();
        Rigidbody rb = go.gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        go.transform.rotation = transform.rotation;
        go.transform.position = transform.position - new Vector3(0,0.5f,0);
        go.transform.position += transform.forward * 2.5f;
        rb.velocity = transform.forward * startVelocity;

        //GameObject go = Instantiate(fireBallPrefab, transform.position, transform.rotation);


        return true;
    }


  


}
