Shader "Custom/DissolveSprite"
{
    Properties
    {
        _MainTex("Sprite", 2D) = "white" {}
        _DissolveAmount("Dissolve Amount", Range(0, 1)) = 0
        _DissolveTex("Dissolve Texture", 2D) = "white" {}
        _EdgeColor("Edge Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            sampler2D _DissolveTex;
            float _DissolveAmount;
            float4 _EdgeColor;

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
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                float d = tex2D(_DissolveTex, i.uv).r;

                if (d < _DissolveAmount)
                {
                    discard;
                }
                else if (d < _DissolveAmount + 0.05)
                {
                    col.rgb = _EdgeColor.rgb;
                }

                return col;
            }
            ENDCG
        }
    }
}
