using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager {
  [Header ("Enemy-specific Component References")]
  public State currentState; // holds starting state, displays current state during runtime
  public Transform target; // the player's transform

  [Header ("State Detection Triggers")]
  public float distanceFromTarget; // tracks the current distance from target
  public float viewRadius = 20f; // detection radius
  public float viewAngle = 70f; // detection field-of-view angle (in degrees)
  public float attackRadius = 50f; // distance player has to escape to end attack state
  public float stopPursuitRadius = 10f; // "personal space" distance around the player, enemy will stop pursuit at this radius
  public LayerMask viewMask; // TODO: assign this in editor (Environment mask?)

  [Header ("Idle Path Following")]
  public Transform path; // idle patrolling path
  public Vector3[] waypoints; // hold invididual waypoints. populated in Start() and utilized in IdleState
  public float followPathWaitTime = .3f; // time to wait between path nodes (idle state)

  void Start() {
    // populate waypoints array with path waypoints:
    waypoints = new Vector3[path.childCount];
    for (int i = 0; i < waypoints.Length; i++) {
      waypoints[i] = path.GetChild(i).position;
      //waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
    }
  }

  /*
    Perform global actions and refresh variables that aren't dependent on a state.
  */
  void Update() {
    distanceFromTarget = Vector3.Distance(target.position, transform.position); // refresh distance
    HandleStateMachine();
    ApplyMovement(); // inherited from CharacterManager
  }

  /*
    Run the current state, and if a next state is returned, switch to it.
  */
  private void HandleStateMachine() {
    State nextState = currentState?.RunCurrentState(); // if variable is not null, run current state

    if (nextState != null) {
      SwitchToNextState(nextState);
    }
  }

  /*
    Switch to a given state. Called in HandleStateMachine().
  */
  private void SwitchToNextState(State nextState) {
    currentState = nextState;
  }

  void OnDrawGizmos() {
    Vector3 startPosition = path.GetChild(0).position;
    Vector3 previousPosition = startPosition;

    foreach (Transform waypoint in path) {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(waypoint.position, 1);
      Gizmos.color = Color.white;
      Gizmos.DrawLine(previousPosition, waypoint.position);
      previousPosition = waypoint.position;
    }
    Gizmos.DrawLine(previousPosition, startPosition);
  }
}
