using UnityEngine;

namespace SimpleMachine
{
    /// <summary>
    /// Controller for application events outside exercise mechanism.
    /// Manages canvases and level loading.
    /// </summary>
    public class AppStateController : MonoBehaviour
    {
        public AppStateModel appStateModel;
        public MachineController machineController;

        public RuntimeCanvasView runtimeCanvas;
        public ResultCanvasView resultCanvas;
        public MistakeCanvasView mistakeCanvas;

        public LevelLoader levelLoader;

        private void Start()
        {
            appStateModel.InitModel();
            machineController.OnUserMistake += MistakeHandler;
            machineController.OnExerciseComplete += ExerciseCompleteHandler;
            
            SetCanvasActivities(true, false, false);
            mistakeCanvas.continueButton.onClick.AddListener(ContinueHandler);
            mistakeCanvas.mainMenuButton.onClick.AddListener(MainMenuHandler);
            mistakeCanvas.restartButton.onClick.AddListener(RestartHandler);
            resultCanvas.mainMenuButton.onClick.AddListener(MainMenuHandler);
        }

        private void Update()
        {
            appStateModel.AddTime(Time.deltaTime);
            runtimeCanvas.SetTimeText(appStateModel.exerciseTimer);
        }

        /// <summary>
        /// Provides mistake info to Model.
        /// Sets mistake canvas active.
        /// </summary>
        private void MistakeHandler()
        {
            appStateModel.AddMistake();
            appStateModel.Pause();
            SetCanvasActivities(false, false, true);
        }
        
        /// <summary>
        /// Provides finish info to Model.
        /// Delivers Model data to result canvas.
        /// </summary>
        private void ExerciseCompleteHandler()
        {
            appStateModel.Pause();
            SetCanvasActivities(false, true, false);
            resultCanvas.SetTimeText(appStateModel.exerciseTimer);
            resultCanvas.SetMistakesText(appStateModel.mistakesCounter);
        }

        /// <summary>
        /// Loads main menu via LevelLoader.
        /// </summary>
        private void MainMenuHandler()
        {
            levelLoader.LoadMainMenu();
        }

        /// <summary>
        /// Returns model and canvas to runtime state.
        /// </summary>
        private void ContinueHandler()
        {
            appStateModel.Continue();
            SetCanvasActivities(true, false, false);
        }

        /// <summary>
        /// Restarts exercise via LevelLoader.
        /// </summary>
        private void RestartHandler()
        {
            levelLoader.ReloadCurrentScene();
        }
        
        private void SetCanvasActivities(bool runtime, bool result, bool mistake)
        {
            runtimeCanvas.SetActivity(runtime);
            resultCanvas.SetActivity(result);
            mistakeCanvas.SetActivity(mistake);
        }
    }
}
