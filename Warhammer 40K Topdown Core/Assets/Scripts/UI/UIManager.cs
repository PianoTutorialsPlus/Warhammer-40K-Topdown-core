using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public VoidEventChannelSO OnInteractionEndedEvent;

    public InteractionUIEventChannelSO SetInteractionEvent;
    public InfoUIEventChannelSO SetInfoEvent;
    public InfoUIEventChannelSO SetEnemyInfoEvent;
    private Unit unit = default;



    private void OnEnable()
    {
        //Check if the event exists to avoid errors
        if (SetInteractionEvent != null) SetInteractionEvent.OnEventRaised += SetInteractionPanel;
        if (SetInfoEvent != null) SetInfoEvent.OnEventRaised += SetInfoPanel;
        if (SetEnemyInfoEvent != null) SetEnemyInfoEvent.OnEventRaised += SetEnemyInfoPanel;

    }
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    [SerializeField] UIInteractionManager interactionPanel;
    [SerializeField] UIInfoManager infoPanel;
    [SerializeField] UIInfoManager enemyInfoPanel;



    public void SetInteractionPanel(bool isOpenEvent, InteractionType interactionType)
    {
        if (isOpenEvent) 
            interactionPanel.FillInteractionPanel(interactionType);
        interactionPanel.gameObject.SetActive(isOpenEvent);
    }
    
    public void SetInfoPanel(bool isOpenEvent, Unit interactionType)
    {
        if (isOpenEvent) {
            infoPanel.FillInfoPanel(interactionType);
            }

        infoPanel.gameObject.SetActive(isOpenEvent);

    }

    public void SetEnemyInfoPanel(bool isOpenEvent, Unit interactionType)
    {
        if (isOpenEvent)
        {
            enemyInfoPanel.FillInfoPanel(interactionType);
        }

        enemyInfoPanel.gameObject.SetActive(isOpenEvent);

    }

}
