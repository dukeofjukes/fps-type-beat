using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour {
  public EnemyManager enemyManager;

  public abstract State RunCurrentState();
}
