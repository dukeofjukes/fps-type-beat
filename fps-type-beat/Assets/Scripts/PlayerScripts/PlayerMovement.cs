using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Intended for use with first person movement system. 
*/
public class PlayerMovement : CharacterManager {
  void Update() {
    GetInputMovement();
    ApplyMovement(); // inherited from CharacterManager
  }

  /*
    Assign x, y, z with user input.
  */
  private void GetInputMovement() {
    // get player wasd input:
    this.xMovement = Input.GetAxis("Horizontal");
    this.zMovement = Input.GetAxis("Vertical");

    // apply jump velocity:
    if (Input.GetButtonDown("Jump") && isGrounded) {
      velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
  }
}
