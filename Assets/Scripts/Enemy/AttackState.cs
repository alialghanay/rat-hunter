using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    private GameObject targetPlayer;
    private bool isRetreating = false;
    private float retreatDuration = 2f;
    private float retreatTimer = 0f;

    public AttackState()
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
        isRetreating = false;
        retreatTimer = 0f;
    }

    public override void Preform()
    {
        if (isRetreating)
        {
            Retreat();
        }
        else if (enemy.CanSeePlayer())
        {
            targetPlayer = enemy.player;
            enemy.transform.LookAt(targetPlayer.transform);

            if (targetPlayer != null)
            {
                Attack(targetPlayer);
            }
        }
        else
        {
            stateMachine.ChangeState(new PatrolState());
        }
    }

    public void Attack(GameObject player)
    {
        enemy.Agent.speed = enemy.AttackSpeed;
        enemy.Agent.SetDestination(player.transform.position);

        if (Vector3.Distance(enemy.transform.position, player.transform.position) <= enemy.Agent.stoppingDistance)
        {
            StartRetreating();
        }
    }

    private void StartRetreating()
    {
        isRetreating = true;
        retreatTimer = 0f;
        enemy.Agent.speed = enemy.RetreatSpeed;

        Vector3 retreatDirection = -enemy.transform.forward * 5f;
        Vector3 retreatPosition = enemy.transform.position + retreatDirection;
        enemy.Agent.SetDestination(retreatPosition);
    }

    private void Retreat()
    {
        retreatTimer += Time.deltaTime;
        if (retreatTimer >= retreatDuration)
        {
            isRetreating = false;
            stateMachine.ChangeState(new PatrolState()); 
        }
    }
}
