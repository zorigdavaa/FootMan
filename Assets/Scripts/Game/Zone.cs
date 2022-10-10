using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] List<GameObject> icons;
    [SerializeField] TextMeshPro priceText;
    public Transform insPos;
    public int Price;
    public float SwallowDelay = 0.04f;
    public float SwallowDuration = 0.5f;
    public GameObject InstantiateObj;


    Inventory Inventory;
    private void Start()
    {
        Inventory = FindObjectOfType<Player>().inventory;
    }
    float WaitTime = 0;
    private void OnTriggerStay(Collider other)
    {
        WaitTime -= Time.deltaTime;
        if (other.gameObject.CompareTag("Player") && WaitTime < 0 && Price > 0)
        {
            Inventory = other.GetComponent<Player>().inventory;
            if (!Inventory.HasItem())
            {
                return;
            }
            Transform item = Inventory.GetLastItem().transform;
            if (item)
            {
                Price--;
                // priceText.text = Price.ToString();
                SwallowMaterial(item, Price);
            }
            WaitTime = SwallowDelay;
        }
    }

    public virtual void SwallowMaterial(Transform item, int price)
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
                Destroy(gameObject);
            }
            Destroy(item.gameObject);
        }
    }
}
