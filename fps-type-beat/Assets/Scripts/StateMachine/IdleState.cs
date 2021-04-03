using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {
  public AttackState attackState; // reference to the attack state

  //public float waitTime = .3f;

  public override State RunCurrentState(EnemyManager em) {
    // handle state switching:
    if (CanSeePlayer(em)) {
      return attackState;
    } else {
      return this;
    }
  }

  bool CanSeePlayer(EnemyManager em) {
    em.distanceFromTarget = Vector3.Distance(em.target.position, transform.position);

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

  /* TODO: make enemy follow a path, and return to it if it switches from attack state
  IEnumerator FollowPath(Vector3[] waypoints, EnemyManager em) {
    em.transform.position = waypoints[0];

    int targetWaypointIndex = 1;
    Vector3 targetWaypoint = waypoints[targetWaypointIndex];

    while (true) {
      em.transform.position = Vector3.MoveTowards(em.transform.position, targetWaypoint, em.speed * Time.deltaTime);
      if (em.transform.position == targetWaypoint) {
        targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
        targetWaypoint = waypoints[targetWaypointIndex];
        yield return new WaitForSeconds(waitTime);
      }
      yield return null;
    }
  }
  */
}
