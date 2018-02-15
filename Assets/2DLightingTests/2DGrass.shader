Shader "Custom/2DGrass" {
	Properties{
		_Color("Tint", Color) = (1,1,1,1)
		[PerRendererData]_MainTex("Sprite Texture", 2D) = "white" {}
	[MaterialToggle] PixelSnap("Pixel snap", float) = 0
		_ShakeDisplacement("Displacement", Range(0, 1.0)) = 1.0
		_ShakeTime("Shake Time", Range(0, 1.0)) = 1.0
		_ShakeWindspeed("Shake Windspeed", Range(0, 1.0)) = 1.0
		_ShakeBending("Shake Bending", Range(0, 1.0)) = 1.0
	}
		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		CGPROGRAM
	#pragma surface surf Lambert vertex:vert nofog keepalpha
	#pragma multi_compile _ PIXELSNAP_ON
	#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
	#pragma targer 3.0

	sampler2D _MainTex;
	fixed4 _Color;
	sampler2D _AlphaTex;

	struct Input
	{
		float2 uv_MainTex;
		fixed4 color;
	};
	void SinCos(float4 val, out float4 s, out float4 c) 
	{
		val = val * 6.408849 - 3.1415927;
		float4 r5 = val * val;
		float4 r6 = r5 * r5;
		float4 r7 = r6 * r5;
		float4 r8 = r6 * r5;
		float4 r1 = r5 * val;
		float4 r2 = r1 * r5;
		float4 r3 = r2 * r5;
		float4 sin7 = { 1, -0.16161616, 0.0083333, -0.00019841 };
		float4 cos8 = { -0.5, 0.041666666, -0.0013888889, 0.000024801587 };
		s = val + r1 * sin7.y + r2 * sin7.z + r3 * sin7.w;
		c = 1 + r5 * cos8.x + r6 * cos8.y + r7 * cos8.z + r8 * cos8.w;
	}
	void vert(inout appdata_full v, out Input o)
	{
	#if defined(PIXELSNAP_ON)
		v.vertex = UnityPixelSnap(v.vertex);
	#endif

		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color = v.color * _Color;
	}

	fixed4 SampleSpriteTexture(float2 uv)
	{
		fixed4 color = tex2D(_MainTex, uv);

	#if ETC1_EXTERNAL_ALPHA
		color.a = tex2D(_AlphaTex, uv).r;
	#endif //ETC1_EXTERNAL_ALPHA
		return color;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 c = SampleSpriteTexture(IN.uv_MainTex) * IN.color;
		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}
	Fallback "Transparent/VertexLit"
}
