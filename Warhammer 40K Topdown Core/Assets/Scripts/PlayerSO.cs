using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player")]
public class PlayerSO : ScriptableObject
{
    public List<Unit> _playerUnits;
}
