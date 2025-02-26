using UnityEngine;
using DG.Tweening;

namespace CaseDemo.Scripts.FortuneWheel
{
    public class WheelIndicatorController : MonoBehaviour
    {
        [SerializeField] private RectTransform fortuneWheelRectTransform;
        [SerializeField] private RectTransform indicatorTransform;
        private readonly int _wheelSlotCount = 8;
        private int _lastSlotIndex = -1;

        public void IndicatorRoutine()
        {
            float currentAngle = fortuneWheelRectTransform.eulerAngles.z;
            float slotAngle = 360f / _wheelSlotCount;
            int currentSlotIndex = Mathf.FloorToInt(currentAngle / slotAngle);

            if (currentSlotIndex != _lastSlotIndex)
            {
                _lastSlotIndex = currentSlotIndex;

                DOTween.Kill(this);
                indicatorTransform.DOPunchRotation(new Vector3(0, 0, -10f), 0.1f, 10, 1f).OnComplete(() =>
                {
                    indicatorTransform.transform.DORotate(Vector3.zero, 0.1f);
                });
            }
        }
    }
}