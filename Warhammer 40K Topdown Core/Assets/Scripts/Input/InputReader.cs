using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    // Assign delegate{} to events to initialise them with an empty delegate
    // so we can skip the null check when we use them

    // public ActiveUnitSO _unit;

    // Gameplay
    public event UnityAction activateEvent = delegate { };
    public event UnityAction executeEvent = delegate { };

    private GameInput gameInput;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            gameInput.Gameplay.SetCallbacks(this);
        }
        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }



    public void OnCurserControl(InputAction.CallbackContext context)
    {

    }

    public void OnActivate(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Button pressed");
            activateEvent.Invoke();
        }

    }

    public void OnExecute(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            executeEvent.Invoke();
    }

    public void EnableGameplayInput()
    {
        gameInput.Gameplay.Enable();
    }


    public void DisableAllInput()
    {
        gameInput.Gameplay.Disable();
    }


}
