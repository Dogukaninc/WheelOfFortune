using UnityEngine;

namespace _Main.Scripts.SO_Classes
{
    [CreateAssetMenu(fileName = "Wheel_Slot_Data", menuName = "Scriptable Objects/wheel_slot_data", order = 0)]
    public class WheelSlotSo : ScriptableObject
    {
        [field: SerializeField] public Sprite SlotIcon { get; private set; }
        [field: SerializeField] public int RewardValue { get; private set; }

        //Green  box , yellow box, red box, blue box, purple box, orange box, pink box, light blue box
        // Reward values, reward icons, reward names
    }
}