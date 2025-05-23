using UnityEngine;

namespace CaseDemo.Scripts.Pool
{
    public class PoolableObject : MonoBehaviour
    {
        public string PoolTag { get; set; }

        private void OnDisable()
        {
            transform.localScale = Vector3.one;
            PoolSystem.Instance.ReturnToPool(PoolTag, gameObject);
        }
    }
}