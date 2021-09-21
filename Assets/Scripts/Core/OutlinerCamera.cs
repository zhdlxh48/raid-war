using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlinerCamera : MonoBehaviour
{
    public Transform boss;
    public Transform player;

    // Shader
    Camera attachCam;
    public Shader glow_outline;
    public Shader DrawSimple;
    Camera TempCam;
    Material postMat;
    RenderTexture tempTex;
    // Start is called before the first frame update
    void Awake()
    {
        tempTex = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.Default);
        attachCam = GetComponent<Camera>();
        TempCam = new GameObject().AddComponent<Camera>();
        TempCam.enabled = false;

        glow_outline = Shader.Find("Custom/GlowOutline");
        DrawSimple = Shader.Find("Standard");
    }

    private void Start()
    {
        postMat = new Material(glow_outline);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        TempCam.CopyFrom(attachCam);
        TempCam.clearFlags = CameraClearFlags.Color;
        TempCam.backgroundColor = Color.black;

        TempCam.cullingMask = 1 << LayerMask.NameToLayer("Outline");

        //put it to video memory
        tempTex.Create();

        //set the camera's target texture when rendering
        TempCam.targetTexture = tempTex;

        //render all objects this camera can render, but with our custom shader.
        postMat.SetTexture("_SceneTex", source);

        TempCam.RenderWithShader(DrawSimple, "");

        if (boss.position.z - 0.5f < player.position.z)
        {
            if ((boss.position - player.position).sqrMagnitude < 4.0f * 4.0f)
            {
                Graphics.Blit(tempTex, destination, postMat);
            }
            else
                Graphics.Blit(source, destination);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
        //release the temporary RT
        tempTex.Release();

    }
}
