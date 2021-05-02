using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {
  public AttackState attackState; // reference to the attack state

  public override State RunCurrentState(EnemyManager em) {
    //StartCoroutine(FollowPath(em.waypoints, em));

    // handle state switching:
    if (CanSeePlayer(em)) {
      return attackState;
    } else {
      return this;
    }
  }

  /*
    Determines whether the enemy can see the player, and returns the result.
  */
  bool CanSeePlayer(EnemyManager em) {
    if (em.distanceFromTarget < em.viewRadius) { // check distance
      Vector3 dirToTarget = (em.target.position - em.transform.position).normalized;
      float angleBetweenEnemyAndTarget = Vector3.Angle(em.transform.forward, dirToTarget);

      if (angleBetweenEnemyAndTarget < em.viewAngle / 2f) { // check view angle
        if (!Physics.Linecast(em.transform.position, em.target.transform.position, em.viewMask)) { // check if view is obstructed
          return true;
        }
      }
    }
    return false;
  }

  /*
    Continuously follow idle patrol path defined in editor.
  */
  IEnumerator FollowPath(Vector3[] waypoints, EnemyManager em) {
    em.transform.position = waypoints[0];

    int targetWaypointIndex = 1;
    Vector3 targetWaypoint = waypoints[targetWaypointIndex];

    while (true) {
      em.transform.position = Vector3.MoveTowards(em.transform.position, targetWaypoint, em.speed * Time.deltaTime);
      if (em.transform.position == targetWaypoint) {
        targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
        targetWaypoint = waypoints[targetWaypointIndex];
        yield return new WaitForSeconds(em.followPathWaitTime);
      }
      yield return null;
    }
  }
}
