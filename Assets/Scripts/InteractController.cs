using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractController : MonoBehaviour
{
    [SerializeField]
    private float interactRange = 3f;
    [SerializeField]
    private int interactLayer = 6;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private KeyCode interactKey = KeyCode.E;

    private bool CastRays = true;
    private bool CanInteract = false;

    private RaycastHit hit;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (CastRays)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(ray, out hit, interactRange))
            {

                if (hit.transform.gameObject.layer == 6) {
                    
                    CanInteract = true;
                    text.gameObject.SetActive(CanInteract);
                    Debug.Log("can interact");
                } else
                {
                    if (!CanInteract) return;
                    CanInteract = false;
                    text.gameObject.SetActive(CanInteract);
                }
            }
            else if (CanInteract)
            {
                CanInteract = false;
                text.gameObject.SetActive(CanInteract);
            }
        }

        if (CanInteract && Input.GetKeyDown(interactKey))
        {
            hit.transform.gameObject.GetComponent<Interactable>().CallOtherFunctions();
        }
    }


}
