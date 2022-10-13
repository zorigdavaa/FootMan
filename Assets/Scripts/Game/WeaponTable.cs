using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTable : MonoBehaviour
{
    [SerializeField] List<Transform> weaponPos;
    [SerializeField] List<Weapon> placedWeapong;
    public float SwallowDelay = 0.5f;
    float WaitTime = 0;
    int weaponIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        // for (int i = 0; i < placedWeapong.Count; i++)
        // {
        //     placedWeapong[i].GotoPos(weaponPos[i].position);
        // }
    }

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
            GameObject item = Inventory.GetItemByTag("Weapon");
            if (item)
            {
                // priceText.text = Price.ToString();
                PlaceWeapon(item.transform);
            }
            WaitTime = SwallowDelay;
        }
    }

    public void PlaceWeapon(Transform item)
    {
        if (weaponIndex < weaponPos.Count)
        {
            item.SetParent(transform);
            item.GetComponent<Weapon>().GotoPos(weaponPos[weaponIndex].position);
            placedWeapong.Add(item.GetComponent<Weapon>());
            weaponIndex++;
        }
    }
}
