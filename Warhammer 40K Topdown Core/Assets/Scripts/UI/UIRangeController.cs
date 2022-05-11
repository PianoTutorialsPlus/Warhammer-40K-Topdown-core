using System;
using UnityEngine;

namespace WH40K.UI
{
    public class UIRangeController
    {
        private float _scale = 3.5f;
        private IUIRangeIndicator _rangeIndicator;
        private float _baseSize => _rangeIndicator.BaseSize;

        public UIRangeController(IUIRangeIndicator rangeIndicator)
        {
            _rangeIndicator = rangeIndicator;
        }
        public void SetPosition(Vector3 position)
        {
            _rangeIndicator.Position = position;
        }
        public void ScaleRange(float range)
        {
            if (range < 0) throw new ArgumentOutOfRangeException("Range");

            float actionRadiusXZ = (range + _baseSize) * _scale;
            Vector3 actionArea = new Vector3(actionRadiusXZ, 1, actionRadiusXZ);

            _rangeIndicator.LocalScale = actionArea;
        }
    }
}