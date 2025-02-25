using Sirenix.OdinInspector;
using UnityEngine;

namespace CaseDemo.Scripts.SO_Classes
{
    [CreateAssetMenu(fileName = "so_wheel_slot_config", menuName = "Scriptable Objects/wheel_slot_config", order = 0)]
    public class WheelSlotSo : ScriptableObject
    {
        [field: PreviewField(100), SerializeField] public Sprite SlotIcon { get; private set; }
        [field: SerializeField] public int RewardValue { get; private set; }

        //Green  box , yellow box, red box, blue box, purple box, orange box, pink box, light blue box
        // Reward values, reward icons, reward names

        // static readonly List<RuntimeScriptableObject> Instances = new();
        //
        // private void OnEnable() => Instances.Add(this);
        // private void OnDisable() => Instances.Remove(this);
        // protected abstract void OnReset();
        //
        // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // todo
        // static void ResetAllInstances()
        // {
        //     foreach (var instace in Instances)
        //     {
        //         instace.OnReset();
        //     }
        // }
    }
}