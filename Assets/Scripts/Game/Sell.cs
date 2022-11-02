using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZPackage;

public class Sell : Zone
{
    private Camera cam;
    [SerializeField] GameObject moneyPf;
    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }
    public override void WaitFindSwallow(Collider other)
    {
        WaitTime -= Time.deltaTime;
        if (other.gameObject.CompareTag("Player") && WaitTime < 0)
        {
            Inventory Inventory = other.GetComponent<Player>().inventory;
            if (!Inventory.HasItem())
            {
                return;
            }
            Transform item = Inventory.GetLastItem().transform;
            if (item)
            {
                SwallowItem(item, item.GetComponent<Collect>().SellPrice);
            }
            WaitTime = SwallowDelay;
        }
    }
    public override void DoAction(int price)
    {
        GameManager.Instance.Coin += price;
        InsMoneyAndGotoTop();
        GameObject plusOne = Instantiate(InstantiateObj, InstantiateObj.transform.position, Quaternion.identity, transform);
        plusOne.GetComponent<TextMeshPro>().text = price.ToString();
        plusOne.SetActive(true);
        Destroy(plusOne, 1);
    }
    private void InsMoneyAndGotoTop()
    {
        GameObject money = Instantiate(moneyPf, transform.position + Vector3.up, Quaternion.identity);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(CanvasManager.Instance.GetComponent<RectTransform>(), CanvasManager.Instance.Coin.GetComponent<RectTransform>().position, cam, out Vector3 worldPoint);
        // Vector3 toPoint = cam.ScreenToWorldPoint(((RectTransform)CanvasManager.Instance.Coin.transform).position);
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
                money.transform.position = Vector3.Lerp(money.transform.position, worldPoint, t);
                // money.transform.localScale = Vector3.Lerp(money.transform.localScale, Vector3.zero, t);
                // Mathf.Lerp(0, 1, t);
                yield return null;
            }
            Destroy(money.gameObject);
        }
    }
}
