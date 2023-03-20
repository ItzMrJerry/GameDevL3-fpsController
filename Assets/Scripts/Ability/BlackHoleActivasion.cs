using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleActivasion : MonoBehaviour
{
    [SerializeField] private BlackHole blackhole;
    public int RbSizeUpdate = 1;
    public List<Rigidbody> rigidodies = new List<Rigidbody>();
    public bool stayOn = false;

    private void Awake()
    {
        rigidodies = blackhole.GetRigidBodies(gameObject.transform.position);
    }
    int index;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            rigidodies = blackhole.GetRigidBodies(gameObject.transform.position);
            index = 0;
        }
        if (Input.GetKey(KeyCode.I) || stayOn)
        {
            for (int i = 0; i < RbSizeUpdate; i++)
            {
                if (index >= rigidodies.Count - 1)
                    index = 0;
                BlackHoleForce(rigidodies[index]);
                index++;
            }
        }
        
            
        
    }
    public void BlackHoleForce(Rigidbody rig)
    {
        Vector3 directionVector = (gameObject.transform.position - rig.transform.position).normalized;
        rig.AddForce(directionVector * blackhole.MaxPower,ForceMode.Acceleration);
    }
}
