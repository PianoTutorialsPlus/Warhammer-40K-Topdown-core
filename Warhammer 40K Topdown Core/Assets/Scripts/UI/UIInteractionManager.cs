using System.Collections.Generic;
using UnityEngine;
using WH40K.Gameplay.GamePhaseEvents;

namespace
    WH40K.UI
{
    public class UIInteractionManager : MonoBehaviour
    {
        [SerializeField] private List<InteractionSO> listInteractions;

        [SerializeField] private UIInteractionItemFiller interactionItem;

        public void FillInteractionPanel(InteractionType interactionType)
        {
            if (listInteractions != null && interactionItem != null)
            {
                if (listInteractions.Exists(o => o.InteractionType == interactionType))
                {
                    interactionItem.FillInteractionPanel(listInteractions.Find(o => o.InteractionType == interactionType));
                }
            }
        }

    }
}