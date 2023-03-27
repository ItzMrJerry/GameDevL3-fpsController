using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleActivasion : MonoBehaviour
{
    [SerializeField] private BlackHole blackhole;
    //public List<Rigidbody> rigidodies = new List<Rigidbody>();
    public bool stayOn = false;
    private bool isOn;
    private float dir = 1;
  
    int index;

    private void Awake()
    {
        transform.localScale = new Vector3();
        StartCoroutine(scaleOverTime(transform, blackhole.size, blackhole.scaleTime));
        GameManager.instance.rigidodies = blackhole.GetRigidBodies(gameObject.transform.position);

    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    GameManager.instance.rigidodies = blackhole.GetRigidBodies(gameObject.transform.position);
        //    index = 0;
        //}
        if (stayOn && GameManager.instance.rigidodies.Count > 0 && isOn)
        {
            for (int i = 0; i < blackhole.RbSizeUpdate; i++)
            {
                if (index >= GameManager.instance.rigidodies.Count - 1)
                    index = 0;
                BlackHoleForce(GameManager.instance.rigidodies[index]);
                index++;
            }
        }
    }

    IEnumerator destroyOnLifeTime()
    {
        yield return new WaitForSeconds(blackhole.lifeTime);
        StartCoroutine(scaleOverTime(transform, Vector3.zero, blackhole.scaleTime));
        yield return new WaitForSeconds(blackhole.scaleTime - 1);
        dir = -1;
        yield return new WaitForSeconds(1);
        GameManager.instance.rigidodies.Clear();
        GameManager.instance.blackHoleIsActive = false;
        Destroy(gameObject);
    }

    bool isScaling = false;

    IEnumerator scaleOverTime(Transform objectToScale, Vector3 toScale, float duration)
    {
        //Make sure there is only one instance of this function running
        if (isScaling)
        {
            yield break; ///exit if this is still running
        }
        isScaling = true;

        float counter = 0;

        //Get the current scale of the object to be moved
        Vector3 startScaleSize = objectToScale.localScale;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
            yield return null;
        }

        isScaling = false;
        if (!isOn) StartCoroutine(destroyOnLifeTime());
        isOn = true;
    }

     IEnumerator changeOverTime(float objectToScale, float ChangeTo, float duration)
        {
            //Make sure there is only one instance of this function running
            if (isScaling)
            {
                yield break; ///exit if this is still running
            }
            isScaling = true;

            float counter = 0;

            //Get the current scale of the object to be moved
            float startScaleSize = objectToScale;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                objectToScale = Mathf.Lerp(startScaleSize, ChangeTo, counter / duration);
                yield return null;
            }

            isScaling = false;
        }

    public void BlackHoleForce(Rigidbody rig)
    {
        Vector3 directionVector = (gameObject.transform.position - rig.transform.position).normalized;
        float distance = Vector3.Distance(gameObject.transform.position, rig.transform.position);
        float force = blackhole.MaxPower / (distance * distance);
        rig.AddForce(directionVector * force, ForceMode.Acceleration);


        //Vector3 directionVector = (gameObject.transform.position - rig.transform.position).normalized;
        //rig.AddForce((directionVector * blackhole.MaxPower) * dir, ForceMode.Acceleration);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(GameManager.instance.rigidodies.Contains(other.GetComponent<Rigidbody>()))
        {
            GameManager.instance.rigidodies.Remove(other.GetComponent<Rigidbody>());
            other.gameObject.SetActive(false);
            GameManager.instance.AddToDestructionMeter(0.03f);
        }
    }


}
