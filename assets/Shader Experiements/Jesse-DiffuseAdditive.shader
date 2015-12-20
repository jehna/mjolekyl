Shader "Jesse/DiffuseAdditive" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_HaloColor ("Halo Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Halo1Tex ("Halo 1", 2D) = "white" {}
	_Halo2Tex ("Halo 2", 2D) = "white" {}
	_Halo3Tex ("Halo 3", 2D) = "white" {}
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 200

CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
fixed4 _Color;
fixed4 _HaloColor;
sampler2D _Halo1Tex;
sampler2D _Halo2Tex;
sampler2D _Halo3Tex;


struct Input {
	float2 uv_MainTex;
	float2 uv_Halo1Tex;
	float2 uv_Halo2Tex;
	float2 uv_Halo3Tex;
	float4 screenPos;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 halo = tex2D(_Halo1Tex, IN.uv_Halo1Tex) * tex2D(_Halo2Tex, IN.uv_Halo2Tex) * tex2D(_Halo3Tex, IN.uv_Halo3Tex);
	halo = lerp(halo, 1.0f, _HaloColor);
	fixed4 diff = _Color * tex2D(_MainTex, IN.uv_MainTex);
	
	fixed4 c = halo / diff;
	o.Albedo = c.rgb;
	
	
}
ENDCG
}

Fallback "VertexLit"
}
