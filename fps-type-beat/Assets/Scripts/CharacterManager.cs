using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
  [Header ("Primary Component References")]
  public CharacterController controller; // this character controller

  [Header ("Movement and Gravity")]
  public float speed = 16f;
  public float gravity = -9.81f;
  public float jumpHeight = 5f;
  public Vector3 velocity;
  public float stepOffset = 0.3f; // variable to set the character controller's stair-step offset
  public float xMovement, zMovement;

  [Header ("Ground Checking")]
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;
  public bool isGrounded;

  /*
    Applies character movement to the CharacterController. x, y, z movement should be defined in
    child classes. This funtion should be called in the child's Update() function.
  */
  protected void ApplyMovement() {
    // if character is touching ground, set velocity to a constant:
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    if (isGrounded && velocity.y < 0) {
      velocity.y = -2f;
    }

    // move player horizontally:
    Vector3 movementDir = (transform.right) * xMovement + (transform.forward * zMovement);
    controller.Move(movementDir * speed * Time.deltaTime);

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
}