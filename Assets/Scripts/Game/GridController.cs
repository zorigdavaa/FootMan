using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using ZPackage;

public class GridController : Mb
{
    [SerializeField] List<Slot> Slots;
    Camera cam;

    RaycastHit hit;
    Vector3 mouseWorldPos;
    Ray ray;
    [SerializeField] Shooter draggingObject;
    [SerializeField] Shooter shooterPF;
    Vector3 dragObjOriginPos, lastDragPos;
    LayerMask roadMask;
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        roadMask = LayerMask.GetMask("Road");
        // CanvasController.Instance.HudCoin(GameController.Coin);
        GetSaveSlots();
        price=PlayerPrefs.GetInt("CurrentPrice",price);
         PriceText.text="Price"+price;
    }
    float countDown = 2;
    public bool dragging = false;
    // Update is called once per frame
    void Update()
    {
        // countDown -= Time.deltaTime;
        // if (countDown < 0 && !dragging)
        // {
        //     countDown = 2;
        //     InstantiatePF();
        // }

        if (IsDown)
        {
            dragging = true;
        }
        else if (IsClick && dragging)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            // Vector3 mousePos = Input.mousePosition;
            // mousePos.z = 6;
            // mouseWorldPos = cam.ScreenToWorldPoint(mousePos);
            if (draggingObject == null && Physics.Raycast(ray, out hit) && hit.transform.GetComponent<Shooter>())
            {
                draggingObject = hit.transform.GetComponent<Shooter>();
                dragObjOriginPos = draggingObject.transform.position;
                draggingObject.GetSlot()?.SetShooter(null);
            }
            else if (draggingObject && Physics.Raycast(ray, out hit, 100, roadMask))
            {
                draggingObject.transform.position = hit.point + Vector3.up;
                lastDragPos = hit.point;
            }
        }
        else if (IsUp)
        {
            if (draggingObject)
            {
                Slot nearestSlot = null;
                float distance = 100;
                foreach (var item in Slots)
                {
                    float dis = Vector3.Distance(lastDragPos, item.transform.position);
                    if (dis < distance)
                    {
                        distance = dis;
                        nearestSlot = item;
                    }
                }
                if (distance < 1 && nearestSlot.shooter == null)
                {
                    nearestSlot.SetShooter(draggingObject);
                }
                else if (distance < 1 && nearestSlot.shooter != null && draggingObject.UpgradeAble() && nearestSlot.shooter.GetModelIndex() == draggingObject.GetModelIndex())
                {
                    nearestSlot.shooter.UpGrade();
                    Destroy(draggingObject.gameObject);
                }
                else
                {
                    draggingObject.GetSlot()?.SetShooter(draggingObject);
                }
                draggingObject = null;
                SaveSlots();
            }

            dragging = false;
        }
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     SaveSlots();
        // }
    }
    public void SaveSlots()
    {
        List<SaveSlot> saveSlots = new List<SaveSlot>();
        foreach (var item in Slots)
        {
            // SaveSlot ss = new SaveSlot((item.shooter != null), item.shooter.GetModelIndex(), item.shooter.dam);
            if (item.shooter != null)
            {
                saveSlots.Add(new SaveSlot(true, item.shooter.GetModelIndex(), item.shooter.dam));
            }
            else
            {
                saveSlots.Add(new SaveSlot());
            }
        }
        StringBuilder builder = new StringBuilder();
        foreach (var item in saveSlots)
        {
            builder.Append(item.hasShooter ? 1 : 0);
            // builder.Append(",");

            builder.Append(item.curModelIndex);
            // builder.Append(",");

            builder.Append(item.dam);
            // builder.Append(",");

        }
        PlayerPrefs.SetString("list", builder.ToString());
        // print(builder);
    }
    public void GetSaveSlots()
    {
        string saveString = PlayerPrefs.GetString("list");
        print(saveString);
        List<SaveSlot> saveSlots = new List<SaveSlot>();
        int stringIndex = 0;
        for (int i = 0; i < Slots.Count; i++)
        {
            saveSlots.Add(new SaveSlot(int.Parse(saveString[stringIndex].ToString()) == 1, int.Parse(saveString[stringIndex + 1].ToString()), int.Parse(saveString[stringIndex + 2].ToString())));
            // saveSlots[i].hasShooter = int.Parse(saveString[stringIndex].ToString()) == 1;
            // print(int.Parse(saveString[stringIndex].ToString()));
            // saveSlots[i].curModelIndex = int.Parse(saveString[stringIndex + 1].ToString());
            // saveSlots[i].dam = int.Parse(saveString[stringIndex + 2].ToString());
            stringIndex += 3;
            if (saveSlots[i].hasShooter)
            {
                // print("dd");
                Shooter shooter = InstantiatePF(Slots[i]);
                for (int j = 0; j < saveSlots[i].curModelIndex; j++)
                {
                    shooter.UpGrade();
                }
            }
        }

    }
    int price = 10;
    public Text PriceText;
    public void InstantiatePF()
    {
        if (GameManager.Instance.Coin >= price)
        {
            Slot firstFreeSlot = Slots.Where(x => x.shooter == null).FirstOrDefault();
            if (firstFreeSlot)
            {
                Shooter shooter = Instantiate(shooterPF, firstFreeSlot.transform.position, Quaternion.identity);
                firstFreeSlot.SetShooter(shooter);
            }

            // GameController.Coin = GameController.Coin - price;
            // CanvasController.Instance.HudCoin(GameController.Coin);
            price += 1;
            PriceText.text="Price "+price;
            PlayerPrefs.SetInt("CurrentPrice",price);
        }

    }
    public Shooter InstantiatePF(Slot slot)
    {
        Shooter shooter = Instantiate(shooterPF, slot.transform.position, Quaternion.identity);
        slot.SetShooter(shooter);
        return shooter;
    }
}
