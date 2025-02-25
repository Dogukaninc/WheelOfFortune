using CaseDemo.Scripts.SO_Classes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaseDemo.Scripts.FortuneWheel
{
    public class WheelSlot : MonoBehaviour
    {
        [ReadOnly] public WheelSlotSo wheelSlotSo;
        [field: SerializeField] public int SlotIndex { get; set; }
        [field: SerializeField] public string SlotRewardName { get; private set; }

        [SerializeField] private TextMeshProUGUI slotRewardValueText;
        [SerializeField] private Image slotIconImage;

        public void Initialize()
        {
            slotIconImage.sprite = wheelSlotSo.SlotIcon;
            slotRewardValueText.text = "x" + wheelSlotSo.RewardValue.ToString();
        }
    }
}