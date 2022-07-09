using System;
using System.Collections.Generic;
using WH40K.Stats;

/// <summary>
/// This script executes the calls from the shooting phase manager in the specific state.
/// </summary>
namespace WH40K.Gameplay.GamePhaseEvents
{
    public abstract class ShootingSubPhases : PhasesBase
    {
        public ShootingSubPhases() { }

        public abstract void HandleShooting(List<int> parameter); // handles the subphases
        public void Next() { CombatProcessor.Next((ShootingSubEvents)SubEvents); }
    }

    public class SelectEnemy : ShootingSubPhases
    {
        public SelectEnemy() { }
        public override Enum SubEvents => ShootingSubEvents.SelectEnemy;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action((ShootingSubEvents)SubEvents, parameter);
        }
    }

    public class Hit : ShootingSubPhases
    {
        public Hit() { }

        public override Enum SubEvents => ShootingSubEvents.Hit;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action((ShootingSubEvents)SubEvents, parameter);
        }
    }

    public class Wound : ShootingSubPhases
    {
        public Wound() { }
        public override Enum SubEvents => ShootingSubEvents.Wound;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action((ShootingSubEvents)SubEvents, parameter);
        }
    }

    public class Save : ShootingSubPhases
    {
        public Save() { }
        public override Enum SubEvents => ShootingSubEvents.Save;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action((ShootingSubEvents)SubEvents, parameter);
        }
    }
    public class Damage : ShootingSubPhases
    {
        public Damage() { }
        public override Enum SubEvents => ShootingSubEvents.Damage;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action((ShootingSubEvents)SubEvents, parameter);
        }
    }
}