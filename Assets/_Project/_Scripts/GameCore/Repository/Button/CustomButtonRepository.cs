using UnityEngine;
using View.Button;

namespace GameCore.Repository.Button
{
    public class CustomButtonRepository : MonoBehaviour
    {
        [SerializeField] private CustomButton _tamagotchiImage;

        private StateButtonRepository _stateButtonRepository;

        private void Awake()
        {
            _stateButtonRepository = GetComponent<StateButtonRepository>();
        }

        public CustomButton GetTamagotchiImage() => _tamagotchiImage;

        public StateButtonRepository GetStateButtonRepository() => _stateButtonRepository;
    }
}