using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Zone
{
    [SerializeField] List<Transform> jagsahPos;
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
                Instantiate(InstantiateObj, insPos.position, Quaternion.identity);

                Price = 1;
            }
            Destroy(item.gameObject);
        }
    }
}
