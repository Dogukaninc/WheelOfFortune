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

        #region Fortune Wheel Events

        public static Action OnWheelMoveUp;
        public static Action OnWheelMoveDown;
        public static Action OnWheelRendererChange;

        #endregion

        #region Zone Bar Events

        public static Action OnZoneChange;
        

        #endregion
    }
}