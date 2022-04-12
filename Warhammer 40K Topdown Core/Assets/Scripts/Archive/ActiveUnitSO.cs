using UnityEngine;
using WH40K.Essentials;

namespace WH40K.UnitHandler
{

    //[CreateAssetMenu(menuName ="Player/Active Unit")]
    public class ActiveUnitSO : ScriptableObject
    {
        public Unit activeUnit;
    }
}