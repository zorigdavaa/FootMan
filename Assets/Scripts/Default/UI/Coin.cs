using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Amount;
    float rotateSpeed;
    private void Start() {
        rotateSpeed = Settings.Instance.CoinRotationSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime,0 );
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.GetComponent<Money>() != null)
    //     {
    //         other.gameObject.GetComponent<Money>().IncreaseTempMoneyByAmount(Amount,transform.position);
    //         Destroy(gameObject);
            
    //     }
    // }
    
}
