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

        [SerializeField] private TextMeshProUGUI slotRewardValueText;
        [SerializeField] private Image slotIconImage;

        public void Initialize()
        {
            wheelSlotSo.SelectRandomCard();
            slotIconImage.sprite = wheelSlotSo.SlotIcon;
            slotRewardValueText.text = "x" + wheelSlotSo.SlotRewardInfoValue.ToString();
        }
        
    }
}