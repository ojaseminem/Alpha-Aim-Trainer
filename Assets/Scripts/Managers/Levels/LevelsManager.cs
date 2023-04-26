using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelsManager : MonoBehaviour
    {
        public void LoadLevel(Levels level)
        {
            switch (level)
            {
                case Levels.FlickingTaskGridShot:
                    LoadFlickingTaskGridShot();
                    break;
                case Levels.PrecisionTaskMicroShot:
                    LoadPrecisionTaskMicroShot();
                    break;
                case Levels.PrecisionTaskSpiderShot:
                    LoadPrecisionTaskSpiderShot();
                    break;
                case Levels.TrackingTaskMotionTrack:
                    LoadTrackingTaskMotionTrack();
                    break;
                case Levels.FlickingTaskMicroFlex:
                    LoadFlickingTaskMicroFlex();
                    break;
                case Levels.TrackingTaskStrafeBot:
                    LoadTrackingTaskStrafeBot();
                    break;
                case Levels.FlickingTaskSpiderShot180:
                    LoadFlickingTaskSpiderShot180();
                    break;
                case Levels.FlickingTaskTileFrenzy:
                    LoadFlickingTaskTileFrenzy();
                    break;
                case Levels.SwitchingTaskDecisionShot:
                    LoadSwitchingTaskDecisionShot();
                    break;
                case Levels.FlickingTaskMotionShot:
                    LoadFlickingTaskMotionShot();
                    break;
                case Levels.PrecisionTaskMicroShotSpeed:
                    LoadPrecisionTaskMicroShotSpeed();
                    break;
                case Levels.PrecisionTaskDetection:
                    LoadPrecisionTaskDetection();
                    break;
            }
        }

        private static void LoadFlickingTaskGridShot()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_FlickingTaskGridShot");
        }

        private static void LoadPrecisionTaskMicroShot()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_PrecisionTaskMicroShot");
        }

        private static void LoadPrecisionTaskSpiderShot()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_PrecisionTaskSpiderShot");
        }

        private static void LoadTrackingTaskMotionTrack()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_TrackingTaskMotionTrack");
        }

        private static void LoadFlickingTaskMicroFlex()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_FlickingTaskMicroFlex");
        }

        private static void LoadTrackingTaskStrafeBot()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_TrackingTaskStrafeBot");
        }

        private static void LoadFlickingTaskSpiderShot180()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_FlickingTaskSpiderShot180");
        }

        private static void LoadFlickingTaskTileFrenzy()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_FlickingTaskTileFrenzy");
        }

        private static void LoadSwitchingTaskDecisionShot()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_SwitchingTaskDecisionShot");
        }

        private static void LoadFlickingTaskMotionShot()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_FlickingTaskMotionShot");
        }

        private static void LoadPrecisionTaskMicroShotSpeed()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_PrecisionTaskMicroShotSpeed");
        }

        private static void LoadPrecisionTaskDetection()
        {
            SceneManager.LoadScene("Scenes/Scene_Levels/Scene_PrecisionTaskDetection");
        }
    }

    public enum Levels
    {
        FlickingTaskGridShot,
        PrecisionTaskMicroShot,
        PrecisionTaskSpiderShot,
        TrackingTaskMotionTrack,
        FlickingTaskMicroFlex,
        TrackingTaskStrafeBot,
        FlickingTaskSpiderShot180,
        FlickingTaskTileFrenzy,
        SwitchingTaskDecisionShot,
        FlickingTaskMotionShot,
        PrecisionTaskMicroShotSpeed,
        PrecisionTaskDetection,
    }
}