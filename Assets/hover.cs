using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class hover : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color normalColor;
    public Color hoverColor = Color.green; 
    private Vector3 normalScale;
    public float scaleMultiplier = 1.2f; 

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        normalColor = objectRenderer.material.color;

        normalScale = transform.localScale;
    }

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject) 
            {
                OnMouseOver();
            }
            else
            {
                OnMouseExit(); 
            }
        }
    }

    void OnMouseOver()
    {
        objectRenderer.material.color = hoverColor;

        transform.localScale = normalScale * scaleMultiplier;
    }

    void OnMouseExit()
    {
        objectRenderer.material.color = normalColor;

        transform.localScale = normalScale;
    }
}
