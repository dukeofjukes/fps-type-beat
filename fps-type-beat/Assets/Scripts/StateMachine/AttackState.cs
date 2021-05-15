using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State {
  public PatrolState patrolState;

  private float strafeTimer;
  private float jumpTimer;

  public override State RunCurrentState() {
    SetRandomMovement();
    Vector3 lookAtTarget = new Vector3(enemyManager.target.position.x,
                                       enemyManager.transform.position.y, // lock y axis
                                       enemyManager.target.position.z);
    enemyManager.transform.LookAt(lookAtTarget);
    //HandleShooting(enemyManager);

    // handle state switching:
    if (enemyManager.targetDistance > enemyManager.attackRadius) {
      return patrolState; // switch states
    } else {
      return this; // remain in this state
    }
  }

  /*
    Assign x, y, z with random movement.
  */
  private void SetRandomMovement() {
    // set timers:
    strafeTimer -= Time.deltaTime;
    jumpTimer -= Time.deltaTime;

    // randomize movement:
    if (enemyManager.targetDistance > enemyManager.stopPursuitRadius) { // outside of stopPursuitRadius, follow
      enemyManager.zMovement = (Random.value > 0.5) ? 0 : 1;
    } else { // within stopPursuitRadius "personal space", back up
      enemyManager.zMovement = (Random.value > 0.5) ? 0 : -1;
    }

    if (strafeTimer <= 0 && enemyManager.isGrounded) { // only switch strafe directions when grounded
      enemyManager.xMovement = (Random.value > 0.5) ? -1 : 1;
      strafeTimer = enemyManager.timerStrafeInterval;
    }

    if (jumpTimer <= 0) {
      // randomize jumping:
      if (Random.value > 0.5 && enemyManager.isGrounded) {
        enemyManager.velocity.y = Mathf.Sqrt(enemyManager.jumpHeight * -2f * enemyManager.gravity);
      }
      jumpTimer = enemyManager.timerJumpInterval;
    }
  }

  //TODO: define shooting behavior
  // private void HandleShooting() {}
}
