using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class SafeArea : MonoBehaviour
    {
        private float _bottomUnits, _topUnits;
        private float _leftUnits, _rightUnits;

        [SerializeField] private bool _verticalSafeArea = true;
        [SerializeField] private bool _horizontalSafeArea = true;

        private RectTransform _parentRectTransform;

        private void Start()
        {
#if UNITY_EDITOR

            ApplySafeAreaOnStart();
            _parentRectTransform = GetComponent<RectTransform>();
            EditorApplySafeArea();
#else
            ApplySafeAreaOnStart();
#endif
        }

        private void ApplySafeAreaOnStart()
        {
            var scaler = GetComponentInParent<CanvasScaler>();
            ApplySafeArea(scaler);
        }
        
        private void EditorApplySafeArea()
        {
            var safeAreaRect = Screen.safeArea;

            float scaleRatio = _parentRectTransform.rect.width / Screen.width;

            var left = safeAreaRect.xMin * scaleRatio;
            var right = -(Screen.width - safeAreaRect.xMax) * scaleRatio;
            var top = -safeAreaRect.yMin * scaleRatio;
            var bottom = (Screen.height - safeAreaRect.yMax) * scaleRatio;

            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.offsetMin = new Vector2(left, bottom);
            rectTransform.offsetMax = new Vector2(right, top);
        }

        private void ApplySafeArea(CanvasScaler scaler)
        {
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            if (_horizontalSafeArea) ApplyHorizontalSafeArea(scaler);
            if (_verticalSafeArea) ApplyVerticalSafeArea(scaler);
        }

        private void ApplyVerticalSafeArea(CanvasScaler scaler)
        {
            float bottomPixels = Screen.safeArea.y;
            float topPixels = Screen.currentResolution.height
                              - (Screen.safeArea.y + Screen.safeArea.height);

            float bottomRatio =
                bottomPixels / Screen.currentResolution.height;
            float topRatio = topPixels / Screen.currentResolution.height;

            var referenceResolution = scaler.referenceResolution;
            _bottomUnits = referenceResolution.y * bottomRatio;
            _topUnits = referenceResolution.y * topRatio;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.offsetMin =
                new Vector2(rectTransform.offsetMin.x, _bottomUnits);
            rectTransform.offsetMax =
                new Vector2(rectTransform.offsetMax.x, -_topUnits);
        }

        private void ApplyHorizontalSafeArea(CanvasScaler scaler)
        {
            float leftPixels = Screen.safeArea.x;
            float rightPixels = Screen.currentResolution.width
                                - (Screen.safeArea.x + Screen.safeArea.width);

            float leftRatio = leftPixels / Screen.currentResolution.width;
            float rightRatio = rightPixels / Screen.currentResolution.width;

            var referenceResolution = scaler.referenceResolution;
            _leftUnits = referenceResolution.x * leftRatio;
            _rightUnits = referenceResolution.x * rightRatio;

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.offsetMin =
                new Vector2(_leftUnits, rectTransform.offsetMin.y);
            rectTransform.offsetMax =
                new Vector2(-_rightUnits, rectTransform.offsetMax.y);
        }

        [Button]
        [UsedImplicitly] // In inspector
        private void ForceApply()
        {
            var scaler = GetComponentInParent<CanvasScaler>();
            ApplySafeArea(scaler);
        }
    }
}