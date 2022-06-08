using System;
using System.Collections.Generic;

namespace WH40K.Gameplay.Combat
{
    public class CombatResults
    {
        private int _threshhold;
        private List<int> _result;

        public List<int> Hits => GetResults(true);
        public List<int> Wounds => GetResults(true);
        public List<int> FailedSaves => GetResults(false);

        public CombatResults(int threshhold, List<int> result)
        {
            if (threshhold < 1 || threshhold > 6) throw new ArgumentOutOfRangeException("Combat Result Threshhold");
            foreach (int hit in result)
            {
                if (hit < 1 || hit > 6) throw new ArgumentOutOfRangeException("Combat Result");
            }

            _threshhold = threshhold;
            _result = result;
        }
        private List<int> GetResults(bool resultsGreaterThreshhold)
        {
            List<int> resultList = new List<int>();

            foreach (int result in _result)
            {
                if (resultsGreaterThreshhold) AddResultsGreaterAndEqualThreshhold(resultList, result);
                else AddResultsLowerThreshhold(resultList, result);
            }
            return resultList;
        }
        private void AddResultsLowerThreshhold(List<int> resultList, int result)
        {
            if (result < _threshhold) resultList.Add(result);
        }
        private void AddResultsGreaterAndEqualThreshhold(List<int> resultList, int result)
        {
            if (result >= _threshhold) resultList.Add(result);
        }
    }
}