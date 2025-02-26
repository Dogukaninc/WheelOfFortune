using UnityEngine;

namespace CaseDemo.Scripts.RectAxisSetter
{
    public class RectAxisSetter : MonoBehaviour
    {
        // [SerializeField] private RectTransform rectTransform;

        private void OnValidate()
        {
            // if (rectTransform == null) return;
            var rect = transform.GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, rect.offsetMin.x);
            rect.offsetMax = new Vector2(0, rect.offsetMax.x);
        }
    }
}