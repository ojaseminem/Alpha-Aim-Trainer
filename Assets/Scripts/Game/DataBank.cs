using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [CreateAssetMenu(menuName = "DataBank")]
    public class DataBank : ScriptableObject
    {
        #region Crosshair Settings

        public Image Crosshair;
        
        #endregion

        #region Username Settings

        public string Username = "Player";

        #endregion
    }
}
