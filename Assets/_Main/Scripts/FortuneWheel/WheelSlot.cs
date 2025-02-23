using _Main.Scripts.SO_Classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.FortuneWheel
{
    public class WheelSlot : MonoBehaviour
    {
        [field: SerializeField] public int SlotIndex { get; private set; }
        [field: SerializeField] public string SlotRewardName { get; private set; }

        [SerializeField] private WheelSlotSo wheelSlotSo;
        [SerializeField] private TextMeshProUGUI slotRewardValueText;
        [SerializeField] private Image slotIconImage;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            SlotIndex = transform.GetSiblingIndex();
            slotIconImage.sprite = wheelSlotSo.SlotIcon;
            slotRewardValueText.text = "x" + wheelSlotSo.RewardValue.ToString();
        }

        private void OnValidate()
        {
            Initialize();
        }
    }
}