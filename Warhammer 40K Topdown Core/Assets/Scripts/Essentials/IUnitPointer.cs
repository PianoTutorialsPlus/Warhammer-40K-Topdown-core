﻿using UnityEngine.Events;

namespace WH40K.Essentials
{
    public interface IUnitPointer
    {
        UnityAction<Unit> OnTapDownAction { get; set; }
        UnityAction OnPointerEnter { get; set; }
        UnityAction<IUnit> OnPointerEnterInfo { get; set; }
        UnityAction<IUnit> OnPointerExit { get; set; }
    }
}