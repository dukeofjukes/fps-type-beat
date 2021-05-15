using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State {
  public AttackState attackState;

  public override State RunCurrentState() {
    //StartCoroutine(FollowPath(enemyManager.waypoints));

    // handle state switching:
    if (CanSeePlayer()) {
      return attackState;
    } else {
      return this;
    }
  }

  /*
    Determines whether the enemy can see the player, and returns the result.
  */
  bool CanSeePlayer() {
    if (enemyManager.targetDistance < enemyManager.viewRadius) { // check distance
      float angleBetweenEnemyAndTarget = Vector3.Angle(enemyManager.transform.forward, enemyManager.targetDirection);

      if (angleBetweenEnemyAndTarget < enemyManager.viewAngle / 2f) { // check view angle
        if (!Physics.Linecast(enemyManager.transform.position, enemyManager.target.transform.position, enemyManager.viewMask)) { // check if view is obstructed
          return true;
        }
      }
    }
    return false;
  }

  /*
    Continuously follow idle patrol path defined in editor.
    FIXME: kinda broken
  */
  IEnumerator FollowPath(Vector3[] waypoints) {
    enemyManager.transform.position = waypoints[0];

    int targetWaypointIndex = 1;
    Vector3 targetWaypoint = waypoints[targetWaypointIndex];

    while (true) {
      enemyManager.transform.position = Vector3.MoveTowards(enemyManager.transform.position, targetWaypoint, enemyManager.speed * Time.deltaTime);
      if (enemyManager.transform.position == targetWaypoint) {
        targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
        targetWaypoint = waypoints[targetWaypointIndex];
        yield return new WaitForSeconds(enemyManager.followPathWaitTime);
      }
      yield return null;
    }
  }
}
