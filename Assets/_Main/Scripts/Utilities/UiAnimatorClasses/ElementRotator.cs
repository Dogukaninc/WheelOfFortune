using DG.Tweening;
using UnityEngine;

namespace CaseDemo.Scripts.ElementRotator
{
    public class ElementRotator : MonoBehaviour
    {
        [SerializeField] private float rotationInterval;
        [SerializeField] private Vector3 rotationAxis;

        private void Start()
        {
            transform.DORotate(rotationAxis, rotationInterval, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);
        }
    }
}