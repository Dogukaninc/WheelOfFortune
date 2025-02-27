using CaseDemo.Scripts.EffectControllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CaseDemo.Scripts.SO_Classes
{
    [CreateAssetMenu(fileName = "so_prize_unit_config", menuName = "Scriptable Objects/so_prize_unit_config", order = 0)]
    public class UiPrizeUnitSo : ScriptableObject
    {
        [field: SerializeField] public int UnitSpawnCount { get; private set; }
        [field: SerializeField] public UnitType UnitType { get; private set; }
        [field: PreviewField(100), SerializeField] public Sprite UnitSprite { get; private set; }
    }
}