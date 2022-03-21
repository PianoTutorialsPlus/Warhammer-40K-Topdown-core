using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player")]
public class PlayerSO : ScriptableObject
{
    public List<Unit> _playerUnits;
    public Fraction fraction;
}
