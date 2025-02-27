using CaseDemo.Scripts.EffectControllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CaseDemo.Scripts.SO_Classes
{
    [CreateAssetMenu(fileName = "so_prize_card", menuName = "Scriptable Objects/so_prize_card", order = 0)]
    public class PrizeCardSo : ScriptableObject
    {
        [field: PreviewField(100), SerializeField] public Sprite CardItemIcon { get; private set; }
        [field: SerializeField] public string CardHeaderText { get; private set; }
        [field: SerializeField] public int CardRewardValue { get; private set; }

        [field: SerializeField] public UnitType UnitType { get; private set; }
    }
}