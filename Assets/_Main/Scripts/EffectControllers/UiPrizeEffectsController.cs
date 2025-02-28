using System.Collections;
using System.Collections.Generic;
using Assets._Main.Scripts.Utilities;
using CaseDemo.Scripts.CollectedPrizeDisplayPanel;
using CaseDemo.Scripts.Pool;
using CaseDemo.Scripts.SO_Classes;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CaseDemo.Scripts.EffectControllers
{
    public class UiPrizeEffectsController : MonoBehaviour
    {
        [InlineEditor, SerializeField] private SpawnPrizeUnitConfigSo spawnPrizeUnitConfigSo;
        [field: SerializeField] public List<UiPrizeUnitSo> PrizeUnitSos { get; private set; }

        [SerializeField] private RectTransform prizeSpawnPoint;
        [SerializeField] private Transform mainCanvas;
        [SerializeField] private UiPrizeUnitSo uiDefaultPrizeUnitSo;
        [SerializeField] private int spawnCount;

        private List<UiPrizeUnitController> _spawnedUnits = new();
        private CollectedPrizeHolderController _collectedPrizeDisplayPanel;
        private UnitType _unitType;

        private void OnEnable()
        {
            GeneralEvents.OnUiPrizeCardCollected += SpawnUnits;
        }

        private void OnDisable()
        {
            GeneralEvents.OnUiPrizeCardCollected -= SpawnUnits;
        }

        private void Start()
        {
            _collectedPrizeDisplayPanel = CollectedPrizeHolderController.Instance;
        }

        [Button]
        private void SpawnUnits(UnitType unitType)
        {
            _unitType = unitType;
            StartCoroutine(SpawnUnitsRoutine(_unitType));
        }

        IEnumerator SpawnUnitsRoutine(UnitType unitType)
        {
            _spawnedUnits.Clear();

            for (int i = 0; i < spawnCount; i++)
            {
                var unit = PoolSystem.Instance.SpawnGameObject("UiPrizeUnit");
                var prizeUnit = unit.GetComponent<UiPrizeUnitController>();
                var prizeUnitSo = SelectPrizeUnitSo(unitType);
                prizeUnit.Initialize(prizeUnitSo.UnitSprite, prizeUnitSo.UnitType);
                unit.transform.SetParent(mainCanvas);
                
                _spawnedUnits.Add(prizeUnit);
                SetUnits(prizeUnit);
                
                yield return new WaitForSeconds(spawnPrizeUnitConfigSo.SpawningInterval);
            }

            MoveAllUnitsToTarget();
        }

        private void MoveAllUnitsToTarget()
        {
            foreach (var prizeUnit in _spawnedUnits)
            {
                var targetPosition = SelectTargetPrizePosition(prizeUnit);
                prizeUnit.UnitSequnece(targetPosition);
            }
        }

        private void SetUnits(UiPrizeUnitController prizeUnit)
        {
            var spawnRaidus = new Vector3(Random.Range(-spawnPrizeUnitConfigSo.SpawnUnitRadius, spawnPrizeUnitConfigSo.SpawnUnitRadius),
                Random.Range(-spawnPrizeUnitConfigSo.SpawnUnitRadius, spawnPrizeUnitConfigSo.SpawnUnitRadius), 0);

            prizeUnit.transform.position = prizeSpawnPoint.position + spawnRaidus;
        }

        private UiPrizeUnitSo SelectPrizeUnitSo(UnitType unitType)
        {
            var relativeUnitType = PrizeUnitSos.Find(u => u.UnitType == unitType);
            if (relativeUnitType != null)
            {
                return relativeUnitType;
            }

            return uiDefaultPrizeUnitSo;
        }

        private Vector3 SelectTargetPrizePosition(UiPrizeUnitController prizeUnit)
        {
            var targetElement = _collectedPrizeDisplayPanel.DisplayElements.Find(i => i.UnitType == prizeUnit.unitType);
            if (targetElement == null)
            {
                targetElement = _collectedPrizeDisplayPanel.DisplayElements[0];
            }

            return targetElement.transform.position;
        }
    }
}