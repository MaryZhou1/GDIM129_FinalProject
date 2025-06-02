using System.Collections;
using UnityEngine;

public class SanityEffects : MonoBehaviour
{
    [Header("Sanity Settings")]
    public int lowSanityThreshold = 3; // Threshold for low sanity effects (San ≤ 3)
    public GlobalManager globalManager; // Reference to GlobalManager for sanity value

    [Header("Blur Effect (Shader Based)")]
    public Material blurMaterial; // Material with the ScreenBlur shader
    public float maxBlurSize = 1.0f; // Maximum blur size for the shader
    public float blurFadeDuration = 1f; // Duration of blur fade-in/out

    private bool isBlurActive = false; // Track if blur is currently active

    private void Start()
    {
        // Initialize blur material
        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurSize", 0f); // Start with no blur
        }
        else
        {
            Debug.LogWarning("Blur Material not assigned! Please assign a Material with the ScreenBlur shader in the Inspector.");
        }
    }

    private void Update()
    {
        // Check sanity value from GlobalManager
        if (globalManager != null)
        {
            int currentSanity = globalManager.sanity;
            if (currentSanity <= lowSanityThreshold && !isBlurActive)
            {
                // Start blur effect when sanity is low
                if (blurMaterial != null)
                {
                    StartCoroutine(FadeBlur(true));
                }
            }
            else if (currentSanity > lowSanityThreshold && isBlurActive)
            {
                // Remove blur effect when sanity recovers
                if (blurMaterial != null)
                {
                    StartCoroutine(FadeBlur(false));
                }
            }
        }
        else
        {
            Debug.LogWarning("GlobalManager reference not set in SanityEffects! Please assign in Inspector.");
        }
    }

    // Coroutine to fade blur effect in or out
    private IEnumerator FadeBlur(bool fadeIn)
    {
        if (blurMaterial == null) yield break;

        isBlurActive = fadeIn; // Update blur state
        float startBlur = blurMaterial.GetFloat("_BlurSize");
        float targetBlur = fadeIn ? maxBlurSize : 0f;
        float elapsedTime = 0f;

        while (elapsedTime < blurFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / blurFadeDuration;
            float newBlur = Mathf.Lerp(startBlur, targetBlur, t);
            blurMaterial.SetFloat("_BlurSize", newBlur);
            yield return null;
        }

        blurMaterial.SetFloat("_BlurSize", targetBlur);
    }
}