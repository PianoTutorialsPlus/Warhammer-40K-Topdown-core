using System.Collections.Generic;
using UnityEngine;

public class UIInfoManager : MonoBehaviour
{
    [SerializeField] private List<InteractionSO> listInteractions;

    [SerializeField] private UIInfoUnitStatsFiller infoItem;

    public void FillInfoPanel(Unit interactionType)
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
