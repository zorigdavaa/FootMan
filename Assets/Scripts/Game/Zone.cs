using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] List<GameObject> icons;
    [SerializeField] TextMeshPro priceText;
    [SerializeField] int Price;


    Inventory Inventory;
    private void Start()
    {
        Inventory = FindObjectOfType<Player>().inventory;
    }
    float WaitTime = 0;
    private void OnTriggerStay(Collider other)
    {
        WaitTime -= Time.deltaTime;
        if (other.gameObject.CompareTag("Player") && WaitTime < 0 && Price > 0 && Inventory.HasItem())
        {
            Transform item = Inventory.GetLastItem().transform;
            if (item)
            {
                Price--;
                // priceText.text = Price.ToString();
                SwallowMaterial(item, Price);
            }
            WaitTime = 0.04f;
        }
    }

    void SwallowMaterial(Transform item, int price)
    {
        StartCoroutine(localFunction(item));
        IEnumerator localFunction(Transform item)
        {
            float time = 0;
            float duration = 0.1f;
            Vector3 itemStartPos = item.position;
            while (time < duration)
            {
                time += Time.deltaTime;
                item.position = Vector3.Lerp(itemStartPos, transform.position, time / duration);
                yield return null;
            }
            Destroy(item.gameObject);
        }
    }
}
