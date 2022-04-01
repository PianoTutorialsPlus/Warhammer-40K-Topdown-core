using System;
using System.Collections.Generic;

namespace WH40K.Combat
{
    public class Shots
    {
        private int _maxShots;

        public Shots(int maxShots)
        {
            if (maxShots < 1) throw new ArgumentOutOfRangeException("Max Shots");
            _maxShots = maxShots;
        }
        public List<int> GetShots()
        {
            List<int> shots = new List<int>();

            for (int shot = 0; shot < _maxShots; shot++)
            {
                shots.Add(shot);
            }
            return shots;
        }
    }
}