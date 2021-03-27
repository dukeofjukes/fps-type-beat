using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
  State currentState;

  void Update() {
    RunStateMachine();
  }

  private void RunStateMachine() {
    State nextState = currentState?.RunCurrentState(); // if variable is not null, run current state

    if (nextState != null) {
      SwitchToNextState(nextState);
    }
  }

  private void SwitchToNextState(State nextState) {
    currentState = nextState;
  }
}
