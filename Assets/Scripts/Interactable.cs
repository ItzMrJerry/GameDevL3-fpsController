using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent firstEvent;
    public virtual void CallOtherFunctions()
    {
        firstEvent.Invoke();
        Debug.Log("Ineracted");
    }
}
