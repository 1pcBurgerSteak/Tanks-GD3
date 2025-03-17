using UnityEngine;
using UnityEngine.UI;

public class PlayerCanva : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Image targetImage;

    void Start()
    {
        // Initialize slider values
        redSlider.onValueChanged.AddListener(UpdateColor);
        greenSlider.onValueChanged.AddListener(UpdateColor);
        blueSlider.onValueChanged.AddListener(UpdateColor);

        UpdateColor(0); // Apply the initial color
    }

    void UpdateColor(float value)
    {
        if (targetImage != null)
        {
            float red = redSlider.value / 255f;
            float green = greenSlider.value / 255f;
            float blue = blueSlider.value / 255f;

            targetImage.color = new Color(red, green, blue, 1f); // Set full alpha
        }
    }

    void OnDestroy()
    {
        // Remove listeners when the script is destroyed
        redSlider.onValueChanged.RemoveListener(UpdateColor);
        greenSlider.onValueChanged.RemoveListener(UpdateColor);
        blueSlider.onValueChanged.RemoveListener(UpdateColor);
    }
}
