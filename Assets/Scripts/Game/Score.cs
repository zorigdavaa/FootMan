using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;

public class Score : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            particle.Play();
            particle.transform.SetParent(null);
            // LevelSpawner.PutSomeWhere(transform);
            Invoke(nameof(BringParticle),0.5f);
            gameObject.SetActive(false);
        }
    }

    private void BringParticle()
    {
        particle.transform.SetParent(transform);
        particle.transform.localPosition = Vector3.zero;
    }
}
