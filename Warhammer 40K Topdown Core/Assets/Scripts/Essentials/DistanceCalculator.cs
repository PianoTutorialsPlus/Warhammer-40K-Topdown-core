public class DistanceCalculator
{
    private IPathCalculator pathCalculator;
    private float moveDistance;
    //public float RestDistance => CalculateRestDistance();

    public DistanceCalculator(IPathCalculator pathCalculator, float moveDistance)
    {
        this.pathCalculator = pathCalculator;
        this.moveDistance = moveDistance;
    }

    //public float CalculateRestDistance()
    //{
    //    return moveDistance - movedDistance - (distanceToMove - GetRemainingDistance());
    //}

}