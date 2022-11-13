using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[ExecuteInEditMode]
public class PostProcessDepthGrayscale : MonoBehaviour
{
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
        // mat is the material which contains the shader
        // we are passing the destination RenderTexture to
        SaveCertificate();
    }

    public Camera _camerwWithRendTexture;

    public void SaveCertificate()
    {
        if (!_camerwWithRendTexture)
            _camerwWithRendTexture = GameObject.Find("CertificateCamera").GetComponent<Camera>();

        RenderTexture.active = _camerwWithRendTexture.targetTexture;
        Texture2D newTexture = new Texture2D(_camerwWithRendTexture.targetTexture.width, _camerwWithRendTexture.targetTexture.height, TextureFormat.RGBA32, false);
        newTexture.ReadPixels(new Rect(0, 0, _camerwWithRendTexture.targetTexture.width, _camerwWithRendTexture.targetTexture.height), 0, 0, false);

        newTexture.Apply();
        byte[] bytes = newTexture.EncodeToPNG();
        File.WriteAllBytes("depth.png", bytes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
