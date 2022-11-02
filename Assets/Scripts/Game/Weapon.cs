using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collect
{
    [SerializeField] List<GameObject> Models;
    public Vector3 HandPos;
    public Vector3 handRot;
    int currentModelIndex = 0;
    public int GetModelIndex() => currentModelIndex;
    public void UpGrade()
    {
        Models[currentModelIndex].SetActive(false);
        currentModelIndex++;
        SellPrice++;
        Models[currentModelIndex].SetActive(true);
    }
    public void UpGrade(int indx)
    {
        Models[currentModelIndex].SetActive(false);
        currentModelIndex = indx;
        SellPrice = currentModelIndex + 1;
        Models[currentModelIndex].SetActive(true);
    }
    public void BeAtHand(Transform handTransform)
    {
        transform.SetParent(handTransform);
        transform.localPosition = HandPos;
        transform.rotation = Quaternion.Euler(handRot);
    }
    // Start is called before the first frame update


}
