using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int bulletDamage = 1;
    public Transform Target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position,Target.transform.position,Time.deltaTime*50/*Sumnii hurd ni hu*/);
    }
    public void SetBulletDamage(int dam,Transform myTarget)
    {
        Target=myTarget;
        bulletDamage = dam;
        transform.GetChild(dam - 1).gameObject.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // GameController.Coin += bulletDamage;
            // Data.Coin.Set(GameController.Coin);
            // CanvasController.Instance.HudCoin(GameController.Coin);
            Destroy(gameObject);
        }

    }
}
