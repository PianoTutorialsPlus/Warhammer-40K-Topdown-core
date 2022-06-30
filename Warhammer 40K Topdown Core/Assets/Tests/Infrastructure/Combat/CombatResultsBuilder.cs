using System.Collections.Generic;
using WH40K.Gameplay.Combat;
using WH40K.Stats.Combat;

namespace Editor.Infrastructure.Combat
{
    public class CombatResultsBuilder : TestDataBuilder<CombatResults>
    {
        private int _equalizer;
        private List<int> _result = new List<int>();

        public CombatResultsBuilder() { }

        public CombatResultsBuilder WithEqualizer(int equalizer)
        {
            _equalizer = equalizer;
            return this;
        }

        public CombatResultsBuilder WithResult(int result)
        {
            if (result != 0) _result.Add(result);
            return this;
        }

        public override CombatResults Build()
        {
            var combatResults = new CombatResults(_equalizer, _result ??= new List<int>());

            return combatResults;
        }
    }

}