using UnityEngine;

namespace Menu
{
    public class LevelDescriptionBank : MonoBehaviour
    {
        //Flicking Task Grid Shot Level
        [HideInInspector] public string flickingTaskGridShotLevelName;
        [HideInInspector] public string flickingTaskGridShotLevelDescription;
        public Sprite flickingTaskGridShotLevelImage;
        //Precision Task Micro Shot Level
        [HideInInspector] public string precisionTaskMicroShotLevelName;
        [HideInInspector] public string precisionTaskMicroShotLevelDescription;
        public Sprite precisionTaskMicroShotLevelImage;
        //Precision Task Spider Shot Level
        [HideInInspector] public string precisionTaskSpiderShotLevelName;
        [HideInInspector] public string precisionTaskSpiderShotLevelDescription;
        public Sprite precisionTaskSpiderShotLevelImage;
        //Tracking Task Motion Track Level
        [HideInInspector] public string trackingTaskMotionTrackLevelName;
        [HideInInspector] public string trackingTaskMotionTrackLevelDescription;
        public Sprite trackingTaskMotionTrackLevelImage;
        //Flicking Task Micro Flex Level
        [HideInInspector] public string flickingTaskMicroFlexLevelName;
        [HideInInspector] public string flickingTaskMicroFlexLevelDescription;
        public Sprite flickingTaskMicroFlexLevelImage;
        //Tracking Task Strafe Bot Level
        [HideInInspector] public string trackingTaskStrafeBotLevelName;
        [HideInInspector] public string trackingTaskStrafeBotLevelDescription;
        public Sprite trackingTaskStrafeBotLevelImage;
        //Flicking Task Spider Shot 180 Level
        [HideInInspector] public string flickingTaskSpiderShot180LevelName;
        [HideInInspector] public string flickingTaskSpiderShot180LevelDescription;
        public Sprite flickingTaskSpiderShot180LevelImage;
        //Flicking Task Tile Frenzy Level
        [HideInInspector] public string flickingTaskTileFrenzyLevelName;
        [HideInInspector] public string flickingTaskTileFrenzyLevelDescription;
        public Sprite flickingTaskTileFrenzyLevelImage;
        //Switching Task Decision Shot Level
        [HideInInspector] public string switchingTaskDecisionShotLevelName;
        [HideInInspector] public string switchingTaskDecisionShotLevelDescription;
        public Sprite switchingTaskDecisionShotLevelImage;
        //Flicking Task Motion Shot Level
        [HideInInspector] public string flickingTaskMotionShotLevelName;
        [HideInInspector] public string flickingTaskMotionShotLevelDescription;
        public Sprite flickingTaskMotionShotLevelImage;
        //Precision Task Micro Shot Speed Level
        [HideInInspector] public string precisionTaskMicroShotSpeedLevelName;
        [HideInInspector] public string precisionTaskMicroShotSpeedLevelDescription;
        public Sprite precisionTaskMicroShotSpeedLevelImage;
        //Precision Task Detection Level
        [HideInInspector] public string precisionTaskDetectionLevelName;
        [HideInInspector] public string precisionTaskDetectionLevelDescription;
        public Sprite precisionTaskDetectionLevelImage;


        //Assign String Variables
        private void Start()
        {
            //Flicking Task Grid Shot Level
            flickingTaskGridShotLevelName = "Flicking Task Grid Shot Level";
            flickingTaskGridShotLevelDescription = "Description for Flicking Task Grid Shot Level";
            //Precision Task Micro Shot Level
            precisionTaskMicroShotLevelName = "Precision Task Micro Shot Level";
            precisionTaskMicroShotLevelDescription = "Description for Precision Task Micro Shot Level";
            //Precision Task Spider Shot Level
            precisionTaskSpiderShotLevelName = "Precision Task Spider Shot Level";
            precisionTaskSpiderShotLevelDescription = "Description for Precision Task Spider Shot Level";
            //Tracking Task Motion Track Level
            trackingTaskMotionTrackLevelName = "Tracking Task Motion Track Level";
            trackingTaskMotionTrackLevelDescription = "Description for Tracking Task Motion Track Level";
            //Flicking Task Micro Flex Level
            flickingTaskMicroFlexLevelName = "Flicking Task Micro Flex Level";
            flickingTaskMicroFlexLevelDescription = "Description for Flicking Task Micro Flex Level";
            //Tracking Task Strafe Bot Level
            trackingTaskStrafeBotLevelName = "Tracking Task Strafe Bot Level";
            trackingTaskStrafeBotLevelDescription = "Description for Tracking Task Strafe Bot Level";
            //Flicking Task Spider Shot 180 Level
            flickingTaskSpiderShot180LevelName = "Flicking Task Spider Shot 180 Level";
            flickingTaskSpiderShot180LevelDescription = "Description for Flicking Task Spider Shot 180 Level";
            //Flicking Task Tile Frenzy Level
            flickingTaskTileFrenzyLevelName = "Flicking Task Tile Frenzy Level";
            flickingTaskTileFrenzyLevelDescription = "Description for Flicking Task Tile Frenzy Level";
            //Switching Task Decision Shot Level
            switchingTaskDecisionShotLevelName = "Switching Task Decision Shot Level";
            switchingTaskDecisionShotLevelDescription = "Description for Switching Task Decision Shot Level";
            //Flicking Task Motion Shot Level
            flickingTaskMotionShotLevelName = "Flicking Task Motion Shot Level";
            flickingTaskMotionShotLevelDescription = "Description for Flicking Task Motion Shot Level";
            //Precision Task Micro Shot Speed Level
            precisionTaskMicroShotSpeedLevelName = "Precision Task Micro Shot Speed Level";
            precisionTaskMicroShotSpeedLevelDescription = "Description for Precision Task Micro Shot Speed Level";
            //Precision Task Detection Level
            precisionTaskDetectionLevelName = "Precision Task Detection Level";
            precisionTaskDetectionLevelDescription = "Description for Precision Task Detection Level";
        }
    }
}