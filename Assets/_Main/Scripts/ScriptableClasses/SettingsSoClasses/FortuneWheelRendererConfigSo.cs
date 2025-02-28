using Sirenix.OdinInspector;
using UnityEngine;

namespace CaseDemo.Scripts.SO_Classes
{
    [CreateAssetMenu(fileName = "so_fortune_wheel_renderer_config", menuName = "Scriptable Objects/so_fortune_wheel_renderer_config", order = 0)]
    public class FortuneWheelRendererConfigSo : ScriptableObject
    {
        [field: PreviewField, SerializeField] public Sprite GoldenWheelSprite { get; private set; }
        [field: PreviewField, SerializeField] public Sprite SilverWheelSprite { get; private set; }
        [field: PreviewField, SerializeField] public Sprite BronzeWheelSprite { get; private set; }

        [field: PreviewField, SerializeField] public Sprite GoldIndicatorSprite { get; private set; }
        [field: PreviewField, SerializeField] public Sprite SilverIndicatorSprite { get; private set; }
        [field: PreviewField, SerializeField] public Sprite BronzeIndicatorSprite { get; private set; }
    }
}