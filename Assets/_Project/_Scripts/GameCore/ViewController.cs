using UnityEngine;

namespace GameCore.Repository
{
    public class ViewController : MonoBehaviour
    {
        [SerializeField] private ViewRepository.ViewRepository _viewRepository;

        public void UpdateUI(Pet.Pet tamagotchi, int coins)
        {
            _viewRepository.GetCoins().text = $"{coins}";

            //_viewRepository.GetInformationViewRepository().GetLevel()
            //    .text = $"{tamagotchi.Level.Lvl} lvl";
            _viewRepository.GetInformationViewRepository().GetHappinies()
                .fillAmount = tamagotchi.Happy.GetHappinessCoef();
            _viewRepository.GetInformationViewRepository().GetExperience()
                .fillAmount = tamagotchi.Level.GetExperienceCoef();

            _viewRepository.GetStateViewRepository().GetHungerState()
                .fillAmount = tamagotchi.GetState(Pet.Pet.HUNGER_STATE).GetStateCoef();
            _viewRepository.GetStateViewRepository().GetDirtyState()
                .fillAmount = tamagotchi.GetState(Pet.Pet.DIRTY_STATE).GetStateCoef();
            _viewRepository.GetStateViewRepository().GetFatigueState()
                .fillAmount = tamagotchi.GetState(Pet.Pet.FATIGUE_STATE).GetStateCoef();
            _viewRepository.GetStateViewRepository().GetSickState()
               .fillAmount = tamagotchi.GetState(Pet.Pet.SICK_STATE).GetStateCoef();
        }
    }
}