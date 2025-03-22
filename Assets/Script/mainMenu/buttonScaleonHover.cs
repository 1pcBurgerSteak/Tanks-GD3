using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScaleonHover : MonoBehaviour
{
    public Vector3 scaleIncrease = new Vector3(1.2f, 1.4f, 1.2f);
    private Vector3 originalScale;


    private void Start()
    {
        originalScale = transform.localScale;
    }
    public void OnPointerEnter()
    {
        transform.localScale = scaleIncrease;
    }

    public void OnPointerExit()
    {
        transform.localScale = originalScale;
    }

    public void SceneSelect(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
