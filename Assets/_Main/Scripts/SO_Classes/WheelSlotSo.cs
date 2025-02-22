using UnityEngine;

namespace _Main.Scripts.SO_Classes
{
    [CreateAssetMenu(fileName = "Wheel_Slot_Data", menuName = "Scriptable Objects/wheel_slot_data", order = 0)]
    public class WheelSlotSo : ScriptableObject
    {
        [field: SerializeField] public Sprite SlotIcon { get; private set; }
        
    }
}