using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Pathfinding;

public class Bot : Character
{
    [SerializeField] List<Collect> myCollect;
    public Transform Target;
    [SerializeField] Transform Chest;
    public BotState state;
    public bool UseAI = false;

    private void Start()
    {
        // Target = FindObjectOfType<Player>().transform;
        // movement.GoToPosition(Target);
        animationController.Set8WayLayerWeight(false);
        animationController.OnAttackEvent += OnAttack;
    }

    public void GotoTarget()
    {
        movement.GoToPosition(Target);
    }
    public void GotoPos(Vector3 pos)
    {
        movement.GoToPosition(pos);
    }

    internal void GoToWar()
    {
        movement.GoToPosition(new Vector3(-10, 0, 30), afterAction: () =>
        {
            UseAI = true;
            state = BotState.Wandering;
        });
    }

    private void OnAttack(object sender, EventArgs e)
    {
        if (Target.GetComponent<Character>().IsAlive && IsAlive)
        {

            if (Vector3.Distance(transform.position, Target.position) < 3)
            {

                Target.GetComponent<Character>().TakeDamage(-20);
            }
            movement.GoToPosition(Target);
            Attacking = false;
            state = BotState.Chasing;
        }
        else
        {
            state = BotState.Wandering;
        }
    }
    bool Attacking = false;
    private void Update()
    {
        if (UseAI)
        {
            switch (state)
            {
                // case BotState.idle: print("idle"); break;
                case BotState.Wandering: Wander(); break;
                // case BotState.Fighting: Attack(); ; break;
                case BotState.Chasing: Chase(); ; break;
                default: break;
            }
        }
    }
    float chaseTimer = 3;
    private void Chase()
    {
        chaseTimer -= Time.deltaTime;
        if (chaseTimer < 0)
        {
            movement.GoToPosition(Target);
            chaseTimer = 3;
        }
        else if (Vector3.Distance(transform.position, Target.position) < 1)
        {
            Attack();
        }
        else if (Vector3.Distance(transform.position, Target.position) > 5)
        {
            state = BotState.Wandering;
        }
    }

    float wanderTime = 3;
    private void Wander()
    {
        wanderTime -= Time.deltaTime;
        FindTarget();
        if (wanderTime < 0)
        {
            wanderTime = 3;
            movement.GoToPosition(transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)));
        }
    }

    public void FindTarget()
    {
        float nearDistance = 100;
        Character nearEnemy = null;
        foreach (var item in Physics.OverlapSphere(transform.position, 5))
        {
            Character enemy = item.GetComponent<Character>();
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (enemy && enemy.Team != Team && distance < nearDistance)
            {
                nearDistance = distance;
                nearEnemy = enemy;
            }
        }
        if (nearEnemy)
        {
            Target = nearEnemy.transform;
            movement.GoToPosition(Target);
            state = BotState.Chasing;
        }
    }

    private void Attack()
    {
        state = BotState.Fighting;
        Attacking = true;
        animationController.Attack();
        movement.Cancel();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Spear>())
        {
            other.transform.SetParent(Chest);
            other.transform.GetComponent<Spear>().Stabbed();
            rb.AddForce(other.relativeVelocity.normalized * 300);
            TakeDamage(-60);
        }
    }
    public override void Die()
    {
        base.Die();
        // rb.isKinematic = true;
        FindObjectOfType<Player>().IncreaseKillCount();
    }
}
public enum BotState
{
    idle, Wandering, Fighting, Chasing
}