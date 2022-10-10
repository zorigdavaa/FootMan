using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameObject Zombie;
    public int zombieCount = 5;
    public float waitTime = 2;

    private void Start()
    {
        GameManager.Instance.GamePlay += OnGamePlay;

        // for (int i = 0; i < zombieCount; i++)
        // {
        //     InstantiateZombie();
        // }
    }

    private void OnGamePlay(object sender, System.EventArgs e)
    {
        StartCoroutine(localCoroutine());
    }

    IEnumerator localCoroutine()
    {
        for (int i = 0; i < zombieCount; i++)
        {
            yield return new WaitForSeconds(waitTime);
            InstantiateZombie();
        }
    }
    public void InstantiateZombie()
    {
        Vector3 randomPoint = Random.insideUnitSphere * 3;
        randomPoint.y = 0;
        Vector3 pos = transform.position + randomPoint;
        Instantiate(Zombie, pos, Quaternion.identity);
    }
}
