Shader "Custom/VignetteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Resolution ("Resolution", Vector) = (1920,1080,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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

            sampler2D _MainTex;
            float2 _Resolution;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 applyVignette(float4 color, float2 uv)
            {
                float2 position = (uv * _Resolution.xy) - _Resolution.xy * 0.5;           
                float dist = length(position);

                float radius = 0.4 * _Resolution.y; // Circle radius

                if(dist > radius) 
                {
                    color = float4(0, 0, 0, 1); // Black outside the circle
                }
                else
                {
                    color.a = 0; // Fully transparent inside the circle
                }

                return color;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv);
                color = applyVignette(color, i.uv);
                return color;
            }
            ENDCG
        }
    }
}
