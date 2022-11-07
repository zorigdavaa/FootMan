using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Barrack : MonoBehaviour
{
    public GameObject InstantiateObj;
    public Transform insPos;
    [SerializeField] List<Transform> jagsahPos;
    [SerializeField] StandInterActer standInterActer;
    public List<Bot> Soldiers;
    public WeaponTable table;
    public EventHandler<Bot> OnBotTrained;
    int jagsahIndex = 0;
    public int capacity = 5;
    float WaitTime = 3;
    float DelayTime = 3;

    private void Start()
    {
        standInterActer.OnStandedStill += OnStantedStill;
        table = FindObjectOfType<WeaponTable>();
    }

    private void OnStantedStill(object sender, System.EventArgs e)
    {
        foreach (var bot in Soldiers)
        {
            bot.GoToWar(new Vector3(0, 0, 30));
        }
    }

    // [SerializeField] List<>
    private void Update()
    {
        if (capacity > jagsahIndex)
        {
            WaitTime -= Time.deltaTime;
            if (WaitTime < 0)
            {
                Bot insBot = Instantiate(InstantiateObj, insPos.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-2f, -1f)), Quaternion.identity).GetComponent<Bot>();
                // insBot.GotoPos(jagsahPos[jagsahIndex].position);
                if (table)
                {
                    insBot.GotoPath(new List<Vector3> { table.transform.position, jagsahPos[jagsahIndex].position });
                }
                else
                {
                    insBot.GotoPos(jagsahPos[jagsahIndex].position);
                }
                jagsahIndex++;
                Soldiers.Add(insBot);
                OnBotTrained?.Invoke(this, insBot);
                WaitTime = DelayTime;
            }
        }
    }
    // public override void SwallowItem(Transform item, int price)
    // {
    //     StartCoroutine(localFunction(item));
    //     IEnumerator localFunction(Transform item)
    //     {
    //         float time = 0;
    //         float duration = SwallowDuration;
    //         Vector3 itemStartPos = item.position;
    //         while (time < duration)
    //         {
    //             time += Time.deltaTime;
    //             item.position = Vector3.Lerp(itemStartPos, transform.position, time / duration);
    //             yield return null;
    //         }
    //         if (price == 0)
    //         {
    //             Bot insBot = Instantiate(InstantiateObj, insPos.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-2f, -1f)), Quaternion.identity).GetComponent<Bot>();
    //             insBot.GotoPos(jagsahPos[jagsahIndex].position);
    //             jagsahIndex++;
    //             Price = 1;
    //         }
    //         Destroy(item.gameObject);
    //     }
    // }
}
