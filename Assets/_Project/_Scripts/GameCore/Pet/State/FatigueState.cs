using UnityEngine;

namespace GameCore.Pet.State
{
    public class FatigueState : IState
    {
        public float State { get; set; }

        public FatigueState(float value)
        {
            State = value;
        }

        public float GetRandomDecreaseRate() => Random.Range(-6.0f, -3.0f);
    }
}