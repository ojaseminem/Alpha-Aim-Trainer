using UnityEngine;

namespace Menu
{
    public class CrossHairHandler : MonoBehaviour
    {
        [SerializeField] private Transform crossHair;
        
        [SerializeField] private Transform settingsHolder;

        public void MoveToCenter()
        {
            crossHair.transform.SetParent(transform);
            crossHair.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
        public void MoveToSettings()
        {
            crossHair.transform.SetParent(settingsHolder);
            crossHair.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }
}
