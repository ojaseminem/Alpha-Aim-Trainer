using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "DataBank")]
    public class DataBank : ScriptableObject
    {
        #region Crosshair Settings

        public int crosshairSize = 10;
        public int crosshairThickness = 2;
        public int crosshairGap = 4;

        #endregion

        #region Username Settings

        public string username = "Player";

        #endregion
    }
}
