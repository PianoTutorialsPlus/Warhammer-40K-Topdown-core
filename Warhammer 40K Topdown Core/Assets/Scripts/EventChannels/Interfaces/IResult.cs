namespace WH40K.EventChannels
{
    public interface IResult
    {
        RollTheDiceEventChannelSO DiceAction { get; }
        RollTheDiceEventChannelSO DiceSubResult { get; }
        RollTheDiceEventChannelSO DiceResult { get; }
    }
}