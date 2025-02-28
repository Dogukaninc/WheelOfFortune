using CaseDemo.Scripts.SO_Classes;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaseDemo.Scripts.ZoneBar
{
    public enum ZoneType
    {
        Default,
        Silver,
        Gold
    }

    public class Zone : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI ZoneText { get; private set; }
        [field: SerializeField] public Image ZoneImage { get; private set; }
        [field: ReadOnly, SerializeField] public ZoneType ZoneType { get; private set; }

        [InlineEditor, SerializeField] private ZoneTypeSo zoneTypeSo;

        public void InitializeZone(int zoneIndex)
        {
            var zoneValue = zoneIndex + 1;
            ZoneText.text = zoneValue.ToString();

            if (zoneValue % 30 == 0)
            {
                ZoneImage.sprite = zoneTypeSo.GoldZoneSprite;
                ZoneType = ZoneType.Gold;
            }
            else if (zoneValue % 5 == 0)
            {
                ZoneImage.sprite = zoneTypeSo.SilverZoneSprite;
                ZoneType = ZoneType.Silver;
            }
            else
            {
                ZoneImage.sprite = zoneTypeSo.DefaultZoneSprite;
                ZoneType = ZoneType.Default;
            }
        }
    }
}