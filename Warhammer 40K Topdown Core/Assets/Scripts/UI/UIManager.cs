using UnityEngine;
using WH40K.Gameplay.EventChannels;
using WH40K.Gameplay.GamePhaseEvents;
using WH40K.Gameplay.PlayerEvents;
using WH40K.Stats.Player;

namespace WH40K.UI
{
    public class UIManager : MonoBehaviour
    {

        public VoidEventChannelSO OnInteractionEndedEvent;

        public InteractionUIEventChannelSO SetInteractionEvent;
        public InfoUIEventChannelSO SetInfoEvent;
        public InfoUIEventChannelSO SetEnemyInfoEvent;
        public IndicatorUIEventChannelSO ConnectIndicatorEvent;
        public GameInfoUIEventChannelSO SetGameinfoEvent;

        //public PhaseEventChannelSO SetPhaseEvent;
        //private UnitFacade unit = default;


        private void OnEnable()
        {
            //Check if the event exists to avoid errors
            if (SetInteractionEvent != null) SetInteractionEvent.OnEventRaised += SetInteractionPanel;
            if (SetInfoEvent != null) SetInfoEvent.OnEventRaised += SetInfoPanel;
            if (SetEnemyInfoEvent != null) SetEnemyInfoEvent.OnEventRaised += SetEnemyInfoPanel;
            if (ConnectIndicatorEvent != null) ConnectIndicatorEvent.OnEventRaised += SetIndicatorConnection;
            if (SetGameinfoEvent != null) SetGameinfoEvent.OnEventRaised += SetGameinfoPanel;

        }

        [SerializeField] UIInteractionManager interactionPanel;
        [SerializeField] UIInfoManager infoPanel;
        [SerializeField] UIInfoManager enemyInfoPanel;
        [SerializeField] UIRangeIndicator distanceIndicator;
        [SerializeField] UIGameInfoManager gameinfoPanel;

        public void SetInteractionPanel(bool isOpenEvent, InteractionType interactionType)
        {
            if (isOpenEvent)
                interactionPanel.FillInteractionPanel(interactionType);
            interactionPanel.gameObject.SetActive(isOpenEvent);
        }

        public void SetInfoPanel(bool isOpenEvent, IStats unit)
        {
            if (isOpenEvent)
            {
                infoPanel.FillInfoPanel(unit);
            }
            infoPanel.gameObject.SetActive(isOpenEvent);
        }

        public void SetEnemyInfoPanel(bool isOpenEvent, IStats unit)
        {
            if (isOpenEvent)
            {
                enemyInfoPanel.FillInfoPanel(unit);
            }

            enemyInfoPanel.gameObject.SetActive(isOpenEvent);

        }

        public void SetIndicatorConnection(bool isOpenEvent, IUnit unit)
        {
            if (isOpenEvent)
            {
                distanceIndicator.ConnectIndicator(unit);
            }
            distanceIndicator.gameObject.SetActive(isOpenEvent);
        }

        public void SetGameinfoPanel(bool isOpenEvent)
        {
            if (isOpenEvent)
            {
                gameinfoPanel.FillInfoPanel();
            }

            gameinfoPanel.gameObject.SetActive(isOpenEvent);
        }


    }
}