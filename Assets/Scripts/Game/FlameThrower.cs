using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    float time = 2;
    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            foreach (var item in Physics.OverlapCapsule(transform.position, transform.position + transform.forward * 5, 4, 1 << 3))
            {
                item.GetComponent<Character>().TakeDamage(-1);
            }
        }

    }
}
