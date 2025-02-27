using System;
using CaseDemo.Scripts.EffectControllers;
using UnityEngine;

namespace Assets._Main.Scripts.Utilities
{
    public static class GeneralEvents
    {
        #region Ui Events

        public static Action<UnitType> OnUiPrizeCardCollected;
        public static Action<UnitType, int> OnDisplayElementUpdated;
        public static Action<Sprite, int, UnitType> CallInitializeDisplayElement;

        #endregion
    }
}