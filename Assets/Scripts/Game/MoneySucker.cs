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
    float SwallowDelay = 0.1f;
    public float SwallowDuration = 0.5f;
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
            Price--;
            if (Price == 0)
            {
                Instantiate(InstantiateObj, insPos.position, Quaternion.identity);
                Destroy(gameObject);
            }
            WaitTime = SwallowDelay;
        }
    }
}
