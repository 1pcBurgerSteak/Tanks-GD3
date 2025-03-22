using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    float speed = 0f;
    public float destroyZThreshold = -250;

    void Start()
    {
        speed = Random.Range(5f, 15f);
        RandomizeOpacity();
    }

    void Update()
    {
        // Move the cloud along the Z-axis
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);

        // Destroy the cloud if it reaches the threshold
        if (transform.position.z <= destroyZThreshold)
        {
            Destroy(gameObject);
        }
    }

    void RandomizeOpacity()
    {
        // Get the Renderer component
        Renderer cloudRenderer = GetComponent<Renderer>();

        if (cloudRenderer != null && cloudRenderer.material.HasProperty("_Color"))
        {
            // Get the current color of the material
            Color currentColor = cloudRenderer.material.color;

            // Randomize the alpha value between 0.3 and 1 (adjust range as needed)
            currentColor.a = Random.Range(0.3f, 1f);

            // Apply the new color to the material
            cloudRenderer.material.color = currentColor;
        }
        else
        {
            Debug.LogWarning("Material doesn't have a _Color property or Renderer is missing!");
        }
    }
}
