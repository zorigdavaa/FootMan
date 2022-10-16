using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class StandInterActer : MonoBehaviour
{
    [SerializeField] Image fillImage;
    float WaitTime = 3, currentTime;
    public EventHandler OnStandedStill;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentTime += Time.deltaTime;
            fillImage.fillAmount = currentTime / WaitTime;
            if (currentTime > WaitTime)
            {
                currentTime = 0;
                OnStandedStill?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        fillImage.fillAmount = 0;
        currentTime = 0;
    }
}
