using UnityEngine;
using WH40K.Stats.Player;

namespace WH40K.UI
{
    public class UIInfoManager : MonoBehaviour
    {
        //[SerializeField] private List<InteractionSO> listInteractions;

        [SerializeField] private UIInfoUnitStatsFiller infoItem;

        public void FillInfoPanel(IStats interactionType)
        {
            infoItem.FillInfoPanel(interactionType);
            //if ((listInteractions != null) && (infoItem != null))
            //{
            //    if (listInteractions.Exists(o => o.InteractionType == interactionType))
            //    {

            //        infoItem.FillInfoPanel(listInteractions.Find(o => o.InteractionType == interactionType));
            //    }

            //}
        }
    }
}