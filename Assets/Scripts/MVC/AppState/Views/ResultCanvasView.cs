using UnityEngine.UI;

namespace SimpleMachine
{
    /// <summary>
    /// Contains main menu button. Displays time and mistakes statistics.
    /// </summary>
    public class ResultCanvasView : CanvasView
    {
        public Text timeText;
        public string timeTemplate = "TIME: ";
        public Text mistakesText;
        public string mistakesTemplate = "MISTAKES: ";
        
        public Button mainMenuButton;

        /// <summary>
        /// Shows formatted time on result canvas safely.
        /// </summary>
        public void SetTimeText(float time)
        {
            if (timeText == null)
                return;
            
            timeText.text = timeTemplate + $"{time:0.0}";
        }
        
        /// <summary>
        /// Shows mistakes count on result canvas safely.
        /// </summary>
        public void SetMistakesText(int mistakes)
        {
            if (mistakesText == null)
                return;
            
            mistakesText.text = mistakesTemplate + mistakes;
        }
    }
}
