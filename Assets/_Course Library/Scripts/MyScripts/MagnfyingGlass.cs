using UnityEngine;

public class MagnfyingGlass : MonoBehaviour
{
    [SerializeField] private Camera magnifyCamera; //assing the child camera
    [SerializeField] private Renderer glassRenderer; //Assign the magnifying glass surace
    [SerializeField] private float magnifyZoom = 2f; // Adjust magnification level
    [SerializeField] private RenderTexture magnifyTexture;// render texture for the magnified view

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (magnifyCamera != null)
        {
            magnifyCamera.orthographic = true;
            magnifyCamera.orthographicSize /= magnifyZoom; //magnify the view

            //Assign the render texture to the camera
            if (magnifyTexture != null)
            {
                magnifyCamera.targetTexture = magnifyTexture;
                //Assign the texture to the glass material
                if (glassRenderer != null)
                {
                    glassRenderer.material.mainTexture = magnifyTexture;
                }
            }
            else
            {
                Debug.LogError("Render Texture not assinged.");

            }
        }
        else
        {
        Debug.LogError("Magnify Camera not assinged.");    
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Optional: Rotate the mafnify camera to align with the glass movement
        if (magnifyCamera != null)
        {
            magnifyCamera.transform.rotation = transform.rotation;
        }
        
    }
}
