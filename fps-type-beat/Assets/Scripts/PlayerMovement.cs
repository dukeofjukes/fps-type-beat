﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Intended for use with first person movement system. 
*/
public class PlayerMovement : MonoBehaviour {
  public CharacterController controller;

  // horizontal movement and gravity variables:
  public float speed = 12f;
  public float gravity = -9.81f;
  public float jumpHeight = 3f;
  Vector3 velocity;

  // ground check variables:
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;
  bool isGrounded;

  void Update() {
    // if player is touching ground, set velocity to a constant:
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    if (isGrounded && velocity.y < 0) {
      velocity.y = -2f;
    }

    // get player wasd input:
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    // move player horizontally:
    Vector3 movementDir = transform.right * x + transform.forward * z;
    controller.Move(movementDir * speed * Time.deltaTime);

    if (Input.GetButtonDown("Jump") && isGrounded) {
      velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    // apply gravity over time:
    // deltaY = 1/2*g * t^2 (freefall equation)
    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);
  }
}
