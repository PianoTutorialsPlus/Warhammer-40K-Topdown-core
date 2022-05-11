using System.Collections.Generic;
using WH40K.EventChannels;

/// <summary>
/// This script executes the calls from the shooting phase manager in the specific state.
/// </summary>
namespace WH40K.GamePhaseEvents
{
    public abstract class ShootingSubPhases
    {
        protected IResult _result;

        public ShootingSubPhases(IResult result)
        {
            _result = result;
        }
        public abstract ShootingSubEvents SubEvents { get; } // gets the active subphase
        public abstract void HandleShooting(List<int> parameter); // handles the subphases
        public void Next() { CombatProcessor.Next(SubEvents); }
    }

    public class SelectEnemy : ShootingSubPhases
    {
        public SelectEnemy(IResult result) : base(result) { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.SelectEnemy;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action(SubEvents, parameter);
        }
    }

    public class Hit : ShootingSubPhases
    {
        public Hit(IResult result) : base(result) { }

        public override ShootingSubEvents SubEvents => ShootingSubEvents.Hit;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action(SubEvents, parameter);
        }
    }

    public class Wound : ShootingSubPhases
    {
        public Wound(IResult result) : base(result) { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Wound;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action(SubEvents, parameter);
        }
    }

    public class Save : ShootingSubPhases
    {
        public Save(IResult result) : base(result) { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Save;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action(SubEvents, parameter);
        }
    }
    public class Damage : ShootingSubPhases
    {
        public Damage(IResult result) : base(result) { }
        public override ShootingSubEvents SubEvents => ShootingSubEvents.Damage;
        public override void HandleShooting(List<int> parameter)
        {
            CombatProcessor.Action(SubEvents, parameter);
        }
    }
}