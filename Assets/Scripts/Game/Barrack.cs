using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Zone
{
    [SerializeField] List<Transform> jagsahPos;
    int jagsahIndex = 0;
    // [SerializeField] List<>
    public override void SwallowMaterial(Transform item, int price)
    {
        StartCoroutine(localFunction(item));
        IEnumerator localFunction(Transform item)
        {
            float time = 0;
            float duration = SwallowDuration;
            Vector3 itemStartPos = item.position;
            while (time < duration)
            {
                time += Time.deltaTime;
                item.position = Vector3.Lerp(itemStartPos, transform.position, time / duration);
                yield return null;
            }
            if (price == 0)
            {
                Bot insBot = Instantiate(InstantiateObj, insPos.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-2f, -1f)), Quaternion.identity).GetComponent<Bot>();
                insBot.GotoPos(jagsahPos[jagsahIndex].position);
                jagsahIndex++;
                Price = 1;
            }
            Destroy(item.gameObject);
        }
    }
}
