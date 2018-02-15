// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/2DGrass"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
	[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
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
#pragma surface surf Lambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
#include "UnitySprites.cginc"

	struct Input
	{
		float2 uv_MainTex;
		fixed4 color;
	};
	void FastSinCos(float4 val, out float4 s, out float4 c) {
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
		v.vertex.xy *= _Flip.xy;

#if defined(PIXELSNAP_ON)
		v.vertex = UnityPixelSnap(v.vertex);
#endif
		float4 _waveXmove = float4(0.024, 0.04, -0.12, 0.096);
		float4 _waveZmove = float4 (0.006, .02, -0.02, 0.1);
		float4 waves;
		waves = v.vertex.x * 1;
		waves += v.vertex.z * 0;

		waves += _Time.x * (1 - 0.2 * 2 - v.color.b) * 1* 1;

		float4 s, c;
		waves = frac(waves);
		FastSinCos(waves, s, c);

		float waveAmount = v.texcoord.y * (v.color.a + 10);
		s *= 1.1;

		s *= normalize(1.1);

		s = s * s;
		float fade = dot(s, 1.3);
		s = s * s;
		float3 waveMove = float3 (0, 0, 0);
		waveMove.x = dot(s + 5, _waveXmove);
		waveMove.z = dot(s, _waveZmove);
		v.vertex.xz -= mul((float3x3)unity_WorldToObject, waveMove).xz;
		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color = v.color * _Color * _RendererColor;
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
