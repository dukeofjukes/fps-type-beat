using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State {
  public PatrolState patrolState;

  public override State RunCurrentState() {
    SetRandomMovement();
    //SetAim();
    //HandleShooting(enemyManager);

    // handle state switching:
    if (enemyManager.distanceFromTarget > enemyManager.attackRadius) {
      return patrolState; // switch states
    } else {
      return this; // remain in this state
    }
  }

  /*
    Assign x, y, z with random movement.
  */
  private void SetRandomMovement() {
    // FIXME: something here isn't working right. agent keeps jumping (like always (need a cooldown timer?)).
    //        agent also never moves to their right. weird.
    // randomize movement:
    if (enemyManager.distanceFromTarget > enemyManager.stopPursuitRadius) { // outside of stopPursuitRadius
      enemyManager.zMovement = (int)Random.Range(0, 1);
    } else { // within stopPursuitRadius "personal space"
      enemyManager.zMovement = (int)Random.Range(-1, 0);
    }
    enemyManager.xMovement = (int)Random.Range(-1, 1); // strafe left or right

    // randomize jumping:
    if (Random.value > 0.5 && enemyManager.isGrounded) {
      enemyManager.velocity.y = Mathf.Sqrt(enemyManager.jumpHeight * -2f * enemyManager.gravity);
    }
  }

  /*
    While attacking, always face the player.
  */
  private void SetAim() {
    //TODO: face the player (left off here!!!)
  }

  //TODO: define shooting behavior
  // private void HandleShooting() {}
}
