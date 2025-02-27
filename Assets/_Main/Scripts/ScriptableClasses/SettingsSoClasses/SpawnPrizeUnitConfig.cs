using UnityEngine;

namespace CaseDemo.Scripts.SO_Classes.SettingsSoClasses
{
    [CreateAssetMenu(fileName = "so_spawn_prize_unit_config", menuName = "Scriptable Objects/so_spawn_prize_unit_config", order = 0)]
    public class SpawnPrizeUnitConfig : ScriptableObject
    {
        [field: SerializeField] public float SpawnUnitRadius { get; private set; }
        [field: SerializeField] public float SpawningInterval { get; private set; }
    }
}