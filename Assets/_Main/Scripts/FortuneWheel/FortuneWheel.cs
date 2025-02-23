using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.Scripts.FortuneWheel
{
    public class FortuneWheel : MonoBehaviour
    {
        [SerializeField] private float rotationDuration;
        [SerializeField] private float slotYPosOffset;

        [SerializeField] private int initialRotationCount;
        [SerializeField] private RectTransform fortuneWheelRectTransform;

        [SerializeField] private List<WheelSlot> wheelSlots;
        [SerializeField] private Transform slotsParent;

        private Sequence _sequence;
        private int _wheelSlotCount;
        private float _initialRotationAngle;
        private float _fortuneWheelRadius;

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeSlots();
            _sequence = DOTween.Sequence();
            _wheelSlotCount = wheelSlots.Count;
            _initialRotationAngle = initialRotationCount * 360;
        }

        private void InitializeSlots()
        {
            float width = fortuneWheelRectTransform.rect.width;
            float height = fortuneWheelRectTransform.rect.height;
            _fortuneWheelRadius = (Mathf.Min(width, height) * 0.5f) - slotYPosOffset;

            for (int i = 0; i < slotsParent.childCount; i++)
            {
                var slot = slotsParent.GetChild(i).GetComponent<WheelSlot>();
                wheelSlots.Add(slot);
                float angle = i * 45;
                float rad = angle * Mathf.Deg2Rad;

                Vector3 pos = new Vector3(Mathf.Cos(rad) * _fortuneWheelRadius, Mathf.Sin(rad) * _fortuneWheelRadius, 0f);
                slot.GetComponent<RectTransform>().localPosition = pos;
                slot.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            }
        }

        [Button]
        private void SpinWheel()
        {
            if (_sequence.IsActive() && _sequence.IsPlaying()) return;
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            var targetRotationAngle = CalculateTargetRotationAngle();

            float currentAngle = transform.eulerAngles.z;
            float deltaAngle = targetRotationAngle - currentAngle;
            if (deltaAngle < 0)
            {
                deltaAngle += 360f;
            }

            float totalRotationAngle = _initialRotationAngle * 360f + deltaAngle;
            float finalRotation = currentAngle + totalRotationAngle;
            var endRotationAngle = new Vector3(0, 0, finalRotation);
            
            _sequence.Append(fortuneWheelRectTransform.DORotate(endRotationAngle, rotationDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear));
            
            
        }

        private float CalculateTargetRotationAngle()
        {
            var targetSlot = SelectSlot();
            var targetSlotIndex = targetSlot.SlotIndex;
            var slotAngle = 360f / _wheelSlotCount;
            var targetRotationAngle = (slotAngle * targetSlotIndex - slotAngle);

            Debug.Log($"Target Slot Index: {targetSlotIndex} Target Rotation Angle: {targetRotationAngle}");
            Debug.Log("Reward Name:" + targetSlot.SlotRewardName);
            return targetRotationAngle;
        }

        private WheelSlot SelectSlot() //todo: rastgeleliği arttıran bir fonksiyon ya da ihtimali konfigure edilebilir yap.
        {
            var targetIndex = Random.Range(0, _wheelSlotCount);
            return wheelSlots.Find(s => s.SlotIndex == targetIndex);
        }
    }
}