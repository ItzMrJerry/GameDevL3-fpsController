using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider DestructionMeter;

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of audiomanger found.");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void AddToDestructionMeter(float destruction)
    {
        DestructionMeter.value += destruction;
    }
}
