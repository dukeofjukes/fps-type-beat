using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  [Header ("Primary Component References")]
  public CharacterController controller; // this enemy's character controller
  public State currentState; // holds starting state, displays current state during runtime
  public Transform target; // the player's transform

  [Header ("State Detection Triggers")]
  public float distanceFromTarget; // tracks the current distance from target
  public float viewRadius = 10f; // detection radius
  public float viewAngle = 70; // detection field-of-view angle (in degrees)
  public float attackRadius = 20f; // distance player has to escape to end attack state
  public float stopPursuitRadius = 5f; // "personal space" distance around the player, enemy will stop pursuit at this radius
  public LayerMask viewMask; // TODO: assign this in editor (Environment mask?)

  [Header ("Movement and Gravity")]
  public float speed = 16f;
  public float gravity = -9.81f;
  public float jumpHeight = 5f;
  public Vector3 velocity;
  public float stepOffset = 0.3f; // variable to set the character controller's stair-step offset

  [Header ("Idle Path Following")]
  public Transform path; // idle patrolling path
  public Vector3[] waypoints; // hold invididual waypoints. populated in Start() and utilized in IdleState
  public float followPathWaitTime = .3f; // time to wait between path nodes (idle state)

  [Header ("Ground Checking")]
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;
  public bool isGrounded;

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

    // apply gravity over time:
    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);

    HandleStateMachine();
  }

  /*
    Run the current state, and if a next state is returned, switch to it.
  */
  private void HandleStateMachine() {
    State nextState = currentState?.RunCurrentState(this); // if variable is not null, run current state

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
