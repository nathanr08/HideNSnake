Shader "Custom/NewSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
		CGPROGRAM

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		#pragma vertex vert
		#pragma fragment frag
			
		#include "UnityCG.cginc"

		struct VertIn
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
			float4 ray : TEXCOORD1;
		};

		struct VertOut
		{
			float4 vertex : SV_POSITION;
			float2 uv : TEXCOORD0;
			float2 uv_depth : TEXCOORD1;
		};

		float4 _MainTex_TexelSize;
		float4 _CameraWS;

		VertOut vert(VertIn v)
		{
			VertOut o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = v.uv.xy;
			o.uv_depth = v.uv.xy;

			#if UNITY_UV_STARTS_AT_TOP
			if (_MainTex_TexelSize.y < 0)
				o.uv.y = 1 - o.uv.y;
			#endif				

			return o;
		}
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		//void surf (Input IN, inout SurfaceOutputStandard o) {
		//	// Albedo comes from a texture tinted by color
		//	fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
		//	o.Albedo = c.rgb;
		//	// Metallic and smoothness come from slider variables
		//	o.Metallic = _Metallic;
		//	o.Smoothness = _Glossiness;
		//	o.Alpha = c.a;
		//}


		half4 frag (VertOut i) : SV_Target
			{
				half4 c = tex2D (_MainTex, i.uv) * _Color;

				return c;
			}
			ENDCG
		}
	}

	FallBack "Diffuse"
}
