using UnityEngine.UI;

namespace SimpleMachine
{
    /// <summary>
    /// Shows current time after exercise started.
    /// </summary>
    public class RuntimeCanvasView : CanvasView
    {
        public Text timeText;
        public string timeTemplate = "TIME: ";

        /// <summary>
        /// Shows formatted time on runtime canvas (not safe).
        /// </summary>
        public void SetTimeText(float time)
        {
            timeText.text = timeTemplate + $"{time:0.0}";
        }
    }
}
