using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WH40K.UI;

namespace Editor.Infrastructure
{
    public class RangeControllerBuilder : TestDataBuilder<RangeController>
    {
        private IRangeIndicator _rangeIndicator;
        private Vector3 _position;

        public RangeControllerBuilder()
        {
        }

        public RangeControllerBuilder WithRangeIndicator(IRangeIndicator range)
        {
            _rangeIndicator = range;
            return this;
        }
        public RangeControllerBuilder WithPosition(Vector3 position)
        {
            _position = position;
            return this;
        }

        public override RangeController Build()
        {
            var rangeController = new RangeController(_rangeIndicator);
            rangeController.SetPosition(_position);
            return rangeController;
        }
    }
}
