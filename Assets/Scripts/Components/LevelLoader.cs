using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimpleMachine
{
    /// <summary>
    /// Loads main menu, V1/V2 exercise scene or reloads current scene.
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        public int mainMenuSceneIndex = 0;
        public int machineV1SceneIndex = 1;
        public int machineV2SceneIndex = 2;
        
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }

        public void LoadMachineV1()
        {
            SceneManager.LoadScene(machineV1SceneIndex);
        }

        public void LoadMachineV2()
        {
            SceneManager.LoadScene(machineV2SceneIndex);
        }

        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
