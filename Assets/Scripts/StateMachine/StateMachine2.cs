using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionGame
{
    public abstract class StateMachine2 : MonoBehaviour
    {
        private State currentState;

        public void SwitchState(State newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        private void Update()
        {
            currentState?.Tick(Time.deltaTime);
            //Debug.Log(currentState);
        }
    }
}

