using UnityEngine;
using System.Collections;

public class FLight : MonoBehaviour {

    public float intensity;
    public Sprite sprite;
    public Color color;
    bool showInEditor;

    void Start()
    {
        InitLight();
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 1);
        Gizmos.DrawIcon(transform.position, "FL_icon.png", true);
    }

    private void InitLight()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if(spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = this.sprite;
        }

        Material mat = new Material(Shader.Find("FL2D/Additive"));
        mat.SetFloat("_Intensity", intensity);
        sprite = spriteRenderer.sprite;
        spriteRenderer.color = this.color;
        spriteRenderer.material = mat;
    }
	
}
