using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTable : MonoBehaviour, ISwallower
{
    [SerializeField] List<Transform> weaponPos;
    [SerializeField] List<Weapon> placedWeapong;
    public float SwallowDelay = 0.5f;
    float WaitTime = 0;
    int weaponIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        // FindObjectOfType<Barrack>().table = this;
        // for (int i = 0; i < placedWeapong.Count; i++)
        // {
        //     placedWeapong[i].GotoPos(weaponPos[i].position);
        // }
    }
    private void OnTriggerEnter(Collider other)
    {
        Character charac = other.GetComponent<Character>();
        if (charac && !charac.HasWeapon() && placedWeapong.Count > 0)
        {
            int weaponUpgradeIndex = placedWeapong[placedWeapong.Count - 1].GetModelIndex();
            Destroy(placedWeapong[placedWeapong.Count - 1].gameObject);
            placedWeapong.RemoveAt(placedWeapong.Count - 1);
            weaponIndex--;
            charac.ShowWeapon();
            for (int i = 0; i < weaponUpgradeIndex; i++)
            {
                charac.UpGradeWeapon();
            }
        }
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
                SwallowItem(item.transform);
            }
            WaitTime = SwallowDelay;
        }
    }

    public void SwallowItem(Transform item, int prince = 0)
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
