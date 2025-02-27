using System.Collections;
using System.Collections.Generic;
using Assets._Main.Scripts.Utilities;
using CaseDemo.Scripts.CollectedPrizeDisplayPanel;
using CaseDemo.Scripts.Pool;
using CaseDemo.Scripts.SO_Classes;
using CaseDemo.Scripts.SO_Classes.SettingsSoClasses;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CaseDemo.Scripts.EffectControllers
{
    public class UiPrizeEffectsController : MonoBehaviour
    {
        [InlineEditor, SerializeField] private SpawnPrizeUnitConfig spawnPrizeUnitConfig;
        [field: SerializeField] public List<UiPrizeUnitSo> PrizeUnitSos { get; private set; }

        [SerializeField] private RectTransform prizeSpawnPoint;
        [SerializeField] private Transform mainCanvas;
        [SerializeField] private UiPrizeUnitSo uiDefaultPrizeUnitSo;
        [SerializeField] private int spawnCount;


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
            for (int i = 0; i < spawnCount; i++)
            {
                var unit = PoolSystem.Instance.SpawnGameObject("UiPrizeUnit");
                var prizeUnit = unit.GetComponent<UiPrizeUnitController>();
                var prizeUnitSo = SelectPrizeUnitSo(unitType);
                prizeUnit.Initialize(prizeUnitSo.UnitSprite, prizeUnitSo.UnitType);
                unit.transform.SetParent(mainCanvas);
                SetUnits(prizeUnit);
                yield return new WaitForSeconds(spawnPrizeUnitConfig.SpawningInterval);
            }
        }

        private void SetUnits(UiPrizeUnitController prizeUnit)
        {
            var spawnRaidus = new Vector3(Random.Range(-spawnPrizeUnitConfig.SpawnUnitRadius, spawnPrizeUnitConfig.SpawnUnitRadius),
                Random.Range(-spawnPrizeUnitConfig.SpawnUnitRadius, spawnPrizeUnitConfig.SpawnUnitRadius), 0);

            prizeUnit.transform.position = prizeSpawnPoint.position + spawnRaidus;
            var targetPosition = SelectTargetPrizePosition(prizeUnit);
            prizeUnit.UnitSequnece(targetPosition);
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