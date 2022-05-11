using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace WH40K.InputEvents
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions
    {
        // Assign delegate{} to events to initialise them with an empty delegate
        // so we can skip the null check when we use them

        // public ActiveUnitSO _unit;

        // Gameplay
        public event UnityAction ActivateEvent = delegate { };
        public event UnityAction ExecuteEvent = delegate { };

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
                ActivateEvent.Invoke();
            }

        }

        public void OnExecute(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                ExecuteEvent.Invoke();
                Debug.Log("Execute");
            }

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
}