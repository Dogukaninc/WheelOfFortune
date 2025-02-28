using Sirenix.OdinInspector;
using UnityEngine;

namespace CaseDemo.Scripts.SO_Classes
{
    [CreateAssetMenu(fileName = "so_zone_type", menuName = "Scriptable Objects/so_zone_type", order = 0)]
    public class ZoneTypeSo : ScriptableObject
    {
        [field: PreviewField,SerializeField] public Sprite DefaultZoneSprite { get; set; }
        [field: PreviewField,SerializeField] public Sprite SilverZoneSprite { get; set; }
        [field: PreviewField,SerializeField] public Sprite GoldZoneSprite { get; set; }
    }
}