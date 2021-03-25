using UnityEngine;

namespace SimpleMachine
{
    /// <summary>
    /// Manages application data outside exercise mechanism.
    /// Counts time and mistakes.
    /// </summary>
    [CreateAssetMenu]
    public class AppStateModel : ScriptableObject
    {
        public float exerciseTimer;
        public int mistakesCounter;

        enum State
        {
            PAUSED,
            RUNNING
        }
        private State _state;

        /// <summary>
        /// Inits Model data with exercise start values.
        /// </summary>
        public void InitModel()
        {
            exerciseTimer = 0f;
            mistakesCounter = 0;
            _state = State.RUNNING;
        }

        /// <summary>
        /// Increases exercise timer if not paused.
        /// </summary>
        public void AddTime(float deltaTime)
        {
            if (_state == State.PAUSED)
                return;

            exerciseTimer += deltaTime;
        }

        /// <summary>
        /// Increases exercise mistake counter by 1.
        /// </summary>
        public void AddMistake()
        {
            mistakesCounter += 1;
        }

        /// <summary>
        /// Pauses Model.
        /// </summary>
        public void Pause()
        {
            _state = State.PAUSED;
        }
        
        /// <summary>
        /// Continues Model.
        /// </summary>
        public void Continue()
        {
            _state = State.RUNNING;
        }
    }
}
