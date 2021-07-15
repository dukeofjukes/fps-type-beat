using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: this entire file has the potential to be refactored to utilize the CharacterManager class
// depends on gun object references and such. especially when it comes to the shooting visual effect in 
// the camera.
public class RaycastShoot : MonoBehaviour {
  public int gunDamage = 1;
  public float fireRate = .25f;
  public float weaponRange = 50f;
  public float hitForce = 100f;
  public Transform gunEnd;

  private Camera fpsCam;
  private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
  private AudioSource gunAudio;
  private LineRenderer laserLine;
  private float nextFire;

  void Start() {
    gunAudio = GetComponent<AudioSource>();
    laserLine = GetComponent<LineRenderer>();
    fpsCam = GetComponentInParent<Camera>();
  }

  void Update() {
    if (Input.GetButtonDown("Fire1") && Time.time > nextFire) {
      nextFire = Time.time + fireRate;

      StartCoroutine(ShotEffect());

      Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
      RaycastHit hit;

      laserLine.SetPosition(0, gunEnd.position);

      if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange)) {
        laserLine.SetPosition(1, hit.point);

        // handle health damage to shootable objects:
        ShootableBox health = hit.collider.GetComponent<ShootableBox>();
        if (health != null) {
          health.Damage(gunDamage);
        }
        if (hit.rigidbody != null) {
          hit.rigidbody.AddForce(-hit.normal * hitForce);
        }
      } else {
        laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
      }
    }
  }

  private IEnumerator ShotEffect() {
    gunAudio.Play();

    laserLine.enabled = true;
    yield return shotDuration;
    laserLine.enabled = false;
  }
}
