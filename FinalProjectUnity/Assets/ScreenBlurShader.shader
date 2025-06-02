Shader "Custom/ScreenBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" "IgnoreProjector"="True" }
        GrabPass { "_GrabTexture" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 grabUV : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _GrabTexture;
            float _BlurSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.grabUV = ComputeGrabScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.grabUV.xy / i.grabUV.w;
                fixed4 col = 0;
                float blur = _BlurSize / 1000.0; // Scale down blur size for finer control
                int kernelSize = 5; // 5x5 kernel for blur
                int halfKernel = kernelSize / 2;
                float weightSum = 0;

                // Gaussian blur with 5x5 kernel
                for (int x = -halfKernel; x <= halfKernel; x++)
                {
                    for (int y = -halfKernel; y <= halfKernel; y++)
                    {
                        float2 offset = float2(x, y) * blur;
                        float weight = exp(-(x * x + y * y) / (2.0 * 2.0)); // Gaussian weight
                        col += tex2D(_GrabTexture, uv + offset) * weight;
                        weightSum += weight;
                    }
                }
                col /= weightSum; // Normalize by total weight
                return col;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}