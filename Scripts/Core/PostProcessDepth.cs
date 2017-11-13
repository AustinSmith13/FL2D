using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PostProcessDepth : MonoBehaviour {

    public Material mat;
    public RenderTexture output;

    //GameObject cameraObject;
    new Camera camera;

	// Use this for initialization
	void Start () {

        output.width = Screen.width;
        output.height = Screen.height;
        camera = GetComponent<Camera>();
        camera.depthTextureMode = DepthTextureMode.Depth;
	    
	}
	
	void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
       // Graphics.SetRenderTarget(output);
    }
}
