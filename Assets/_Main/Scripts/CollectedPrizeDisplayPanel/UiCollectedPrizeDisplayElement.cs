using CaseDemo.Scripts.EffectControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaseDemo.Scripts.CollectedPrizeDisplayPanel
{
    public class UiCollectedPrizeDisplayElement : MonoBehaviour
    {
        [field: SerializeField] public UnitType UnitType { get; private set; }
        [field: SerializeField] public TextMeshProUGUI PrizeAmountText { get; private set; }
        [field: SerializeField] public Image PrizeDisplayIcon { get; private set; }
        
        private int _totalPrizeAmount;
        
        public void InitializeElement(Sprite sprite, int prizeAmount, UnitType unitType)
        {
            _totalPrizeAmount += prizeAmount;
            PrizeDisplayIcon.sprite = sprite;
            PrizeAmountText.text = prizeAmount.ToString();
            UnitType = unitType;
        }

        public void UpdateElement(int prizeAmount)
        {
            _totalPrizeAmount += prizeAmount;
            PrizeAmountText.text = _totalPrizeAmount.ToString();
        }
    }
}