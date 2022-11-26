using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;

public class Shooter : Mb
{
    [SerializeField] List<GameObject> Models;
    [SerializeField] GameObject sum;
    Slot mySlot;
    int curModelsIndex = 0;
    public int HighestModel => Models.Count - 1;
    LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");

        // dam = PlayerPrefs.GetInt("damage") == 0 ? 1 : PlayerPrefs.GetInt("damage");
        // curModelsIndex = PlayerPrefs.GetInt("ModelIndex");
        // Models[curModelsIndex].SetActive(false);
        // Models[curModelsIndex].SetActive(true);
    }
    public void UpGrade()
    {
        Models[curModelsIndex].SetActive(false);
        curModelsIndex++;
        Models[curModelsIndex].SetActive(true);
        dam++;
        // PlayerPrefs.SetInt("damage", dam);
        // PlayerPrefs.SetInt("ModelIndex", curModelsIndex);
    }
    public void SetSlot(Slot slot)
    {
        mySlot = slot;
    }
    public Slot GetSlot()
    {
        return mySlot;
    }
    public int GetModelIndex()
    {
        return curModelsIndex;
    }
    public bool UpgradeAble()
    {
        return curModelsIndex < HighestModel;
    }
    public float shotTime = 3;
    public float shotPower = 2000;
    float shootTImer = 3;
    float delyTime = 0;
    bool isDely;
    private void Update()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 50, enemyLayer);
        shootTImer -= Time.deltaTime;
        if (enemies.Length > 0 && shootTImer < 0)
        {
            shootTImer = shotTime;
            Shoot(enemies[0].transform);
        }

        if (isDely)
        {
            delyTime += Time.deltaTime;
            if (delyTime > 0.5f)
            {
                shotTime = 3;
            }
        }

        if (IsDown)
        {
            delyTime = 0;
            isDely = false;
            shotTime = 1;
        }
        if (IsUp)
        {
            isDely = true;
        }
    }
    public int dam = 1;

    private void Shoot(Transform Target)
    {
        transform.LookAt(Target); // 
        
        GameObject insSum = Instantiate(sum, transform.position + Vector3.up * 2, Quaternion.identity);
        insSum.GetComponent<BulletDamage>().SetBulletDamage(dam,Target);
        
        // Vector3 dir = collider.transform.position - transform.position;
        // insSum.GetComponent<Rigidbody>().AddForce(dir.normalized * shotPower);
        
        // Destroy(insSum, 2);
    }
}
