using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CaseDemo.Scripts.EffectControllers
{
    public enum UnitType
    {
        Coin,
        Money,
        Armor,
        Rifle,
        Smg,
        Bomb,
    }

    public class UiPrizeUnitController : MonoBehaviour
    {
        public UnitType unitType;
        [SerializeField] private float movementDuration;
        [SerializeField] private Image unitImage;

        public void Initialize(Sprite sprite, UnitType _unitType)
        {
            unitType = _unitType;
            unitImage.sprite = sprite;
        }

        public void UnitSequnece(Vector3 targetPosition)
        {
            var initialScale = transform.localScale;
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(transform.localScale * 1.5f, 0.5f).OnComplete(() => { transform.DOScale(initialScale, 0.5f); }));
            sequence.Append(transform.DOMove(targetPosition, movementDuration));
            sequence.AppendCallback(() => ResetUnit());
        }

        private void ResetUnit()
        {
            unitImage.sprite = null;
            gameObject.SetActive(false);
        }
    }
}