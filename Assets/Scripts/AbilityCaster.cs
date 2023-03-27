using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCaster : MonoBehaviour
{
    [SerializeField] private KeyCode castKey = KeyCode.F;
    [SerializeField] private Ability currentAbility;
    [SerializeField] private List<Ability> _ability;
    [SerializeField] private float fireRate = 0.5f;
    public Image icon;
    public Image rechargeImage;
    private float nextFire;
    public int abilityIndex = 0;

    private void Start()
    {
        currentAbility = _ability[abilityIndex];
    }
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            // Scroll up
            abilityIndex += 1;
            if (abilityIndex > _ability.Count -1)
            {
                abilityIndex = _ability.Count - 1;
            }
            currentAbility = _ability[abilityIndex];
            icon.sprite = currentAbility.icon;
        }
        else if (scroll < 0)
        {
            // Scroll down
            abilityIndex -= 1;
            if (abilityIndex < 0)
            {
                abilityIndex = 0;
            }
            currentAbility = _ability[abilityIndex];
            icon.sprite = currentAbility.icon;
        }
        if (Input.GetKeyDown(castKey) && Time.time > nextFire)
        {
            
            if (currentAbility.Use(CameraController.camController.cameraHolder.transform))
            {
                nextFire = Time.time + currentAbility.fireRate;
                rechargeImage.fillAmount = 1;
                StopAllCoroutines();
                StartCoroutine(changeOverTime(1, 0, currentAbility.fireRate));
            }

            
            
        }


    }
    IEnumerator changeOverTime(float objectToScale, float ChangeTo, float duration)
    {

        float counter = 0;

        //Get the current scale of the object to be moved
        float startScaleSize = objectToScale;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            rechargeImage.fillAmount = Mathf.Lerp(startScaleSize, ChangeTo, counter / duration);
            yield return null;
        }



    }
}
