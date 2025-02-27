using System.Collections.Generic;
using CaseDemo.Scripts.SO_Classes;
using UnityEngine;
using Utilities;

namespace Assets._Main.Scripts.Utilities.PrizeCardSoHolder
{
    public class PrizeCardSoHolder : SingletonMonoBehaviour<PrizeCardSoHolder>
    {
        [field: SerializeField] public List<PrizeCardSo> PrizeCardSos { get; private set; }
    }
}