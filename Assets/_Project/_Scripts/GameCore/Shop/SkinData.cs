using UnityEngine;

namespace GameCore.Shop
{
    [CreateAssetMenu(fileName = "SkinData", menuName = "Game/Skin Data")]
    public class SkinData : ScriptableObject
    {
        [field: SerializeField] public string SkinId { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite PetSprite { get; private set; }
    }
}