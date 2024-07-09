using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waittimer;
    public override void Enter()
    {

    }
    public override void Preform()
    {
        PertrolCycle();
        if(enemy.CanSeePlayer()) stateMachine.ChangeState(new AttackState());
    }
    public override void Exit()
    {

    }

    public void PertrolCycle() {
        if(enemy.Agent.remainingDistance < 0.2f){
            waittimer += Time.deltaTime;
            if(waittimer > 3) {
            if(waypointIndex < enemy.path.waypoints.Count - 1){
                waypointIndex++;
            } else {
                waypointIndex = 0;
            }
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waittimer = 0;
            }
        }
    }
}
