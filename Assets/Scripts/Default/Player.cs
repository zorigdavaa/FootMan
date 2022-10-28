using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZPackage;
using ZPackage.Helper;
using Random = UnityEngine.Random;
using UnityEngine.Pool;

public class Player : Character
{
    [SerializeField] List<GameObject> SkillPrefabs;
    Abilities AbilitiesScript;
    ObjectPool<Spear> Pool;
    [SerializeField] Spear Spear;
    CameraController cameraController;
    SoundManager soundManager;
    UIBar bar;
    URPPP effect;
    int killCount;
    int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        AbilitiesScript = FindObjectOfType<Abilities>(true);
        movement = GetComponent<MovementEggRun>();
        animationController.OnSpearShoot += SpearShoot;
        soundManager = FindObjectOfType<SoundManager>();
        cameraController = FindObjectOfType<CameraController>();
        // line.positionCount = LineResolution;
        effect = FindObjectOfType<URPPP>();
        // bar = FindObjectOfType<UIBar>();
        // bar.gameObject.SetActive(false);
        GameManager.Instance.GameOverEvent += OnGameOver;
        GameManager.Instance.GamePlay += OnGamePlay;
        GameManager.Instance.LevelCompleted += OnGameOver;
        InitPool();
        AbilitiesScript.GetAbilitieByIndex(0).OnSkillUsed = Skill0;
        AbilitiesScript.GetAbilitieByIndex(1).OnSkillUsed = Skill1;
        AbilitiesScript.GetAbilitieByIndex(2).OnSkillUsed = Skill2;
        GameManager.Instance.Coin = 10;
        // foreach (var item in FindObjectsOfType<ZombieSpawner>())
        // {
        //     enemyCount += item.zombieCount;
        // }
        // cameraController.Zoom(0.5f, 20, () => cameraController.Zoom(1, 60));
    }

    private void Skill0(object sender, EventArgs e)
    {
        GameObject InstantiatedFireBall = Instantiate(SkillPrefabs[0], transform.position + Vector3.up * 2, Quaternion.identity);
        Physics.IgnoreCollision(GetComponent<Collider>(), InstantiatedFireBall.GetComponentInChildren<Collider>());
        InstantiatedFireBall.transform.rotation = transform.rotation;

        // Physics.CheckCapsule(transform.position, transform.position + transform.forward * 5, 2, 1 << 3);//only 3 layer
    }

    internal void IncreaseKillCount()
    {
        killCount++;
        // if (killCount >= enemyCount)
        // {
        //     GameManager.Instance.LevelComplete(this, 0);
        // }

    }

    private void Skill1(object sender, EventArgs e)
    {
        GameObject InstantiatedFireBall = Instantiate(SkillPrefabs[1], transform.position + Vector3.up * 3, Quaternion.identity, transform);
        // Physics.IgnoreCollision(GetComponent<Collider>(), InstantiatedFireBall.GetComponentInChildren<Collider>());
        InstantiatedFireBall.transform.rotation = transform.rotation;

    }
    private void Skill2(object sender, EventArgs e)
    {
        GameObject InstantiatedFireBall = Instantiate(SkillPrefabs[2], transform.position + transform.forward * 10, Quaternion.identity);
        // Physics.IgnoreCollision(GetComponent<Collider>(), InstantiatedFireBall.GetComponentInChildren<Collider>());
        InstantiatedFireBall.transform.rotation = transform.rotation;
    }

    private void Update()
    {
        if (IsAlive)
        {
            FindNearestEnemy();
        }
    }
    [SerializeField] Transform Target = null;

    private void FindNearestEnemy()
    {
        float shortest = 100;
        Transform nearest = null;
        foreach (var item in Physics.OverlapSphere(transform.position, 10, 1 << 3))
        {
            float Distance = Vector3.Distance(transform.position, item.transform.position);
            if (shortest > Distance)
            {
                nearest = item.transform;
                shortest = Distance;
            }
        }

        Target = nearest;
        movement.LookTarget = Target;
        if (Target)
        {
            animationController.Throw();
        }
    }

    private void InitPool()
    {
        Pool = new ObjectPool<Spear>(() =>
        {
            Spear spear = Instantiate(Spear, Spear.transform.position, Spear.transform.rotation);
            spear.SetPool(Pool);
            return spear;
        }, (s) =>
        {
            s.transform.position = Spear.transform.position;
            // s.transform.rotation = Spear.transform.rotation;
            if (Target)
            {
                s.Throw(Target);
            }
            else
            {
                s.Throw(transform.forward);
            }
        }, (s) =>
        {
            s.GotoPool();
        });
    }

    Coroutine spearCor;
    private void SpearShoot(object sender, EventArgs e)
    {
        if (spearCor == null)
        {
            // Spear instantiatedSpear = Instantiate(Spear, Spear.transform.position, Spear.transform.rotation);
            // Spear.gameObject.SetActive(false);
            // instantiatedSpear.Throw(transform.forward);
            Spear spear = Pool.Get();
            // spear.GotoPool()

            spearCor = StartCoroutine(localCoroutine(spear));
        }

        IEnumerator localCoroutine(Spear spear)
        {
            Spear.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            // float time = 0;
            // float duration = 0.5f;
            // float t = 0;
            // Vector3 initPos = spear.position;
            // Vector3 endPos = transform.position + transform.forward * 10;
            // while (time < duration)
            // {
            //     time += Time.deltaTime;
            //     t = time / duration;
            //     // Mathf.Lerp(0, 1, t);
            //     spear.position = Vector3.Lerp(initPos, endPos, t);
            //     yield return null;
            // }
            Spear.gameObject.SetActive(true);
            spearCor = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Collect collect = other.GetComponent<Collect>();
        if (collect)
        {
            inventory.AddInventory(collect.gameObject);
        }
    }
    public override void Die()
    {
        base.Die();
        GameManager.Instance.GameOver(this, EventArgs.Empty);
    }

    private void OnGamePlay(object sender, EventArgs e)
    {
        movement.SetSpeed(1);
        movement.SetControlAble(true);
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        // throw new NotImplementedException();
    }
}
