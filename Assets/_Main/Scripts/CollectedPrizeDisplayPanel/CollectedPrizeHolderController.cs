using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Main.Scripts.Utilities;
using CaseDemo.Scripts.EffectControllers;
using CaseDemo.Scripts.Pool;
using CaseDemo.Scripts.SO_Classes;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace CaseDemo.Scripts.CollectedPrizeDisplayPanel
{
    public class CollectedPrizeHolderController : SingletonMonoBehaviour<CollectedPrizeHolderController>
    {
        [field: SerializeField] public List<UiCollectedPrizeDisplayElement> DisplayElements { get; private set; }
        [field: SerializeField] public List<UiPrizeUnitSo> UiPrizeUnitSos { get; private set; }

        [SerializeField] private Transform elementsParent;
        [SerializeField] private float placementOffset;
        
        [Header("Leave Game Panel")]
        [SerializeField] private Button exitButton;
        [SerializeField] private GameObject leaveGamePanel;
        
        private void OnEnable()
        {
            GeneralEvents.OnDisplayElementUpdated += UpdateElement;
            GeneralEvents.CallInitializeDisplayElement += SetNewPrizeDisplayElement;
            exitButton.onClick.AddListener(SetExitButtonCondition);
        }

        private void OnDisable()
        {
            GeneralEvents.OnDisplayElementUpdated -= UpdateElement;
            GeneralEvents.CallInitializeDisplayElement -= SetNewPrizeDisplayElement;
            exitButton.onClick.RemoveListener(SetExitButtonCondition);
        }

        public void SetNewPrizeDisplayElement(Sprite sprite, int prizeAmount, UnitType unitType)
        {
            var newElement = PoolSystem.Instance.SpawnGameObject("UiPrizeDisplayElement");
            var displayElement = newElement.GetComponent<UiCollectedPrizeDisplayElement>();
            if (!DisplayElements.Contains(displayElement)) DisplayElements.Add(displayElement);
            if (DisplayElements.Any(e => e.UnitType == unitType))
            {
                UpdateElement(unitType, prizeAmount);
                return;
            }

            displayElement.transform.SetParent(elementsParent);
            displayElement.transform.localScale = Vector3.one;
            displayElement.InitializeElement(SelectRelativePrizeUnitSo(unitType), prizeAmount, unitType);
            displayElement.transform.position = DisplayElements[^1].transform.position + new Vector3(0, placementOffset, 0);
        }

        private void UpdateElement(UnitType unitType, int prizeAmount)
        {
            var displayElement = DisplayElements.Find(element => element.UnitType == unitType);
            try
            {
                if (displayElement != null)
                {
                    displayElement.UpdateElement(prizeAmount);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error updating element: {ex.Message}");
                Debug.LogWarning("Element not found");
            }
        }
        
        private Sprite SelectRelativePrizeUnitSo(UnitType unitType)
        {
            var prizeUnitSo = UiPrizeUnitSos.Find(prizeUnit => prizeUnit.UnitType == unitType);
            return prizeUnitSo.UnitSprite;
        }

        private void SetExitButtonCondition()
        {
            leaveGamePanel.gameObject.SetActive(true);
        }
        
    }
}