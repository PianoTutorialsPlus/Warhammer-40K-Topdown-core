using UnityEngine;
namespace WH40K.Gameplay.Core
{
    /// <summary>
    /// This class is a base class which contains what is common to all game scenes (Locations or Menus)
    /// </summary>
    public abstract partial class GameSceneSO : ScriptableObject
    {
        [Header("Information")]
#if UNITY_EDITOR
        public UnityEditor.SceneAsset sceneAsset;
#endif
        [HideInInspector]
        public string scenePath;
        [TextArea] public string shortDiscription;

        [Header("Sounds")]
        public AudioClip music;

    }
}
