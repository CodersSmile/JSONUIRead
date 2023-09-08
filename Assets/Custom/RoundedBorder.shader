Shader"Custom/RoundedBorder"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BorderColor("Border Color", Color) = (1,1,1,1)
        _BorderWidth("Border Width", Range(0, 0.1)) = 0.01
        _CornerRadius("Corner Radius", Range(0, 1)) = 0.1
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

float4 _MainTex_ST;
float4 _BorderColor;
float _BorderWidth;
float _CornerRadius;
sampler2D _MainTex;

v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    float2 center = float2(0.5, 0.5);
    float2 border = _BorderWidth / _MainTex_ST.zw;

                // Calculate UV with rounded corners
    float2 cornerPos = i.uv - center;
    float cornerRadius = _CornerRadius;
    float dist = length(cornerPos);

    float4 borderCol = _BorderColor;
    borderCol.a = borderCol.a * smoothstep(0, cornerRadius, dist - border.x);

                // Check if the pixel is inside the border area
    if (dist > cornerRadius - border.x && dist < cornerRadius + border.x)
    {
        return borderCol;
    }

    return tex2D(_MainTex, i.uv);
}
            ENDCG
        }
    }
}
