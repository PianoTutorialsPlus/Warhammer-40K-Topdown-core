using System.Collections;
using UnityEngine;
using WH40K.Essentials;

namespace WH40K.UI
{
    public class UIRangeIndicator : MonoBehaviour, IRangeIndicator
    {
        private float _baseSize = 0.5f;
        private RangeController _rangeController;

        public float BaseSize 
        { 
            get => _baseSize; 
            set => _baseSize = value; 
        }
        public Vector3 LocalScale
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }
        public Vector3 Position 
        {
            get => transform.position;
            set => transform.position = value;
        }
        private void Awake()
        {
            _rangeController = new RangeController(this);
        }
        public void ConnectIndicator(IUnit unit)
        {
            transform.SetParent(unit.Transform);
            _rangeController.SetPosition(unit.Transform.position);
            StartCoroutine(SetActionRadiusCoroutine(unit));
        }
        public IEnumerator SetActionRadiusCoroutine(IUnit unit)
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                _rangeController.ScaleRange(unit.UnitMover.Range);
            }
        }
    }
}