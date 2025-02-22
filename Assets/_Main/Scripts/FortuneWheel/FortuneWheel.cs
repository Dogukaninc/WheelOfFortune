using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Main.Scripts.FortuneWheel
{
    public class FortuneWheel : MonoBehaviour
    {
        [SerializeField] private float rotationDuration;
        [SerializeField] private int initialRotationCount;
        [SerializeField] private RectTransform fortuneWheelRectTransform;

        [SerializeField] private List<WheelSlot> wheelSlots; // todo listeyi getChild ile setle initialize da

        private Sequence _sequence;
        private int _wheelSlotCount;
        private float _initialRotationAngle;

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            _sequence = DOTween.Sequence();
            _wheelSlotCount = wheelSlots.Count;
            _initialRotationAngle = initialRotationCount * 360;
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
            _sequence.Append(fortuneWheelRectTransform.DORotate(endRotationAngle, rotationDuration, RotateMode.FastBeyond360).SetEase(Ease.OutCubic));
        }

        /// <summary>
        /// Slot sayısını başlangıçta çek, daha sonra bu sayıyı 360 dereceye bölecek şekilde slotları döndür.
        /// Slot index'i listede bulunduğu index'e göre başlangıçta set edilebilir.
        /// </summary>

        //Todo her slotun index'i başlangıçta hierarchyde bulunduğu sıraya göre set edilebilir. böylece elle atamamıza gerek kalmaz
        private float CalculateTargetRotationAngle()
        {
            var targetSlot = SelectSlot();
            var targetSlotIndex = targetSlot.SlotIndex;
            var slotAngle = 360f / _wheelSlotCount;
            var targetRotationAngle = (slotAngle * targetSlotIndex - slotAngle);

            Debug.Log($"Target Slot Index: {targetSlotIndex} Target Rotation Angle: {targetRotationAngle}");
            Debug.Log("Reward Name:" + targetSlot.slotRewardName);
            return targetRotationAngle;
        }

        private WheelSlot SelectSlot() //todo: rastgeleliği arttıran bir fonksiyon ya da ihtimali konfigure edilebilir yap.
        {
            var targetIndex = Random.Range(0, _wheelSlotCount);
            return wheelSlots.Find(s => s.SlotIndex == targetIndex);
        }
    }
}