using NSubstitute;
using UnityEngine;
using WH40K.NavMesh;

namespace Editor.Infrastructure.Player
{
    public class PathCalculatorBuilder : TestDataBuilder<IPathCalculator>
    {
        private Vector3 _endPosition = Vector3.zero;

        public PathCalculatorBuilder()
        {
        }
        public PathCalculatorBuilder WithEndPosition(Vector3 endPosition)
        {
            _endPosition = endPosition;
            return this;
        }

        public override IPathCalculator Build()
        {
            var pathCalculator = Substitute.For<IPathCalculator>();
            pathCalculator.GetEndPosition(Arg.Any<Vector3>()).Returns(_endPosition);
            pathCalculator.GetEndPosition(Arg.Any<Vector3>(), Arg.Any<float>()).Returns(_endPosition);
            return pathCalculator;
        }
    }
}
