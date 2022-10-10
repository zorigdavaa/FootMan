using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;

public class Level : MonoBehaviour
{
    [SerializeField] GameObject Hana;
    [SerializeField] int HanaCount;
    [SerializeField] ZombieSpawner zombieSpawner;
    [SerializeField] int zombieSpawnerCount;
    private void OnEnable()
    {
        Random.InitState(GameManager.Instance.Level);
        HanaCount = Random.Range(10, 20);
        zombieSpawnerCount = 4;
        for (int i = 0; i < HanaCount; i++)
        {
            Instantiate(Hana, new Vector3((Random.Range(-40f, 40f)), 0.5f, Random.Range(-40f, 40f)), Quaternion.Euler(0, Random.value > 0.5f ? 0 : 90, 0), transform);
        }
        for (int i = 0; i < zombieSpawnerCount; i++)
        {
            Instantiate(zombieSpawner, new Vector3((Random.Range(-40f, 40f)), 0.5f, Random.Range(-40f, 40f)), Quaternion.identity, transform);
        }
        AstarPath.active.Scan();
    }
}
