using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitSO : ScriptableObject
{
    [Tooltip("Name of the Unit")]
    [SerializeField] private string _name = default;

    [Tooltip("Movementrange")]
    [SerializeField] private int _movement = default;

    [Tooltip("Weapon Skill")]
    [SerializeField] private int _weaponSkill = default;

    [Tooltip("Ballistic Skill")]
    [SerializeField] private int _ballisticSkill = default;

    [Tooltip("Strength of the Unit")]
    [SerializeField] private int _strength = default;

    [Tooltip("Toughness of the Unit")]
    [SerializeField] private int _toughness = default;

    [Tooltip("Wounds / Livepoints")]
    [SerializeField] private int _wounds = default;

    [Tooltip("Attacks of the Unit")]
    [SerializeField] private int _attack = default;

    [Tooltip("Leadership")]
    [SerializeField] private int _leadership = default;

    [Tooltip("Armour Save")]
    [SerializeField] private int _armourSave = default;


    public new string name { get => _name; }

    public int Movement { get => _movement; }

}




