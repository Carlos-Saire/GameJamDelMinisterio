Shader "Custom/PlantWind2D"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _WindStrength ("Wind Strength", Range(0, 0.5)) = 0.1
        _WindSpeed ("Wind Speed", Float) = 1.0
        _WindFrequency ("Wind Frequency", Float) = 5.0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _WindStrength;
            float _WindSpeed;
            float _WindFrequency;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;

                float time = _Time.y * _WindSpeed;
                float wave = sin((v.vertex.y + time) * _WindFrequency);
                float offset = wave * _WindStrength * v.vertex.y;

                v.vertex.x += offset;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                clip(col.a - 0.01); 
                return col;
            }
            ENDCG
        }
    }
}
