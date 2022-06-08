namespace WH40K.DiceEvents
{
    public interface IResult
    {
        RollTheDiceEventChannelSO DiceAction { get; }
        RollTheDiceEventChannelSO DiceSubResult { get; }
        RollTheDiceEventChannelSO DiceResult { get; }
    }
}