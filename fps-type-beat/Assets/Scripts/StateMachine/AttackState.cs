using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State {
  public IdleState idleState;

  public override State RunCurrentState(EnemyManager em) {
    HandleMovement(em);
    //HandleShooting(em);

    // handle state switching:
    if (em.distanceFromTarget > em.attackRadius) {
      return idleState; // switch states
    } else {
      return this; // remain in this state
    }
  }

  void HandleMovement(EnemyManager em) {
    // if player is touching ground, set velocity to a constant:
    em.isGrounded = Physics.CheckSphere(em.groundCheck.position, em.groundDistance, em.groundMask);
    if (em.isGrounded && em.velocity.y < 0) {
      em.velocity.y = -2f;
    }

    // randomize movement:
    float x, z;
    if (em.distanceFromTarget > em.stopPursuitRadius) {
      z = (int)Random.Range(-1, 1);
    } else {
      z = (int)Random.Range(0, 1);
    }
    x = (int)Random.Range(-1, 1);
    Vector3 movementDir = transform.right * x + transform.forward * z;
    em.controller.Move(movementDir * em.speed * Time.deltaTime);

    if (Random.value > 0.5) {
      em.velocity.y = Mathf.Sqrt(em.jumpHeight * -2f * em.gravity);
    }

    // necessary to avoid jump-stuttering, disable step offset while in air:
    if (em.isGrounded) {
      em.controller.stepOffset = em.stepOffset;
    } else {
      em.controller.stepOffset = 0;
    }
  }

  //TODO: define shooting behavior
  // void HandleShooting(EnemyManager em) {}
}
