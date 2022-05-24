using UnityEngine;
using WH40K.PlayerEvents;

namespace WH40K.UnitHandler
{

    //[CreateAssetMenu(menuName ="Player/Active Unit")]
    public class ActiveUnitSO : ScriptableObject
    {
        public UnitFacade activeUnit;
    }
}