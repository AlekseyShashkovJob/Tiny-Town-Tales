using UnityEngine;

namespace GameCore.Shop
{
    [CreateAssetMenu(fileName = "SkinData", menuName = "Game/Skin Data")]
    public class SkinData : ScriptableObject
    {
        [field: SerializeField] public string SkinId { get; private set; }
        [field: SerializeField] public string SkinName { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite PetSprite { get; private set; }
        //[field: SerializeField] public Sprite SkinPreview { get; private set; }
        [field: SerializeField, TextArea(3, 8)] public string StoryText { get; private set; }
        [field: SerializeField] public Color IngotColor { get; private set; } = Color.white;
    }
}