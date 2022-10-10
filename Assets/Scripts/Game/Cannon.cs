using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform Body, Amsar;
    [SerializeField] Vector3 startRotation, endRotation;
    [SerializeField] float speed = 1;
    Quaternion start, end;
    float time;
    bool Rotate = false;
    private void Start()
    {
        start = Quaternion.Euler(startRotation);
        end = Quaternion.Euler(endRotation);
        Rotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Rotate)
        {
            time += Time.deltaTime;
            Body.localRotation = Quaternion.Lerp(start, end, Mathf.PingPong(time * speed, 1));
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rotate = false;
            StartCoroutine(localCoroutine(other.transform.parent));
            // other.transform.parent.GetComponent<Rigidbody>().AddForce(-Body.transform.forward * 20, ForceMode.VelocityChange);

        }
        IEnumerator localCoroutine(Transform player)
        {
            float time = 0;
            float duration = 0.5f;
            float t;
            Vector3 initialPos = player.position;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            while (time < duration)
            {
                time += Time.deltaTime;
                t = time / duration;
                player.position = Vector3.Lerp(initialPos, Amsar.position, time / duration);
                yield return null;
            }
            player.GetComponent<Rigidbody>().AddForce(-Body.transform.forward * 30, ForceMode.VelocityChange);
            yield return new WaitForSeconds(1);
            Rotate = true;
        }
    }
}
