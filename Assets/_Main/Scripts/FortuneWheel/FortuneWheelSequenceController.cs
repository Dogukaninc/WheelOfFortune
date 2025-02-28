using System;
using Assets._Main.Scripts.Utilities;
using CaseDemo.Scripts.SO_Classes;
using DG.Tweening;
using UnityEngine;

namespace CaseDemo.Scripts.FortuneWheel
{
    public class FortuneWheelSequenceController : MonoBehaviour
    {
        [SerializeField] private FortuneWheelSequenceConfigSo fortuneWheelSequenceConfigSo;

        private FortuneWheelController _fortuneWheelController;
        private RectTransform _fortuneWheelRectTransform;

        private void Awake()
        {
            _fortuneWheelController = GetComponent<FortuneWheelController>();
            _fortuneWheelRectTransform = _fortuneWheelController.GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            GeneralEvents.OnWheelMoveUp += MoveWheelUp;
            GeneralEvents.OnWheelMoveDown += MoveWheelDown;
        }

        private void OnDisable()
        {
            GeneralEvents.OnWheelMoveUp -= MoveWheelUp;
            GeneralEvents.OnWheelMoveDown -= MoveWheelDown;
        }

        private void MoveWheelDown()
        {
            var moveDownSequence = DOTween.Sequence();
            moveDownSequence.Append(_fortuneWheelRectTransform.DOAnchorPosY(fortuneWheelSequenceConfigSo.MoveDownPosition.y
                    , fortuneWheelSequenceConfigSo.MoveDownDuration)
                .SetEase(fortuneWheelSequenceConfigSo.MovingEase));
            moveDownSequence.AppendCallback(() => GeneralEvents.OnWheelRendererChange?.Invoke());
        }

        private void MoveWheelUp()
        {
            var moveUpSequence = DOTween.Sequence();
            moveUpSequence.Append(_fortuneWheelRectTransform.DOAnchorPosY(fortuneWheelSequenceConfigSo.MoveUpPosition.y
                    , fortuneWheelSequenceConfigSo.MoveUpDuration)
                .SetEase(fortuneWheelSequenceConfigSo.MovingEase));
        }
    }
}