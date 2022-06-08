using UnityEngine;
using WH40K.Gameplay.GamePhaseEvents;

namespace WH40K.Gameplay
{
    public class Interaction
    {
        public static Interaction NONE = new Interaction(InteractionType.None, null);

        public InteractionType Type;
        public GameObject InteractableObject;

        public Interaction(InteractionType type, GameObject obj)
        {
            Type = type;
            InteractableObject = obj;
        }
    }
}