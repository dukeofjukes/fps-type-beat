using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  public CharacterController controller;
  public State currentState;
  public Transform target; // holds the player's transform
  public Transform path; // holds idle patrolling path

  public float distanceFromTarget;
  public float viewRadius = 10f; // detection radius
  public float viewAngle; // detection fov angle
  public float attackRadius = 20f; // distance player has to escape to end attack state
  public float stopPursuitRadius = 5f;
  public LayerMask viewMask;

  // horizontal movement and gravity variables:
  public float speed = 16f;
  public float gravity = -9.81f;
  public float jumpHeight = 5f;
  public Vector3 velocity;
  public float stepOffset = 0.3f; // variable to set the character controller's stair-step offset

  // ground check variables:
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;
  public bool isGrounded;

  void Update() {
    HandleStateMachine();
  }

  private void HandleStateMachine() {
    State nextState = currentState?.RunCurrentState(this); // if variable is not null, run current state

    if (nextState != null) {
      SwitchToNextState(nextState);
    }
  }

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
