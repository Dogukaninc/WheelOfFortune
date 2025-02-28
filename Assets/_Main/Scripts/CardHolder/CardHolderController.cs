using Assets._Main.Scripts.Utilities;
using CaseDemo.Scripts.EffectControllers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaseDemo.Scripts.CardHolder
{
    public class CardHolderController : MonoBehaviour
    {
        [field: SerializeField] public UnitType CardUnitType { get; private set; }

        [Header("Card Animtaion Settings")] [SerializeField]
        private float cardMoveUpDuration;

        [SerializeField] private float cardMoveDownDuration;

        [Space(20)] [SerializeField] private Transform targetCardPosition;
        [SerializeField] private Image cardItemImage;
        [SerializeField] private TextMeshProUGUI cardItemHeaderText;
        [SerializeField] private TextMeshProUGUI cardPrizeInfoText;

        private Vector3 _intialCardPosition;

        public void InitializeCard(Sprite sprite, string headerText, string prizeInfoText, UnitType unitType)
        {
            _intialCardPosition = transform.position;
            cardItemImage.sprite = sprite;
            cardItemHeaderText.text = headerText;
            cardPrizeInfoText.text = prizeInfoText;
            CardUnitType = unitType;
        }

        public void CardMoveUpAnimationSequnce()
        {
            if (gameObject.activeSelf) return;
            HierarchyObjectHolder.Instance.BlackScreen.SetActive(true);
            gameObject.SetActive(true);
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(targetCardPosition.position, cardMoveUpDuration).SetEase(Ease.OutQuad));
        }

        public void CardMoveDownAnimationSequnce(UnitType unitType)
        {
            if (!gameObject.activeSelf) return;
            HierarchyObjectHolder.Instance.BlackScreen.SetActive(false);
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(_intialCardPosition, cardMoveUpDuration).SetEase(Ease.OutQuad));
            sequence.AppendCallback(() => gameObject.SetActive(false));
            sequence.AppendCallback(() => GeneralEvents.OnUiPrizeCardCollected?.Invoke(unitType));
            sequence.AppendCallback(() => GeneralEvents.OnZoneChange?.Invoke());
        }
    }
}