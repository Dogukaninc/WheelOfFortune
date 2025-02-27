using UnityEngine;
using Utilities;

namespace CaseDemo.Scripts.CardHolder
{
    public class HierarchyObjectHolder : SingletonMonoBehaviour<HierarchyObjectHolder>
    {
        [field: SerializeField] public GameObject BlackScreen { get; private set; }
    }
}