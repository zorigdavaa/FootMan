using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    Color _color;

    public Color GetColor()
    {
        return _color;
    }
    public void SetColor(Color incomingColor)
    {
        transform.GetChild(0).GetComponent<Renderer>().material.color = incomingColor;
        _color = incomingColor;
    }

    internal void SetFree()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Collider>().isTrigger = false;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
    }
    internal void BecomeTrigger()
    {
        StartCoroutine(LocalCoroutine());
        IEnumerator LocalCoroutine()
        {
            yield return new WaitForSeconds(3);
            Vector3 position = transform.position;
            position.y = 0.5f;
            transform.position = position;
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    internal void HideCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
    public virtual void GotoPos(Vector3 toPos, Action afterAction = null)
    {
        StartCoroutine(LocalCoroutine());
        IEnumerator LocalCoroutine()
        {
            float t = 0;
            float time = 0;
            float duration = 1;
            Vector3 initialPosition = transform.position;
            while (time < duration)
            {
                t = time / duration;
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(initialPosition, toPos, t);

                yield return null;
            }
            afterAction?.Invoke();
        }

    }
}
