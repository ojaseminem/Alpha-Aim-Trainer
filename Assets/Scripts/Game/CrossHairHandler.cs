using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CrossHairHandler : MonoBehaviour
    {
        [Header("Main")] [SerializeField] private SimpleCrosshair simpleCrossHair;

        [SerializeField] private int size = 10;
        [SerializeField] private int thickness = 2;
        [SerializeField] private int gap = 5;
        [SerializeField] private int color;

        [Header("Size")] [SerializeField] private TMP_InputField sizeInputText;
        [SerializeField] private Slider sizeInputSlider;

        [Header("Thickness")] [SerializeField] private TMP_InputField thicknessInputText;
        [SerializeField] private Slider thicknessInputSlider;

        [Header("Gap")] [SerializeField] private TMP_InputField gapInputText;
        [SerializeField] private Slider gapInputSlider;

        [Header("Color")] [SerializeField] private TMP_Dropdown colorInputDropDown;
        private Color _colorValue;

        private void Awake()
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            SaveLoadManager.LoadGame();

            size = SaveLoadManager.CurrentSaveData.size;
            thickness = SaveLoadManager.CurrentSaveData.thickness;
            gap = SaveLoadManager.CurrentSaveData.gap;
            color = SaveLoadManager.CurrentSaveData.color;
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            sizeInputSlider.value = size;
            sizeInputText.text = size.ToString();
            thicknessInputSlider.value = thickness;
            thicknessInputText.text = thickness.ToString();
            gapInputSlider.value = gap;
            gapInputText.text = gap.ToString();
            colorInputDropDown.value = color;
            simpleCrossHair.SetSize(size, true);
            simpleCrossHair.SetThickness(thickness, true);
            simpleCrossHair.SetGap(gap, true);
            UpdateColor();
        }

        public void UpdateSize(bool sliderInput)
        {
            int.TryParse(sizeInputText.text, out var sizeText);
            size = sliderInput ? (int)sizeInputSlider.value : sizeText;
            sizeInputSlider.value = size;
            sizeInputText.text = size.ToString();
            simpleCrossHair.SetSize(size, true);
        }

        public void UpdateThickness(bool sliderInput)
        {
            int.TryParse(thicknessInputText.text, out var thicknessText);
            thickness = sliderInput ? (int)thicknessInputSlider.value : thicknessText;
            thicknessInputSlider.value = thickness;
            thicknessInputText.text = thickness.ToString();
            simpleCrossHair.SetThickness(thickness, true);
        }

        public void UpdateGap(bool sliderInput)
        {
            int.TryParse(gapInputText.text, out var gapText);
            gap = sliderInput ? (int)gapInputSlider.value : gapText;
            gapInputSlider.value = gap;
            gapInputText.text = gap.ToString();
            simpleCrossHair.SetGap(gap, true);
        }

        public void UpdateColor()
        {
            color = colorInputDropDown.value;
            ColorNumToColor(color);
            simpleCrossHair.SetColor(_colorValue, true);
        }

        private void ColorNumToColor(int localColor)
        {
            switch (localColor)
            {
                case 0:
                    _colorValue = Color.green;
                    break;
                case 1:
                    _colorValue = Color.cyan;
                    break;
                case 2:
                    _colorValue = Color.magenta;
                    break;
                case 3:
                    _colorValue = Color.red;
                    break;
                case 4:
                    _colorValue = Color.yellow;
                    break;
                case 5:
                    _colorValue = Color.white;
                    break;
            }
        }

        public void ResetCrossHairToDefault()
        {
            var saveData = new SaveData();
            size = saveData.size;
            thickness = saveData.thickness;
            gap = saveData.gap;
            color = saveData.color;
            UpdateSettings();
        }

        public void SaveSettings()
        {
            SaveLoadManager.CurrentSaveData.size = size;
            SaveLoadManager.CurrentSaveData.thickness = thickness;
            SaveLoadManager.CurrentSaveData.gap = gap;
            SaveLoadManager.CurrentSaveData.color = color;
            SaveLoadManager.SaveGame();
        }
    }
}