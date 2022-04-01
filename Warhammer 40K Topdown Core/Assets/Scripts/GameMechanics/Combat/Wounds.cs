using System;
using System.Collections.Generic;

namespace WH40K.Combat
{
    public class Wounds
    {
        private List<int> _notSaved;

        public Wounds(List<int> notSaved)
        {
            _notSaved = notSaved;
        }

        public int TakeDamage(int damage)
        {
            int wounds = 0;
            foreach (int save in _notSaved)
            {
                wounds += damage;
            }
            return wounds;
        }
    }
}