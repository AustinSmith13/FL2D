using UnityEngine;
using Creo.Console;

namespace FL2D
{
   // [ExecuteInEditMode]
    public class FL2D : MonoBehaviour
    {

        public LayerMask LightMapLayer;
        public LayerMask ViewLayer;
        public float intensity;
        public Color tint;
        public int quality = 1;

        private Material material;
        private Camera camera_view;
        private Camera camera_FL;
        private Camera camera_cp;
        private bool isDirty;
        private Config globalState;
        private Config currentState;

        [SerializeField, HideInInspector]
        private RenderTexture source_View;
        [SerializeField, HideInInspector]
        private RenderTexture source_FL;
        [SerializeField, HideInInspector]
        private GameObject _cfl;
        [SerializeField, HideInInspector]
        private GameObject _cv;

        void Awake()
        {
            material = new Material(Shader.Find("FL2D/BlendMode"));
        }

        void Start()
        {
            Debug.Log("FL2D Initializing...");
            camera_cp = GetComponent<Camera>();
            if (camera_cp == null)
                return;

            InitCameras();
            UpdateRenderTextures();
            InitState();

            if(!ConsoleCommandDatabase.CommandExists("FL2D_INFO"))
                ConsoleCommandDatabase.RegisterCommand("FL2D_INFO",
                    "FL2D status", "[none]", cmd_fl2d_info);
        }

        string cmd_fl2d_info(string[] args)
        {
            string output = string.Empty;
            output += "_cfl status- " + ((_cfl != null) ? ("<color=green><b>Ok!</b></color>") : ("<color=red><b>Missing!</b></color>")) + '\n';
            output += "_cv status- " + ((_cv != null) ? ("<color=green><b>Ok!</b></color>") : ("<color=red><b>Missing!</b></color>")) + '\n';
            output += "Render status- " + ((_cfl != null) ? ("<color=green><b>Ok!</b></color>") : ("<color=red><b>Failed!</b></color>")) + '\n';
            output += "FL2D status- " + ((camera_FL != null) ? ("<color=green><b>Ok!</b></color>") : ("<color=red><b>Failed!</b></color>")) + '\n';

            return output;
        }

        void Update()
        {
            if(isDirty)
            {

            }
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            material.SetFloat("_Intensity", currentState.intensity);
            material.SetColor("_Tint", currentState.tint);
            material.SetTexture("_BlendTex", source_View);
            Graphics.Blit(source_FL, destination, material);
        }

        void OnDestroy()
        {
            //GameObject.DestroyImmediate(_cfl);
            //GameObject.DestroyImmediate(_cv);
        }

        void InitState()
        {
            globalState.intensity = this.intensity;
            globalState.tint = this.tint;
            globalState.ambientLight = _cfl.GetComponent<Camera>().backgroundColor;
            currentState = globalState;
        }

        void InitCameras()
        {
            if (_cfl == null)
            {
                _cfl = new GameObject();
                _cfl.name = "FLC_FL";
                _cfl.transform.SetParent(camera_cp.transform);
                camera_FL = _cfl.AddComponent<Camera>();
                camera_FL.CopyFrom(camera_cp);
                camera_FL.cullingMask = LightMapLayer;
                camera_FL.clearFlags = CameraClearFlags.Color;
                camera_FL.backgroundColor = Color.black;
            }

            if (_cv == null)
            {
                _cv = new GameObject();
                _cv.name = "FLC_View";
                _cv.transform.SetParent(camera_cp.transform);
                camera_view = _cv.AddComponent<Camera>();
                camera_view.CopyFrom(camera_cp);
                camera_view.cullingMask = ViewLayer;
                camera_view.clearFlags = CameraClearFlags.Color;
                camera_view.backgroundColor = Color.black;
            }

            // Make the original camera a dummy
            if (_cv != null && _cfl != null)
                GetComponent<Camera>().cullingMask = 0x00;
        }

        void UpdateRenderTextures()
        {
            if (quality < 1)
                quality = 1;

            RenderTexture.DestroyImmediate(source_View);
            source_View = null;
            RenderTexture.DestroyImmediate(source_FL);
            source_FL = null;

            _cfl.GetComponent<Camera>().cullingMask = LightMapLayer;

            source_View = new RenderTexture(Screen.width, Screen.height, 0);

            source_View.wrapMode = TextureWrapMode.Clamp;
            source_View.hideFlags = HideFlags.DontSave;
            source_View.isPowerOfTwo = false;
            source_View.Create();

            RenderTexture.active = source_View;
            _cv.GetComponent<Camera>().targetTexture = source_View;

            source_FL = new RenderTexture(Screen.width / quality, Screen.height / quality, 0);

            source_FL.wrapMode = TextureWrapMode.Clamp;
            source_FL.hideFlags = HideFlags.DontSave;
            source_FL.isPowerOfTwo = false;
            source_FL.Create();

            RenderTexture.active = source_FL;
            _cfl.GetComponent<Camera>().targetTexture = source_FL;
        }
    }
}