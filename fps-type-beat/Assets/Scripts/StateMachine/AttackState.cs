using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State {
  public IdleState idleState;
  public bool isOutOfAttackRange;

  public override State RunCurrentState(EnemyManager em) {
    em.distanceFromTarget = Vector3.Distance(em.target.position, transform.position);
    
    if (em.distanceFromTarget < em.attackRadius) {
      isOutOfAttackRange = false;
      // TODO: define movement and shooting behavior

    } else {
      isOutOfAttackRange = true;
    }

    // handle state switching:
    if (isOutOfAttackRange) {
      return idleState; // switch states
    } else {
      return this; // remain in this state
    }
  }
}
