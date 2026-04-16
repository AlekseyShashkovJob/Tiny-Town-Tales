using UnityEngine;
using View.Button;

namespace GameCore.Repository.Button
{
    public class StateButtonRepository : MonoBehaviour
    {
        [SerializeField] private CustomButton _hunger;
        [SerializeField] private CustomButton _dirty;
        [SerializeField] private CustomButton _fatigue;
        [SerializeField] private CustomButton _sick;

        public CustomButton GetHunger() => _hunger;
        public CustomButton GetDirty() => _dirty;
        public CustomButton GetFatigue() => _fatigue;
        public CustomButton GetSick() => _sick;
    }
}