using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StandInterActer : MonoBehaviour
{
    float WaitTime = 3;
    public EventHandler OnStandedStill;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            WaitTime -= Time.deltaTime;
            if (WaitTime < 0)
            {
                WaitTime = 3;
                OnStandedStill?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
