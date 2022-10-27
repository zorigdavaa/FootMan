using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collect
{
    [SerializeField] List<GameObject> Models;
    public Vector3 HandPos;
    int currentModelIndex = 0;
    public void UpGrade()
    {
        Models[currentModelIndex].SetActive(false);
        currentModelIndex++;
        Models[currentModelIndex].SetActive(true);
    }
    // Start is called before the first frame update


}
