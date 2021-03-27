using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* NOTE: variables taken from PlayerMovement script (should this be a subclass?) */
// TODO: yes, create a parent class for shared variables between player and enemy

public class EnemyMovement : MonoBehaviour {

  public CharacterController controller;
  public Transform targetPosition;

  // horizontal movement and gravity variables:
  public float speed = 16f;
  public float gravity = -9.81f;
  public float jumpHeight = 5f;
  Vector3 velocity;
  public float stepOffset = 0.3f; // variable to set the character controller's stair-step offset

  // ground check variables:
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;
  bool isGrounded;

  // TODO: need to define idle vs. movement state (just a bool set true by some radial trigger?)
  void Update() {
    // if touching ground, set velocity to a constant (to stay grounded):
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    if (isGrounded && velocity.y < 0) {
      velocity.y = -2f;
    }

    Vector3 movementDir = transform.right + transform.forward; // TODO: call pathfinding function here
    controller.Move(movementDir * speed * Time.deltaTime);

    // TODO: randomize jumping (during attack state)

    // apply gravity over time:
    // deltaY = 1/2*g * t^2 (freefall equation)
    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);

    // necessary to avoid jump-stuttering, disable step offset while in air:
    if (isGrounded) {
      controller.stepOffset = stepOffset;
    } else {
      controller.stepOffset = 0;
    }
  }

  // TODO: define pathfinding function? returns a Vector3 for movement direction
  // needs to randomize as well
  
}
