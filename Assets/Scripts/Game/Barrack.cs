using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : MonoBehaviour
{
    public GameObject InstantiateObj;
    public Transform insPos;
    [SerializeField] List<Transform> jagsahPos;
    int jagsahIndex = 0;
    public int capacity = 5;
    float WaitTime = 0;
    float DelayTime = 3;
    // [SerializeField] List<>
    private void Update()
    {
        if (capacity > jagsahIndex)
        {
            WaitTime -= Time.deltaTime;
            if (WaitTime < 0)
            {
                Bot insBot = Instantiate(InstantiateObj, insPos.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-2f, -1f)), Quaternion.identity).GetComponent<Bot>();
                insBot.GotoPos(jagsahPos[jagsahIndex].position);
                jagsahIndex++;
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
