using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] List<GameObject> Models;
    // Start is called before the first frame update

    public void GotoPos(Vector3 toPos)
    {
        StartCoroutine(LocalCoroutine());
        IEnumerator LocalCoroutine()
        {
            float t = 0;
            float time = 0;
            float duration = 1;
            Vector3 initialPosition = transform.position;
            while (time < duration)
            {
                t = time / duration;
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(initialPosition, toPos, t);

                yield return null;
            }
        }

    }
}
