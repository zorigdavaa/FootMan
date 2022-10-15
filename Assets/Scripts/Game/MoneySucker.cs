using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZPackage;

public class MoneySucker : MonoBehaviour
{
    [SerializeField] List<GameObject> icons;
    [SerializeField] TextMeshPro priceText;
    public Transform insPos;
    public int Price;
    public GameObject monePrefab;
    float SwallowDelay = 1f;
    public GameObject InstantiateObj;
    float WaitTime = 0;
    private void OnTriggerStay(Collider other)
    {
        WaitFindSwallow(other);
    }

    public virtual void WaitFindSwallow(Collider other)
    {
        WaitTime -= Time.deltaTime;
        if (other.gameObject.CompareTag("Player") && WaitTime < 0 && Price > 0 && GameManager.Instance.Coin > 0)
        {
            GameManager.Instance.Coin--;
            Price--;
            InsMoneyAndSuck(other, Price);

            WaitTime = SwallowDelay;
        }
    }

    private void InsMoneyAndSuck(Collider other, int price)
    {
        GameObject money = Instantiate(monePrefab, other.transform.position + Vector3.up * 3, Quaternion.identity);
        StartCoroutine(localCoroutine());
        IEnumerator localCoroutine()
        {
            float time = 0;
            float duration = 1;
            float t = 1;
            while (time < duration)
            {
                time += Time.deltaTime;
                t = time / duration;
                money.transform.position = Vector3.Lerp(money.transform.position, transform.position, t);
                // Mathf.Lerp(0, 1, t);
                yield return null;
            }
            if (price == 0)
            {
                Instantiate(InstantiateObj, insPos.position, Quaternion.identity);
                Destroy(gameObject);
            }
            Destroy(money.gameObject);
        }
    }
}
