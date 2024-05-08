using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LowResolutionEffect : MonoBehaviour
{
    public int scaleFactor = 4;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int width = source.width / scaleFactor;
        int height = source.height / scaleFactor;
        RenderTexture scaled = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(source, scaled);
        Graphics.Blit(scaled, destination);
        RenderTexture.ReleaseTemporary(scaled);
    }
}