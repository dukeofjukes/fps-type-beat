using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBox : MonoBehaviour {
  public int currentHealth = 3;
  private AudioSource deathAudio;

  void Start() {
    deathAudio = GetComponent<AudioSource>();
  }
  
  public void Damage(int damageAmount) {
    currentHealth -= damageAmount;
    
    if (currentHealth <= 0) {
      deathAudio.Play();
      gameObject.SetActive(false);
    }
  }
}
