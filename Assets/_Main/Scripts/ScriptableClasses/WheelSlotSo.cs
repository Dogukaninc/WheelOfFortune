using Assets._Main.Scripts.Utilities.PrizeCardSoHolder;
using CaseDemo.Scripts.EffectControllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CaseDemo.Scripts.SO_Classes
{
    [CreateAssetMenu(fileName = "so_wheel_slot_config", menuName = "Scriptable Objects/so_wheel_slot_config", order = 0)]
    public class WheelSlotSo : ScriptableObject
    {
        [field: PreviewField(100), SerializeField]
        public Sprite SlotIcon { get; private set; }
        [field: SerializeField] public int SlotRewardInfoValue { get; private set; }
        [field: InlineEditor, SerializeField] public PrizeCardSo PrizeCardSo { get; private set; }
        [field: SerializeField] public UnitType UnitType { get; private set; }
        
        public void SelectRandomCard()
        {
            PrizeCardSo = PrizeCardSoHolder.Instance.PrizeCardSos[Random.Range(0, PrizeCardSoHolder.Instance.PrizeCardSos.Count)];
        }
    }
}