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

      // if player is touching ground, set velocity to a constant:
      em.isGrounded = Physics.CheckSphere(em.groundCheck.position, em.groundDistance, em.groundMask);
      if (em.isGrounded && em.velocity.y < 0) {
        em.velocity.y = -2f;
      }

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

      // apply gravity over time:
      // deltaY = 1/2*g * t^2 (freefall equation)
      em.velocity.y += em.gravity * Time.deltaTime;
      em.controller.Move(em.velocity * Time.deltaTime);

      // necessary to avoid jump-stuttering, disable step offset while in air:
      if (em.isGrounded) {
        em.controller.stepOffset = em.stepOffset;
      } else {
        em.controller.stepOffset = 0;
      }

      //TODO: define shooting behavior

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
