using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CaseDemo.Scripts.SO_Classes
{
    [CreateAssetMenu(fileName = "so_wheelslot_data_holder", menuName = "Scriptable Objects/so_wheelslot_data_holder")]
    public class WheelSlotDataHolderSo : ScriptableObject
    {
        [field: InlineEditor, SerializeField] public List<WheelSlotSo> AllWheelSlotData { get; private set; }
    }
}
