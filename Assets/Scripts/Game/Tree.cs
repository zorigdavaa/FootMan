using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] List<Transform> applePoint;
    [SerializeField] GameObject apple;
    float appleInsTime = 3;
    bool StartInstantiate = false;

    // Start is called before the first frame update
    void Start()
    {
        StartInstantiate = true;
        // InstantiateApple();
    }

    private void InstantiateApple()
    {
        Vector3 pos = applePoint[Random.Range(0, applePoint.Count)].position;
        Instantiate(apple, pos, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartInstantiate)
        {
            appleInsTime -= Time.deltaTime;
            if (appleInsTime < 0)
            {
                InstantiateApple();
                appleInsTime = 3;
            }
        }
    }
}
