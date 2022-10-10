using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using ZPackage;

public class Spear : MonoBehaviour
{
    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    ObjectPool<Spear> Pool;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void Throw(Vector3 direction)
    {
        gameObject.SetActive(true);
        transform.rotation = Quaternion.LookRotation(direction);
        rb.isKinematic = false;
        capsuleCollider.isTrigger = false;
        rb.AddForce(direction * 50, ForceMode.VelocityChange);
        Invoke(nameof(Release), 3);
    }
    public void Throw(Transform target)
    {
        Vector3 direction = ((target.position + Vector3.up * 3) - transform.position).normalized;
        gameObject.SetActive(true);
        transform.LookAt(target.position + Vector3.up * 3);
        rb.isKinematic = false;
        capsuleCollider.isTrigger = false;
        rb.AddForce(direction * 50, ForceMode.VelocityChange);
        Invoke(nameof(Release), 3);
    }

    internal void GotoPool()
    {
        rb.isKinematic = true;
        gameObject.SetActive(false);
        capsuleCollider.isTrigger = true;
    }
    internal void Stabbed()
    {
        rb.isKinematic = true;
        capsuleCollider.isTrigger = true;
        stabbed = true;
    }
    bool stabbed = false;
    public void SetPool(ObjectPool<Spear> pool)
    {
        if (pool == null)
        {
            print("pool null");
        }
        this.Pool = pool;
    }
    void Release()
    {
        if (stabbed == false)
        {
            Pool.Release(this);
        }
    }
}
