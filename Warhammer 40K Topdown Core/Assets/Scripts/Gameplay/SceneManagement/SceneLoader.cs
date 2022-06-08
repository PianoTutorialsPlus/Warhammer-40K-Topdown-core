using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WH40K.Gameplay.EventChannels;

namespace WH40K.Gameplay.Core
{
    /// <summary>
    /// This class manages the scene loading and unloading.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [Header("Persistent Manager Scene")]
        [SerializeField] private GameSceneSO _persistentManagerScene = default;

        [Header("Gameplay Scene")]
        [SerializeField] private GameSceneSO _gameplayScene = default;

        //Extensions for LoadEvents and Broadcast system

        private List<AsyncOperation> _scenesToLoadAsynchOperations = new List<AsyncOperation>();
        private List<Scene> _scenesToUnload = new List<Scene>();
        private GameSceneSO _activeScene; // The scene we want to set as active (for lighting/skybox)
        private List<GameSceneSO> _persistentScenes = new List<GameSceneSO>(); //Scenes to keep loaded when a load event is raised

        [Header("Load Events")]
        //The location load event we are listening to
        [SerializeField] private LoadEventChannelSO _loadLocation = default;
        //The menu load event we are listening to
        [SerializeField] private LoadEventChannelSO _loadMenu = default;

        private void OnEnable()
        {
            if (_loadLocation != null)
                _loadLocation.OnLoadingRequested += LoadLocation;

            if (_loadMenu != null) _loadMenu.OnLoadingRequested += LoadMenu;
        }

        private void OnDisable()
        {
            if (_loadLocation != null)
                _loadLocation.OnLoadingRequested -= LoadLocation;

            if (_loadMenu != null) _loadMenu.OnLoadingRequested -= LoadMenu;
        }

        /// <summary>
        /// This function loads the location scenes passed as array parameter 
        /// </summary>
        /// <param name="locationsToLoad"></param>
        /// <param name="showLoadingScreen"></param>

        private void LoadLocation(GameSceneSO[] locationToLoad, bool showLoadingScreen)
        {
            //When loading a location, we want to keep the persistent managers and gameplay scenes loaded
            _persistentScenes.Add(_persistentManagerScene);
            _persistentScenes.Add(_gameplayScene);
            AddScenesToUnload(_persistentScenes);
            //LoadScenes(locationToLoad, showLoadingScreen);

        }

        /// <summary>
        /// This function loads the menu scenes passed as array parameter 
        /// </summary>
        /// <param name="MenuToLoad"></param>
        /// <param name="showLoadingScreen"></param>

        private void LoadMenu(GameSceneSO[] MenuToLoad, bool showLoadingScreen)
        {
            //When loading a menu, we only want to keep the persistent managers scene loaded
            _persistentScenes.Add(_persistentManagerScene);
            AddScenesToUnload(_persistentScenes);
            //LoadScenes(MenuToLoad, showLoadingScreen);
        }

        //private void LoadScenes(GameSceneSO[] locationsToLoad, bool showLoadingScreen)
        //{
        //    //Take the first scene in the array as the scene we want to set active
        //    _activeScene = locationsToLoad[0];
        //    UnloadScenes();


        //}


        private void AddScenesToUnload(List<GameSceneSO> persistentScene)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                string scenePath = scene.path;
                for (int j = 0; j < persistentScene.Count; ++j)
                {
                    if (scenePath != persistentScene[j].scenePath)
                    {
                        //Check if we reached the last persistent scenes check
                        if (j == persistentScene.Count - 1)
                        {
                            //If the scene is not one of the persistent scenes, we add it to the scenes to unload
                            _scenesToUnload.Add(scene);
                        }
                    }
                    else
                    {
                        //We move the next scene check as soon as we find that the scene is one of the persistent scenes
                        break;
                    }
                }
            }
        }
        //private void UnloadScenes()
        //{
        //    if(_scenesToUnload != null)
        //    {
        //        for (int i = 0; i < _scenesToUnload.Count; ++i) 
        //            SceneManager.UnloadSceneAsync(_scenesToUnload[i]);
        //        _scenesToUnload.Clear();
        //    }
        //}

        private void ExitGame()
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }
}