Shader "FL2D/BlendMode" {
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_BlendTex("Blend Texture", 2D) = "white" {}
		_BackTex("Background Texture", 2D) = "black" {}
		_Intensity("Intensity", Range(0,1)) = 1
		_Tint("Tint", Color) = (1,1,1,1)
	}

	Subshader
	{

	Pass
	{
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform sampler2D _BlendTex;
		uniform sampler2D _BackTex;
		fixed4 _Tint;
		fixed _Intensity;



		fixed4 frag(v2f_img i) : COLOR
		{
			fixed4 renderTex = tex2D(_MainTex, i.uv);
			fixed4 blendTex = tex2D(_BlendTex, i.uv);
			fixed4 backtex = tex2D(_BackTex, i.uv);


			fixed4 c = ((1 - blendTex.a) * backtex) + blendTex.a * blendTex * renderTex* _Intensity * _Tint;
			//fixed4 blendedImage = renderTex;

			//blendedImage.r = OverlayBlendMode(renderTex.r, blendTex.r);
			//blendedImage.g = OverlayBlendMode(renderTex.g, blendTex.g);
			//blendedImage.b = OverlayBlendMode(renderTex.b, blendTex.b);

			//renderTex = lerp(renderTex, blendedImage, _Opacity);
			//fixed4 finalColor = renderTex * blendTex * _Intensity * _Tint;
			//finalColor += renderTex*0.1;

			return c;
		}


		ENDCG
	}
	}
}