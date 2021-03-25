using UnityEngine;

namespace SimpleMachine
{
    /// <summary>
    /// Base class for exercise canvas views. Contains Canvas component and sets activity to it.
    /// </summary>
    public class CanvasView : MonoBehaviour
    {
        public Canvas canvas;
        public Collider touchBlocker;

        /// <summary>
        /// Sets activity to canvas.
        /// </summary>
        public void SetActivity(bool active)
        {
            if (touchBlocker != null)
            {
                touchBlocker.enabled = active;
            }

            if (canvas != null)
            {
                canvas.enabled = active;
            }
        }
    }
}
