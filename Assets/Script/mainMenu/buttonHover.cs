using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class buttonHover : MonoBehaviour
{
    public TMP_Text buttonText;  // Assign in Inspector (Text inside the button)
    private string originalText;
    private string spacedText;
    private Coroutine animationCoroutine;

    private void Start()
    {
        originalText = buttonText.text; // Store the original text
    }

    public void OnHoverEnter()
    {
        if (animationCoroutine != null) StopCoroutine(animationCoroutine);
        animationCoroutine = StartCoroutine(AnimateTextForward());
    }

    public void OnHoverExit()
    {
        if (animationCoroutine != null) StopCoroutine(animationCoroutine);
        animationCoroutine = StartCoroutine(AnimateTextBackward());
    }

    private IEnumerator AnimateTextForward()
    {
        spacedText = "";

        for (int i = 0; i < originalText.Length; i++)
        {
            spacedText += originalText[i] + "  "; // Add spaces
            buttonText.text = spacedText.TrimEnd();
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator AnimateTextBackward()
    {
        while (spacedText.Contains("  "))
        {
            spacedText = spacedText.Remove(spacedText.LastIndexOf("  "), 2); // Remove last set of spaces
            buttonText.text = spacedText;
            yield return new WaitForSeconds(0.05f);
        }

        buttonText.text = originalText; // Restore original text
    }
}
