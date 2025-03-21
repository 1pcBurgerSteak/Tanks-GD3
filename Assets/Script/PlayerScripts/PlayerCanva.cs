using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCanva : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Image targetImage;
    public TextMeshProUGUI readyText;
    public bool isReady;
    public PlayerController playerController;

    void Start()
    {
        // Initialize slider values
        isReady = false;
        readyText.text = "NOT READY";
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

            targetImage.color = new Color(red, green, blue, 1f);
        }
    }

    void OnDestroy()
    {
        redSlider.onValueChanged.RemoveListener(UpdateColor);
        greenSlider.onValueChanged.RemoveListener(UpdateColor);
        blueSlider.onValueChanged.RemoveListener(UpdateColor);
    }
    public void AssignPlayer(PlayerController controller)
    {
        playerController = controller;
    }
}
