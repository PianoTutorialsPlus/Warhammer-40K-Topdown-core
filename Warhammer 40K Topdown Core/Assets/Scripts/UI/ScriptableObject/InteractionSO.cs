using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Interaction", menuName ="UI/Interaction")]
public class InteractionSO : ScriptableObject
{
    [Tooltip("The interaction name")]
    [SerializeField] private string _interactionName;

    [Tooltip("The interaction Type")]
    [SerializeField] private InteractionType _interactionType;

    public string InteractionName => _interactionName;
    public InteractionType InteractionType => _interactionType;
}
