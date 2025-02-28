using System.Collections.Generic;
using Assets._Main.Scripts.Utilities;
using CaseDemo.Scripts.Pool;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CaseDemo.Scripts.ZoneBar
{
    public class UiZoneBarController : MonoBehaviour
    {
        [ReadOnly] public ZoneType previousZoneType;

        [field: SerializeField] public List<Zone> Zones { get; private set; }
        [field: SerializeField] public Zone CurrentZone { get; private set; }

        [SerializeField] private RectTransform zoneBarElementsParent;
        [SerializeField] private Sprite goldZoneSprite;
        [SerializeField] private Sprite silverZoneSprite;
        [SerializeField] private Sprite defaultZoneSprite;

        [SerializeField] private float slidingDuration;
        [SerializeField] private int zoneCount;


        private float _slidingOffset;
        private int _currentZoneIndex;

        private void OnEnable()
        {
            GeneralEvents.OnZoneChange += SlideZoneBar;
        }

        private void OnDisable()
        {
            GeneralEvents.OnZoneChange -= SlideZoneBar;
        }

        private void Start()
        {
            InitializeZones();
            CurrentZone = Zones[0];
        }

        [Button]
        private void InitializeZones()
        {
            for (int i = 0; i < zoneCount; i++)
            {
                var zone = PoolSystem.Instance.SpawnGameObject("Zone");
                zone.transform.SetParent(zoneBarElementsParent);
                var zoneComponent = zone.GetComponent<Zone>();
                zone.transform.localScale = Vector3.one;
                if (!Zones.Contains(zoneComponent)) Zones.Add(zoneComponent);
                zoneComponent.ZoneText.text = (i + 1).ToString();
                SetZone(zoneComponent, i);
            }
        }

        private void SetZone(Zone zone, int i)
        {
            zone.InitializeZone(i);
        }

        [Button]
        public void SlideZoneBar()
        {
            if (_currentZoneIndex >= Zones.Count) return;

            var element0 = Zones[0].transform.GetComponent<RectTransform>().anchoredPosition.x;
            var element1 = Zones[1].transform.GetComponent<RectTransform>().anchoredPosition.x;
            _slidingOffset = -(element0 - element1);
            previousZoneType = CurrentZone.ZoneType;

            _currentZoneIndex++;
            CurrentZone = Zones[_currentZoneIndex];

            var sequence = DOTween.Sequence();
            var targetPosition = zoneBarElementsParent.anchoredPosition.x - _slidingOffset;
            sequence.Append(zoneBarElementsParent.DOAnchorPosX(targetPosition, slidingDuration));

            if (CurrentZone.ZoneType != previousZoneType)
            {
                GeneralEvents.OnWheelMoveDown?.Invoke();
            }
        }
    }
}