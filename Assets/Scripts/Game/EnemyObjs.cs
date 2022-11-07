using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<Summary>class for enemy management </Summary>
public class EnemyObjs : MonoBehaviour
{
    public Barrack barrack;
    public WeaponTable table;
    // Start is called before the first frame update
    void Start()
    {
        barrack.OnBotTrained += OnBarrackTrained;
    }

    private void OnBarrackTrained(object sender, Bot e)
    {
        table.InstantiateWeapon();
    }

}
