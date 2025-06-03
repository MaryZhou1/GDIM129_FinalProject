using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SanityEffects : MonoBehaviour
{
    [Header("Sanity Settings")]
    public int lowSanityThreshold = 3; // Threshold for low sanity effects (San ≤ 3)
    public GlobalManager globalManager; // Reference to GlobalManager for sanity value

    [Header("Blur Effect (Shader Based)")]
    public Material blurMaterial; // Material with the ScreenBlur shader
    public float maxBlurSize = 1.0f; // Maximum blur size for the shader
    public float blurFadeDuration = 1f; // Duration of blur fade-in/out

    [Header("Blood Splatter Effect")]
    public Image bloodSplatterImage; // UI Image for blood splatter overlay
    public Sprite[] bloodSplatters; // Array of blood splatter sprites
    public float splatterFadeDuration = 1f; // Duration of fade-in/out
    public float splatterDisplayTime = 5f; // How long the splatter stays visible (5 seconds)
    private bool isSplatterActive = false; // Track if splatter is currently active
    private int previousSanity; // Track previous sanity value to detect decreases

    private bool isBlurActive = false; // Track if blur is currently active

    private void Start()
    {
        // Initialize blur material
        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurSize", 0f);
        }
        else
        {
            Debug.LogWarning("Blur Material not assigned! Please assign a Material with the ScreenBlur shader in the Inspector.");
        }

        // Initialize blood splatter image
        if (bloodSplatterImage != null)
        {
            bloodSplatterImage.color = new Color(1f, 1f, 1f, 0f);
            if (globalManager != null)
            {
                previousSanity = globalManager.sanity;
                Debug.Log($"Initial Sanity: {previousSanity}");
            }
            else
            {
                Debug.LogWarning("GlobalManager not assigned, cannot initialize previousSanity!");
            }
        }
        else
        {
            Debug.LogWarning("Blood Splatter Image not assigned! Please assign a UI Image in the Inspector.");
        }
    }

    private void Update()
    {
        if (globalManager == null)
        {
            Debug.LogWarning("GlobalManager reference not set in SanityEffects! Please assign in Inspector.");
            return;
        }

        int currentSanity = globalManager.sanity;

        // Check for sanity decrease to trigger blood splatter
        if (currentSanity < previousSanity && !isSplatterActive)
        {
            Debug.Log("Sanity decreased, triggering blood splatter.");
            if (bloodSplatterImage != null && bloodSplatters.Length > 0)
            {
                StartCoroutine(ShowBloodSplatter());
            }
            else
            {
                Debug.LogWarning("Cannot show blood splatter: Image or sprites not assigned!");
            }
        }
        previousSanity = currentSanity;

        // Handle blur effect
        if (currentSanity <= lowSanityThreshold && !isBlurActive)
        {
            Debug.Log("Sanity below threshold, triggering blur.");
            if (blurMaterial != null)
            {
                StartCoroutine(FadeBlur(true));
            }
        }
        else if (currentSanity > lowSanityThreshold && isBlurActive)
        {
            Debug.Log("Sanity above threshold, removing blur.");
            if (blurMaterial != null)
            {
                StartCoroutine(FadeBlur(false));
            }
        }
    }

    // Coroutine to fade blur effect in or out
    private IEnumerator FadeBlur(bool fadeIn)
    {
        if (blurMaterial == null) yield break;

        isBlurActive = fadeIn;
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

    // Coroutine to show and fade blood splatter
    private IEnumerator ShowBloodSplatter()
    {
        if (bloodSplatterImage == null || bloodSplatters.Length == 0) yield break;

        isSplatterActive = true;

        // Randomly select a blood splatter sprite
        int randomIndex = Random.Range(0, bloodSplatters.Length);
        bloodSplatterImage.sprite = bloodSplatters[randomIndex];

        // Fade in
        float elapsedTime = 0f;
        Color startColor = bloodSplatterImage.color;
        Color targetColor = new Color(1f, 1f, 1f, 0.7f); // Semi-transparent for visibility

        while (elapsedTime < splatterFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / splatterFadeDuration;
            bloodSplatterImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        bloodSplatterImage.color = targetColor;

        // Hold the splatter for a duration
        yield return new WaitForSeconds(splatterDisplayTime);

        // Fade out
        elapsedTime = 0f;
        startColor = bloodSplatterImage.color;
        targetColor = new Color(1f, 1f, 1f, 0f); // Fully transparent

        while (elapsedTime < splatterFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / splatterFadeDuration;
            bloodSplatterImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        bloodSplatterImage.color = targetColor;
        isSplatterActive = false;
    }
}