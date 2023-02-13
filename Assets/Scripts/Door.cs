using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : Interactable
{
    public UnityEvent secondEvent;
    public UnityEvent lockEvent;
    [SerializeField]
    private bool DoorState = false;
    [SerializeField]
    private float firstEventgDelay = 0f;
    [SerializeField]
    private float secondEventDelay = 0f;



    public override void CallOtherFunctions()
    {
        if (!DoorState)
        {
            LockEvent();
            StopAllCoroutines();
            StartCoroutine(FirstEventDelay());
        }
        else
        {
            LockEvent();
            StopAllCoroutines();
            StartCoroutine(SecondEventDelay());

        }
    }
    public void LockEvent()
    {
        lockEvent.Invoke();
    }
    public void SecondEvent()
    {
        secondEvent.Invoke();
        DoorState = false;
    }
    IEnumerator FirstEventDelay()
    {
        yield return new WaitForSeconds(firstEventgDelay);
        base.CallOtherFunctions();
        DoorState = true;
    }
    IEnumerator SecondEventDelay()
    {
        yield return new WaitForSeconds(secondEventDelay);
        secondEvent.Invoke();
        DoorState = false;
    }
}
