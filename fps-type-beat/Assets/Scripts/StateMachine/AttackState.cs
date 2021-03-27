using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State {
  public IdleState idleState;
  public bool isOutOfAttackRange;

  public override State RunCurrentState() {
    // TODO: check for attack range
    // TODO: define movement and shooting behavior

    if (isOutOfAttackRange) {
      return idleState; // switch states
    } else {
      return this; // remain in this state
    }
  }
}
