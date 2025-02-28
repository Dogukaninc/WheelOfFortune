using DG.Tweening;
using UnityEngine;

namespace CaseDemo.Scripts.SO_Classes
{
    [CreateAssetMenu(fileName = "so_config_fortune_wheel_sequence", menuName = "Scriptable Objects/so_config_fortune_wheel_sequence", order = 0)]
    public class FortuneWheelSequenceConfigSo : ScriptableObject
    {
        [field: SerializeField] public float MoveDownDuration { get; private set; }
        [field: SerializeField] public float MoveUpDuration { get; private set; }
        [field: SerializeField] public Vector3 MoveDownPosition { get; private set; }
        [field: SerializeField] public Vector3 MoveUpPosition { get; private set; }
        [field: SerializeField] public Ease MovingEase { get; private set; }
        
    }
}