using UnityEngine;
using UnityEngine.Events;

namespace WH40K.Essentials
{
    internal interface IGameTable
    {
        UnityAction<Vector3> OnTapDownAction { get; }
    }
}