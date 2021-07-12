using System.Collections;
using UnityEngine;

namespace Prototype02
{
    public abstract class State
    {
        public virtual IEnumerator Init()
        {
            yield break;
        }

        public virtual IEnumerator Attack()
        {
            yield break;
        }

        public virtual IEnumerator GetHurt(Vector2 backOff)
        {
            yield break;
        }

        public virtual IEnumerator Move(Vector2 offset)
        {
            yield break;
        }
        
        public virtual IEnumerator Jump(Vector2 velocityOffset)
        {
            yield break;
        }
        
        public virtual IEnumerator Idle()
        {
            yield break;
        }
    }

    public abstract class StateMachine : MonoBehaviour
    {
        protected State State;

        public void SetState(State newState)
        {
            State = newState;
            StartCoroutine(newState.Init());
        }
    }
}