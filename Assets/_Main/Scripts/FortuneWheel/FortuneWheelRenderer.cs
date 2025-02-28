using Assets._Main.Scripts.Utilities;
using CaseDemo.Scripts.ZoneBar;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CaseDemo.Scripts.SO_Classes
{
    public class FortuneWheelRenderer : MonoBehaviour
    {
        [SerializeField] private Image rouletteWheelImage;
        [SerializeField] private Image rouletteIndicatorImage;
        [SerializeField] private UiZoneBarController uiZoneBarController;
        [field:InlineEditor, SerializeField] public FortuneWheelRendererConfigSo FortuneWheelRendererConfigSo { get; private set; }

        private void OnEnable()
        {
            GeneralEvents.OnWheelRendererChange += SetFortuneWheelRenderer;
        }

        private void OnDisable()
        {
            GeneralEvents.OnWheelRendererChange -= SetFortuneWheelRenderer;
        }

        private void SetFortuneWheelRenderer()
        {
            var rendererConfig = FortuneWheelRendererConfigSo;
            var currentZone = uiZoneBarController.CurrentZone;
            Debug.Log("Wheel Render Changeee");
            
            switch (currentZone.ZoneType)
            {
                case ZoneType.Default:
                    rouletteWheelImage.sprite = rendererConfig.BronzeWheelSprite;
                    rouletteIndicatorImage.sprite = rendererConfig.BronzeIndicatorSprite;
                    Debug.Log("Wheel Renderer: Bronze");
                    break;
                
                case ZoneType.Silver:
                    rouletteWheelImage.sprite = rendererConfig.SilverWheelSprite;
                    rouletteIndicatorImage.sprite = rendererConfig.SilverIndicatorSprite;
                    Debug.Log("Wheel Renderer: Silver");
                    break;
                
                case ZoneType.Gold:
                    rouletteWheelImage.sprite = rendererConfig.GoldenWheelSprite;
                    rouletteIndicatorImage.sprite = rendererConfig.GoldIndicatorSprite;
                    Debug.Log("Wheel Renderer: Gold");
                    break;
                
                default:
                    rouletteWheelImage.sprite = rendererConfig.BronzeWheelSprite;
                    rouletteIndicatorImage.sprite = rendererConfig.BronzeIndicatorSprite;
                    break;
            }
            
            GeneralEvents.OnWheelMoveUp?.Invoke();
        }
    }
}