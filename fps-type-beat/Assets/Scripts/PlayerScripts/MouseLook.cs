using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/*
 * Intended for use with the first person camera.
 */
public class MouseLook : MonoBehaviour {
  public Transform playerBody;
  public float mouseSensitivity = 100f;
  private float xRotation = 0f;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update() {
    // get mouse input:
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    // rotate the player's body when mouse moves along x-axis:
    playerBody.Rotate(Vector3.up * mouseX);

    // rotate player's camera when mouse moves along y-axis:
    xRotation -= mouseY; // has to be negative
    xRotation = Mathf.Clamp(xRotation, -90f, 90f); // prevent overrotation
    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
  }
}
