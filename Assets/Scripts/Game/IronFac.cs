using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronFac : MonoBehaviour, ISwallower
{
    public List<Iron> Irons;
    public List<Transform> IronPos;
    public float SwallowDelay = 0.5f;
    float WaitTime = 0;
    int weaponIndex = 0;
    public Weapon WeaponPf;
    private void OnTriggerStay(Collider other)
    {
        WaitFindSwallow(other);
    }
    public void WaitFindSwallow(Collider other)
    {
        WaitTime -= Time.deltaTime;
        if (other.gameObject.CompareTag("Player") && WaitTime < 0)
        {
            Inventory Inventory = other.GetComponent<Player>().inventory;
            if (!Inventory.HasItem())
            {
                return;
            }
            GameObject item = Inventory.GetItemByTag("Iron");
            if (item)
            {
                // priceText.text = Price.ToString();
                SwallowItem(item.transform);
            }
            WaitTime = SwallowDelay;
        }
    }

    public void SwallowItem(Transform item, int prince = 0)
    {
        if (weaponIndex < IronPos.Count)
        {
            item.SetParent(transform);
            item.GetComponent<Iron>().GotoPos(IronPos[weaponIndex].position, () =>
            {
                Instantiate(WeaponPf, item.position, Quaternion.identity);
                Destroy(item.gameObject);
            });
            Irons.Add(item.GetComponent<Iron>());
            weaponIndex++;
        }
    }
}
