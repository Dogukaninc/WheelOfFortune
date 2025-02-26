using System.Collections.Generic;
using CaseDemo.Scripts.Pool;
using CaseDemo.Scripts.SO_Classes;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CaseDemo.Scripts.FortuneWheel
{
    public class FortuneWheelController : MonoBehaviour
    {
        // [ReadOnly] public bool IsSpinning { get; set; }
        [SerializeField] private float rotationDuration;
        [SerializeField] private float slotYPosOffset;
        [SerializeField] private int desiredSlotCount;
        [SerializeField] private int initialRotationCount;

        [SerializeField] private WheelSlotDataHolderSo wheelSlotDataHolderSo;
        [SerializeField] private List<WheelSlot> wheelSlots; // todo: odin'den custom inspector yapılabilir
        [SerializeField] private RectTransform fortuneWheelRectTransform;
        [SerializeField] private Transform slotsParent;
        [SerializeField] private Button spinButton;
        [SerializeField] private Ease spinningEase;
        [FormerlySerializedAs("indicatorSway")] [SerializeField] private WheelIndicatorController wheelIndicatorController;

        private Sequence _sequence;
        private int _wheelSlotCount;
        private float _fortuneWheelRadius;

        private void OnEnable()
        {
            spinButton.onClick.AddListener(SpinWheel);
        }

        private void OnDisable()
        {
            spinButton.onClick.AddListener(SpinWheel);
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeSlots();
            _sequence = DOTween.Sequence();
        }

        [Button]
        private void InitializeSlots(float divider = 5) // todo -> divider değerini configurable yap
        {
            float width = fortuneWheelRectTransform.rect.width;
            float height = fortuneWheelRectTransform.rect.height;
            slotYPosOffset = height / divider;
            _fortuneWheelRadius = (Mathf.Min(width, height) * 0.5f) - slotYPosOffset;

            for (int i = 0; i < desiredSlotCount; i++) // todo slotların so'ları rastgele atanacak ve initialize edilecek
            {
                var slot = PoolSystem.Instance.SpawnGameObject("WheelSlot");
                slot.transform.SetParent(slotsParent);
                var wheelSlot = slot.GetComponent<WheelSlot>();
                SelectRandomSlotConfig(wheelSlot);
                wheelSlot.SlotIndex = i;
                wheelSlot.Initialize();

                float angle = i * (360 / desiredSlotCount) + 90f;
                float rad = angle * Mathf.Deg2Rad;
                Vector3 pos = new Vector3(Mathf.Cos(rad) * _fortuneWheelRadius, Mathf.Sin(rad) * _fortuneWheelRadius, 0f);

                var slotRect = slot.GetComponent<RectTransform>();
                slotRect.localPosition = pos;
                slotRect.rotation = Quaternion.Euler(0, 0, angle - 90f);
                var slotScaleFactor = height / slotRect.rect.height;
                slotRect.localScale = Vector3.one * (slotScaleFactor / 10);
                Debug.Log(slotRect.rect.height);

                if (wheelSlots.Contains(wheelSlot)) continue;

                wheelSlots.Add(wheelSlot);
            }

            _wheelSlotCount = wheelSlots.Count;
        }

        private void SelectRandomSlotConfig(WheelSlot wheelSlot)
        {
            var slotConfig = wheelSlotDataHolderSo.AllWheelSlotData[Random.Range(0, wheelSlotDataHolderSo.AllWheelSlotData.Count)];
            wheelSlot.wheelSlotSo = slotConfig;
        }

        [Button]
        private void SpinWheel()
        {
            if (_sequence.IsActive() || _sequence.IsPlaying()) return;
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            float targetRotationAngle = CalculateTargetRotationAngle();
            float initialRotation = (360f * initialRotationCount);

            _sequence.Append(
                fortuneWheelRectTransform
                    .DORotate(new Vector3(0, 0, initialRotation + targetRotationAngle), rotationDuration, RotateMode.FastBeyond360)
                    .SetEase(spinningEase)
            );
            _sequence.OnUpdate(() => wheelIndicatorController.IndicatorRoutine());
        }

        private float CalculateTargetRotationAngle()
        {
            var targetSlot = SelectSlot();
            var targetSlotIndex = targetSlot.SlotIndex;
            var slotAngle = 360f / _wheelSlotCount;
            var targetRotationAngle = -slotAngle * targetSlotIndex;

            Debug.Log($"Target Slot Index: {targetSlotIndex} Target Rotation Angle: {targetRotationAngle}");
            Debug.Log("Reward Name:" + targetSlot.SlotRewardName);
            return targetRotationAngle;
        }

        private WheelSlot SelectSlot() //todo: rastgeleliği arttıran bir fonksiyon ya da ihtimali konfigure edilebilir yap.
        {
            var targetIndex = Random.Range(0, _wheelSlotCount);
            Debug.Log("Target Index:" + targetIndex);
            return wheelSlots.Find(s => s.SlotIndex == targetIndex);
        }
    }
}