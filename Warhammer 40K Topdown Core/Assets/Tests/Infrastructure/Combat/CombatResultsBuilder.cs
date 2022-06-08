using System.Collections.Generic;
using WH40K.Gameplay.Combat;

namespace Editor.Infrastructure.Combat
{
    public class CombatResultsBuilder : TestDataBuilder<CombatResults>
    {
        private int _equalizer;
        private List<int> _result = new List<int>();

        public CombatResultsBuilder() { }

        public CombatResultsBuilder WithToHit(int toHit)
        {
            _equalizer = toHit;
            return this;
        }
        public CombatResultsBuilder WithToWound(int toWound)
        {
            _equalizer = toWound;
            return this;
        }
        public CombatResultsBuilder WithToSave(int toSave)
        {
            _equalizer = toSave;
            return this;
        }

        public CombatResultsBuilder WithResult(int result)
        {
            _result.Add(result);
            return this;
        }

        public override CombatResults Build()
        {
            var combatResults = new CombatResults(_equalizer, _result ??= new List<int>());

            return combatResults;
        }
    }

}