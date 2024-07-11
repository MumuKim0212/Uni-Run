using UnityEngine;
using UnityEngine.UI;

public class UIBlurEffect : MonoBehaviour
{
    public Shader shader;
    public float blurSize = 2.0f;
    public int downsample = 2;
    public int iterations = 2;

    private Image targetImage;
    private RenderTexture rt;

    void Start()
    {
        targetImage = GetComponent<Image>();
        ApplyBlur();
    }

    void OnDisable()
    {
        if (rt != null)
        {
            RenderTexture.ReleaseTemporary(rt);
            rt = null;
        }
    }

    public void ApplyBlur()
    {
        if (targetImage == null) return;

        int width = Screen.width / downsample;
        int height = Screen.height / downsample;

        rt = RenderTexture.GetTemporary(width, height, 0);
        Graphics.Blit(targetImage.sprite.texture, rt);

        for (int i = 0; i < iterations; i++)
        {
            RenderTexture rt2 = RenderTexture.GetTemporary(width, height, 0);
            Graphics.Blit(rt, rt2, GetBlurMaterial());
            RenderTexture.ReleaseTemporary(rt);
            rt = rt2;
        }

        Texture2D blurredTexture = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        blurredTexture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        blurredTexture.Apply();

        targetImage.sprite = Sprite.Create(blurredTexture, new Rect(0, 0, blurredTexture.width, blurredTexture.height), new Vector2(0.5f, 0.5f));
    }

    Material GetBlurMaterial()
    {
        Material material = new Material(shader);
        material.SetFloat("_BlurSize", blurSize);
        return material;
    }
}