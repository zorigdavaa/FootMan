using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<Summary>class for enemy management </Summary>
public class EnemyObjs : MonoBehaviour
{
    public Barrack barrack;
    public WeaponTable table;
    float Attactime = 10;
    // Start is called before the first frame update
    void Start()
    {
        barrack.OnBotTrained += OnBarrackTrained;
    }

    private void OnBarrackTrained(object sender, Bot e)
    {
        table.InstantiateWeapon();
    }
    private void Update()
    {
        Attactime -= Time.deltaTime;
        if (Attactime < 0)
        {
            Attactime = 10;
            foreach (var item in barrack.Soldiers)
            {
                item.GoToWar(new Vector3(0, 0, -10));
            }
        }
    }

}
