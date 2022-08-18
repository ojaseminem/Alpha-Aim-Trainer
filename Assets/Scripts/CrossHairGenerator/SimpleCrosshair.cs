using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CrosshairColorChannel
{
    RED,
    GREEN,
    BLUE,
    ALPHA
}

[System.Serializable]
public class Crosshair
{
    [Range(1, 150), Tooltip("Controls the length of each crosshair line.")]
    public int size = 10;

    [Range(1, 100), Tooltip("Controls the width of each crosshair line.")]
    public int thickness = 2;

    [Range(0, 350), Tooltip("Controls the distance between the center of the crosshair and the start of each crosshair line.")]
    public int gap = 5;

    [Tooltip("Specifies the color of the crosshair.")]
    public Color color = Color.green;

    public int SizeNeeded
    {
        private set { }
        get
        {
            int width = size + size + gap + gap;
            return width > thickness ? width : thickness;
        }
    }
}

public class SimpleCrosshair : MonoBehaviour
{
    [SerializeField, Tooltip("Contains properties that Specify how the crosshair looks.")]
    private Crosshair m_crosshair = null;

    [Tooltip("Specifies the image to draw the crosshair to. If you leave this empty, this script generates a Canvas and an Image with the correct settings for you.")]
    public Image m_crosshairImage;

    //New Variables
    [SerializeField] private Image crossHairImage;
    [SerializeField] private Slider crossHairSizeSlider;
    [SerializeField] private Slider crossHairThicknessSlider;
    [SerializeField] private Slider crossHairGapSlider;
    [SerializeField] private TMP_InputField crossHairSizeInput;
    [SerializeField] private TMP_InputField crossHairThicknessInput;
    [SerializeField] private TMP_InputField crossHairGapInput;

    public DataBank dataBank;

    private void Awake()
    {
        if(m_crosshairImage == null)
        {
            InitialiseCrossHairImage();
        }

        SetDefaultCrossHairSettings();
        SetCrossHairSettings();
        GenerateCrosshair();
    }

    public void InitialiseCrossHairImage()
    {
        /*GameObject crossHairGameObject = new GameObject();
        crossHairGameObject.name = "CrossHair Canvas";

        Canvas crossHairCanvas = crossHairGameObject.AddComponent<Canvas>();
        crossHairCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        crossHairGameObject.AddComponent<CanvasScaler>();

        GameObject imageGameObject = new GameObject();
        imageGameObject.name = "CrossHair Image";
        imageGameObject.transform.parent = crossHairGameObject.transform;*/

        m_crosshairImage = crossHairImage;
        m_crosshairImage.raycastTarget = false;
    }

    public void GenerateCrosshair()
    {
        Texture2D crosshairTexture =  DrawCrosshair(m_crosshair);

        m_crosshairImage.rectTransform.sizeDelta = new Vector2(m_crosshair.SizeNeeded, m_crosshair.SizeNeeded);
        Sprite crosshairSprite = Sprite.Create(crosshairTexture,
            new Rect(0, 0, crosshairTexture.width, crosshairTexture.height),
            Vector2.one / 2);

        m_crosshairImage.sprite = crosshairSprite;
    }

    /*public void SetColor(CrosshairColorChannel channel, int value, bool redrawCrosshair)  // Set between 0 and 255
    {
        switch (channel)
        {
            case CrosshairColorChannel.RED:
                {
                    m_crosshair.color.r = value / 255.0f;
                }
                break;
            case CrosshairColorChannel.GREEN:
                {
                    m_crosshair.color.g = value / 255.0f; ;
                }
                break;
            case CrosshairColorChannel.BLUE:
                {
                    m_crosshair.color.b = value / 255.0f; ;
                }
                break;
            case CrosshairColorChannel.ALPHA:
                {
                    m_crosshair.color.a = value / 255.0f; ;
                }
                break;
        }
        if (redrawCrosshair)
        {
            GenerateCrosshair();
        }
    }*/

    public void SetColor(Color color, bool redrawCrosshair)
    {
        m_crosshair.color = color;
        if (redrawCrosshair)
        {
            GenerateCrosshair();
        }
    }

    public void SetThickness(int newThickness, bool redrawCrosshair)
    {
        m_crosshair.thickness = newThickness;
        if (m_crosshair.thickness < 1) { m_crosshair.thickness = 3; }
        if (redrawCrosshair) GenerateCrosshair();
    }

    public void SetSize(int newSize, bool redrawCrosshair)
    {
        m_crosshair.size = newSize;
        if(m_crosshair.size < 1) { m_crosshair.size = 1; }
        if (redrawCrosshair) GenerateCrosshair();
    }

    public void SetGap(int newGap, bool redrawCrossHair)
    {
        m_crosshair.gap = newGap;
        if (m_crosshair.gap < 1) { m_crosshair.gap = 4; }
        if (redrawCrossHair) GenerateCrosshair();
    }

    private void SetDefaultCrossHairSettings()
    {
        var size = dataBank.crosshairSize;
        var thickness = dataBank.crosshairThickness;
        var gap = dataBank.crosshairGap;
        SetSize(size, true);
        SetThickness(thickness, true);
        SetGap(gap, true);
    }
    
    public void SetCrossHairSettings()
    {
        m_crosshair.size = dataBank.crosshairSize;
        crossHairSizeSlider.value = m_crosshair.size;
        crossHairSizeInput.text = m_crosshair.size.ToString();
        SetSize(m_crosshair.size, true);
        
        m_crosshair.thickness = dataBank.crosshairThickness;
        crossHairThicknessSlider.value = m_crosshair.thickness;
        crossHairThicknessInput.text = m_crosshair.thickness.ToString();
        SetThickness(m_crosshair.thickness, true);
        
        m_crosshair.gap = dataBank.crosshairGap;
        crossHairGapSlider.value = m_crosshair.gap;
        crossHairGapInput.text = m_crosshair.gap.ToString();
        SetGap(m_crosshair.gap, true);
    }

    public void SliderUpdateCrossHairSettings()
    {
        m_crosshair.size = ((int)crossHairSizeSlider.value);
        m_crosshair.thickness = ((int)crossHairThicknessSlider.value);
        m_crosshair.gap = ((int)crossHairGapSlider.value);
        crossHairSizeInput.text = m_crosshair.size.ToString();
        crossHairThicknessInput.text = m_crosshair.thickness.ToString();
        crossHairGapInput.text = m_crosshair.gap.ToString();
        SetSize(m_crosshair.size, true);
        SetThickness(m_crosshair.thickness, true);
        SetGap(m_crosshair.gap, true);
    }
    
    public void TextInputUpdateCrossHairSettings()
    {
        int.TryParse(crossHairSizeInput.text, out m_crosshair.size);
        int.TryParse(crossHairThicknessInput.text, out m_crosshair.thickness);
        int.TryParse(crossHairGapInput.text, out m_crosshair.gap);
        crossHairSizeSlider.value = m_crosshair.size;
        crossHairThicknessSlider.value = m_crosshair.thickness;
        crossHairGapSlider.value = m_crosshair.gap;
        SetSize(m_crosshair.size, true);
        SetThickness(m_crosshair.thickness, true);
        SetGap(m_crosshair.gap, true);
    }
    
    public void SaveCrossHairSettings()
    {
        dataBank.crosshairSize = m_crosshair.size;
        dataBank.crosshairThickness = m_crosshair.thickness;
        dataBank.crosshairGap = m_crosshair.gap;
    }

    #region Getters
    public int GetSize() { return m_crosshair.size; }
    public int GetThickness() { return m_crosshair.thickness; }
    public int GetGap() { return m_crosshair.gap; }
    public Color GetColor() { return m_crosshair.color; }
    public Crosshair GetCrosshair() { return m_crosshair; }
    #endregion

    #region Draw Crosshair Texture
    public Texture2D DrawCrosshair(Crosshair crosshair = null)
    {
        if(crosshair == null) { crosshair = m_crosshair; }

        int sizeNeeded = crosshair.SizeNeeded;
        int centerBias = sizeNeeded / 2;


        Texture2D crosshairTexture = new Texture2D(sizeNeeded, sizeNeeded, TextureFormat.RGBA32, false);
        crosshairTexture.wrapMode = TextureWrapMode.Clamp;
        crosshairTexture.filterMode = FilterMode.Point;

        DrawBox(0, 0, crosshairTexture.width, crosshairTexture.height, crosshairTexture, new Color(0, 0, 0, 0));

        // Top
        int startGapShort = Mathf.CeilToInt(crosshair.thickness / 2.0f);
        DrawBox(centerBias - startGapShort,
            centerBias + crosshair.gap,
            crosshair.thickness,
            crosshair.size,
            crosshairTexture,
            crosshair.color);

        // Right
        DrawBox(centerBias + crosshair.gap,
            centerBias - startGapShort,
            crosshair.size,
            crosshair.thickness,
            crosshairTexture,
            crosshair.color);


        // Bottom
        DrawBox(centerBias - startGapShort,
            centerBias - crosshair.gap - crosshair.size,
            crosshair.thickness,
            crosshair.size,
            crosshairTexture,
            crosshair.color);

        // Left
        DrawBox(centerBias - crosshair.gap - crosshair.size,
           centerBias - startGapShort,
           crosshair.size,
           crosshair.thickness,
           crosshairTexture,
           crosshair.color);

        crosshairTexture.Apply();
        return crosshairTexture;
    }

    private void DrawBox(int startX, int startY, int width, int height, Texture2D target, Color color)
    {
        if (startX + width > target.width ||
            startY + height > target.height)
        {
            Debug.LogWarning("Crosshair box is out of range.");
            return;
        }
        for (int x = startX; x < startX + width; ++x)
        {
            for (int y = startY; y < startY + height; ++y)
            {
                target.SetPixel(x, y, color);
            }
        }
    }
    #endregion
}
