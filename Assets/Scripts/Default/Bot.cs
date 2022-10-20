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
    public bool UseAI;

    private void Start()
    {
        // Target = FindObjectOfType<Player>().transform;
        // movement.GoToPosition(Target);
        animationController.Set8WayLayerWeight(false);
        animationController.OnAttackEvent += OnAttack;
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
        }
        else
        {
            movement.GoToPosition(transform.position + Random.insideUnitSphere * 5);
        }
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
        movement.GoToPosition(new Vector3(-10, 0, 30), afterAction: () => FindTarget());
    }

    private void OnAttack(object sender, EventArgs e)
    {
        if (Target.GetComponent<Character>().IsAlive && IsAlive)
        {

            if (Vector3.Distance(transform.position, Target.position) < 6)
            {

                Target.GetComponent<Character>().TakeDamage(-20);
            }
            movement.GoToPosition(Target);
            Attacking = false;
        }
    }
    bool Attacking = false;
    private void Update()
    {
        if (UseAI)
        {
            switch (state)
            {
                case BotState.idle: print("idle"); break;
                case BotState.Wandering: FindTarget(); break;
                case BotState.Fighting: Attack(); ; break;
                case BotState.Chasing: Attack(); ; break;
                default: break;
            }
        }
    }

    private void Attack()
    {
        if (Target && Vector3.Distance(Target.position, transform.position) < 4 && !Attacking && IsAlive)
        {
            Attacking = true;
            animationController.Attack();
            movement.Cancel();
        }
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