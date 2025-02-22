using _Main.Scripts.SO_Classes;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.FortuneWheel
{
    public class WheelSlot : MonoBehaviour
    {
        [field: SerializeField] public int SlotIndex { get; private set; }
        [field: SerializeField] public string slotRewardName { get; private set; }
        
        
        [SerializeField] private WheelSlotSo wheelSlotSo;
        [SerializeField] private Image slotIconImage;
                
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            SlotIndex = transform.GetSiblingIndex();
            slotIconImage.sprite = wheelSlotSo.SlotIcon;
        }

        private void OnValidate()
        {
            SlotIndex = transform.GetSiblingIndex();
        }
    }
}