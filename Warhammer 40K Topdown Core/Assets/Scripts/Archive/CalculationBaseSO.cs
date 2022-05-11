using UnityEngine;
using WH40K.EventChannels;

public abstract class CalculationBaseSO : ScriptableObject
{
    public RollTheDiceEventChannelSO rollDices;
    public RollTheDiceEventChannelSO rollSubResult;
    public RollTheDiceEventChannelSO rollDiceResult;
}