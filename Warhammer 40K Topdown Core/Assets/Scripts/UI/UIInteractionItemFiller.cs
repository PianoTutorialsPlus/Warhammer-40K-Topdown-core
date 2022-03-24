using TMPro;
using UnityEngine;


public class UIInteractionItemFiller : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI interactionName; // add Scriptable Object?

    [SerializeField] TextMeshProUGUI interactionKeyButton;


    public void FillInteractionPanel(InteractionSO interactionItem)
    {
        interactionName.text = interactionItem.InteractionName;
        interactionKeyButton.text = KeyCode.E.ToString(); // this keycode will be modified later on 
    }
}
