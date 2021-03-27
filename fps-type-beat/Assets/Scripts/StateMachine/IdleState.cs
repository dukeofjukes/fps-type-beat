using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {
  public AttackState attackState; // reference to the attack state
  public bool canSeePlayer;

  public override State RunCurrentState() {
    // TODO: wait for player to enter range/fov

    // switch to attack state if target is found:
    if (canSeePlayer) {
      return attackState;
    } else {
      return this;
    }
  }
}
