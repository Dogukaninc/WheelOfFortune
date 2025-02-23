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
        [SerializeField] private int desiredSlotCount;


        [SerializeField] private int initialRotationCount;
        [SerializeField] private RectTransform fortuneWheelRectTransform;

        [SerializeField] private WheelSlot slotPrefab;

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
            _initialRotationAngle = initialRotationCount * 360;
        }

        [Button]
        private void InitializeSlots(float divider = 8)
        {
            float width = fortuneWheelRectTransform.rect.width;
            float height = fortuneWheelRectTransform.rect.height;
            slotYPosOffset = height / divider;
            _fortuneWheelRadius = (Mathf.Min(width, height) * 0.5f) - slotYPosOffset;

            for (int i = 0; i < desiredSlotCount; i++)
            {
                var slot = Instantiate(slotPrefab, slotsParent);
                float angle = i * 360 / desiredSlotCount;
                float rad = angle * Mathf.Deg2Rad;
                Vector3 pos = new Vector3(Mathf.Cos(rad) * _fortuneWheelRadius, Mathf.Sin(rad) * _fortuneWheelRadius, 0f);
                var slotRect = slot.GetComponent<RectTransform>();
                slotRect.localPosition = pos;
                slotRect.rotation = Quaternion.Euler(0, 0, angle - 90f);
                var slotScaleFactor = height / slotRect.rect.height;
                slotRect.localScale = Vector3.one * (slotScaleFactor / 10);
                Debug.Log(slotRect.rect.height);

                if (wheelSlots.Contains(slot))
                {
                    continue;
                }

                wheelSlots.Add(slot);
            }

            _wheelSlotCount = wheelSlots.Count;
        }

        [Button]
        private void SpinWheel()
        {
            if (_sequence.IsActive() || _sequence.IsPlaying()) return;
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            var targetRotationAngle = CalculateTargetRotationAngle();

            float currentAngle = transform.eulerAngles.z;
            float deltaAngle = targetRotationAngle - currentAngle;
            if (deltaAngle < 0)
            {
                deltaAngle += 360f;
            }

            float totalRotationAngle = deltaAngle;
            float finalRotation = currentAngle + totalRotationAngle;
            var endRotationAngle = new Vector3(0, 0, finalRotation);

            _sequence.SetEase(Ease.InOutQuad);
            // _sequence.Append(fortuneWheelRectTransform.DORotate(endRotationAngle, rotationDuration, RotateMode.FastBeyond360).SetLoops(5, LoopType.Incremental));
            //todo append ile desired slot rotasyonuna kadar son kez döndür
            _sequence.Append(fortuneWheelRectTransform.DORotate(new Vector3(0, 0, 1) * 360 * 5, rotationDuration, RotateMode.FastBeyond360));
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