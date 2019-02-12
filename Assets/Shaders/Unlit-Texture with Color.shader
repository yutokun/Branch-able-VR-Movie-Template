// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit shader. Simplest possible textured shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Unlit/Texture with Color" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Color ("Color", Color) = (1, 1, 1, 1)
    _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.3
    _GlitchThreshold ("Glitch Threshold", Range(0, 1)) = 0.5
    _GlitchOffset ("Glitch Offset", Range(0, 1)) = 0.2
}

SubShader {
    Tags { "Queue"="Transparent" "RenderType"="Fade" }
    LOD 100
    
    Blend SrcAlpha OneMinusSrcAlpha
    
    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed _NoiseIntensity;
            fixed _GlitchThreshold;
            fixed _GlitchOffset;
            
            float rand(fixed2 co){
                return frac(sin(dot(co.xy ,fixed2(12.9898,78.233))) * 43758.5453);
            }

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Glitch
                fixed3 random = rand(_Time);
                fixed2 uv = i.texcoord;
                uv.x += frac(random).x < _GlitchThreshold ? _GlitchOffset : 0;
                
                float3 col = tex2D(_MainTex, uv).rgb;
                
                // Noisy Signal
                uv = i.texcoord * _SinTime;
                float r = rand(uv);
                fixed3 noise = float3(r, r, r);
                col = lerp(col, noise, _NoiseIntensity);
                
                // Old CRT Monitor Effect
                col -= abs(sin(i.texcoord.y * 100.0 + _Time * 5.0)) * 0.08;
                col -= abs(sin(i.texcoord.y * 300.0 + _Time * 10.0)) * 0.05;
                
                return fixed4(col, 1.0).rgba;
            }
        ENDCG
    }
}

}
