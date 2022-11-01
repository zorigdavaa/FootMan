using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZPackage;

public class MoneySucker : MonoBehaviour
{
    [SerializeField] List<GameObject> icons;
    [SerializeField] TextMeshPro priceText;
    [SerializeField] Image fillImage;
    public Transform insPos;
    public int Price, CollectedMoney;
    public GameObject monePrefab;
    float SwallowDelay = 0.3f;
    public GameObject InstantiateObj;
    float WaitTime = 0;
    private void Start()
    {
        fillImage.fillAmount = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        WaitFindSwallow(other);
    }

    public virtual void WaitFindSwallow(Collider other)
    {
        WaitTime -= Time.deltaTime;
        if (other.gameObject.CompareTag("Player") && WaitTime < 0 && CollectedMoney < Price && GameManager.Instance.Coin > 0)
        {
            GameManager.Instance.Coin--;
            CollectedMoney++;
            fillImage.fillAmount = (float)CollectedMoney / (float)Price;
            InsMoneyAndSuck(other, CollectedMoney);
            WaitTime = SwallowDelay;
        }
    }

    private void InsMoneyAndSuck(Collider other, int curColMoney)
    {
        GameObject money = Instantiate(monePrefab, other.transform.position + Vector3.up * 5, Quaternion.identity);
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
            if (curColMoney == Price)
            {
                Instantiate(InstantiateObj, insPos.position, Quaternion.identity);
                Destroy(gameObject);
            }
            Destroy(money.gameObject);
        }
    }
}
