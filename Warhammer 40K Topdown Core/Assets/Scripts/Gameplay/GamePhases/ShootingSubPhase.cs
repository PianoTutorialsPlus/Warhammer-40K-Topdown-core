using System.Collections.Generic;

/// <summary>
/// This script executes the calls from the shooting phase manager in the specific state.
/// </summary>
namespace WH40K.Gameplay.GamePhaseEvents
{
    public abstract class ShootingSubPhases
    {
        public ShootingSubPhases() { }

        public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
        public abstract void HandleShooting(List<int> parameter); // handles the subphases
        public void Next() { CombatProcessor.Next(SubEvents); }
    }

    public class SelectEnemy : ShootingSubPhases
    {
        public SelectEnemy() { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action(SubEvents, parameter);
        }
    }

    public class Hit : ShootingSubPhases
    {
        public Hit() { }

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action(SubEvents, parameter);
        }
    }

    public class Wound : ShootingSubPhases
    {
        public Wound() { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action(SubEvents, parameter);
        }
    }

    public class Save : ShootingSubPhases
    {
        public Save() { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Save;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action(SubEvents, parameter);
        }
    }
    public class Damage : ShootingSubPhases
    {
        public Damage() { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action(SubEvents, parameter);
        }
    }
}