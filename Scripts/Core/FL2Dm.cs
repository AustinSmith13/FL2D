using UnityEngine;
using Creo.Console;

[ExecuteInEditMode]
public class FL2Dm : MonoBehaviour
{

    public LayerMask LightMapLayer;
    public LayerMask ViewLayer;
    public float intensity;
    public Color tint;
    public int quality = 1;
    public Material material;

    private Camera camera_cp;
    private bool isDirty;

    [SerializeField, HideInInspector]
    private RenderTexture source_View;
    [SerializeField, HideInInspector]
    private RenderTexture source_FL;
    [SerializeField, HideInInspector]
    private RenderTexture source_B;


    public GameObject _cfl;

    public GameObject _cv;

    public GameObject _cb;

    void Start()
    {
        Debug.Log("FL2D Initialized");
        camera_cp = GetComponent<Camera>();
        if (camera_cp == null)
            return;

        UpdateRenderTextures();

        if (!ConsoleCommandDatabase.CommandExists("FL2D_INFO"))
            ConsoleCommandDatabase.RegisterCommand("FL2D_INFO",
                "FL2D status", "[none]", cmd_fl2d_info);

        if (!ConsoleCommandDatabase.CommandExists("FL2D_RESET"))
            ConsoleCommandDatabase.RegisterCommand("FL2D_RESET",
                "FL2D reset", "[none]", cmd_fl2d_reset);
    }

    string cmd_fl2d_info(string[] args)
    {
        string output = string.Empty;
        output += "_cfl status- " + ((_cfl != null) ? ("<color=green><b>Ok!</b></color>") : ("<color=red><b>Missing!</b></color>")) + '\n';
        output += "_cv status- " + ((_cv != null) ? ("<color=green><b>Ok!</b></color>") : ("<color=red><b>Missing!</b></color>")) + '\n';
        output += "Render status- " + ((_cfl != null) ? ("<color=green><b>Ok!</b></color>") : ("<color=red><b>Failed!</b></color>")) + '\n';
        // output += "FL2D status- " + ((camera_FL != null) ? ("<color=green><b>Ok!</b></color>") : ("<color=red><b>Failed!</b></color>")) + '\n';

        return output;
    }

    string cmd_fl2d_reset(string[] args)
    {
        string output = string.Empty;

        if (source_View == null || source_FL == null)
            output += "Render texture is missing!\n";

        output += "Atempting to reset render targets...\n";
        UpdateRenderTextures();

        return output;
            
    }


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Intensity", intensity);
        material.SetColor("_Tint", tint);
        material.SetTexture("_BlendTex", source_View);
        material.SetTexture("_BackTex", source_B);
        Graphics.Blit(source_FL, destination, material);
    }

    void UpdateRenderTextures()
    {
        _cv.GetComponent<Camera>().targetTexture = null;
        _cfl.GetComponent<Camera>().targetTexture = null;
        _cb.GetComponent<Camera>().targetTexture = null;

        if (quality < 1)
            quality = 1;

        RenderTexture.DestroyImmediate(source_View);
        source_View = null;
        RenderTexture.DestroyImmediate(source_FL);
        source_FL = null;
        RenderTexture.DestroyImmediate(source_B);

        _cfl.GetComponent<Camera>().cullingMask = LightMapLayer;

        source_View = new RenderTexture(Screen.width, Screen.height, 0);

        source_View.wrapMode = TextureWrapMode.Clamp;
        //source_View.hideFlags = HideFlags.DontSave;
        source_View.isPowerOfTwo = false;
        source_View.Create();

        RenderTexture.active = source_View;
        _cv.GetComponent<Camera>().targetTexture = source_View;
        //camera_view.targetTexture = source_View;

        source_FL = new RenderTexture(Screen.width / quality, Screen.height / quality, 0);

        source_FL.wrapMode = TextureWrapMode.Clamp;
        //source_FL.hideFlags = HideFlags.DontSave;
        source_FL.isPowerOfTwo = false;
        source_FL.Create();

        RenderTexture.active = source_FL;
        _cfl.GetComponent<Camera>().targetTexture = source_FL;
        //camera_FL.targetTexture = source_FL;

        source_B = new RenderTexture(Screen.width, Screen.height, 0);

        source_B.wrapMode = TextureWrapMode.Clamp;
        //source_View.hideFlags = HideFlags.DontSave;
        source_B.isPowerOfTwo = false;
        source_B.Create();

        RenderTexture.active = source_B;
        _cb.GetComponent<Camera>().targetTexture = source_B;

    }
}
