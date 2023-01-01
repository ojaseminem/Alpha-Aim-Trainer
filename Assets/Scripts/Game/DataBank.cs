using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [CreateAssetMenu(menuName = "DataBank")]
    public class DataBank : ScriptableObject
    {
        #region Crosshair Settings

        public Image crossHair;
        
        #endregion

        #region Username Settings

        public string username = "Player";

        #endregion
    }
}
