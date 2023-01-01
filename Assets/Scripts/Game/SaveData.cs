namespace Game
{
    [System.Serializable]
    
    public class SaveData
    {
        //Player Settings
        public string username = "Player 0";
        public float mouseSens = 1;
        
        //CrossHair Settings
        public int size = 10;
        public int thickness = 2;
        public int gap = 5;
        public int color = 0;
        
        //Volume / Audio Settings
        public float masterVolume = 1;
        public float musicVolume = 1;
        public float sfxVolume = 1;
        public float ambientVolume = 1;
    }
}