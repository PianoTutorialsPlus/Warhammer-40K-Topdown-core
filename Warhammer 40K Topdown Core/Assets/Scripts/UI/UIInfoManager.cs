using System.Collections.Generic;
using UnityEngine;
using WH40K.Essentials;

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
