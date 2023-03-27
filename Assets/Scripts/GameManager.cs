using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider DestructionMeter;
    public GameObject victoryText;
    private bool victory = false;
    public List<Rigidbody> rigidodies = new List<Rigidbody>();
    public bool blackHoleIsActive = false;
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

    private void Update()
    {
        if (victory && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void AddToDestructionMeter(float destruction)
    {
        DestructionMeter.value += destruction;
        if (DestructionMeter.value == DestructionMeter.maxValue)
        {
            victoryText.SetActive(true);
            victory = true;
        }
    }
}
