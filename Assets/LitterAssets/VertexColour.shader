Shader "Custom/VertexColorURP" {
    Properties {
        _BaseColor("Base Color", Color) = (1,1,1,1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 200

        Pass {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardUtils.cginc"
            #include "UnityShaderUtilities.cginc"

            struct Attributes {
                float4 positionOS : POSITION;
                float4 color : COLOR;
            };

            struct Varyings {
                float4 positionHCS : SV_POSITION;
                float4 color : COLOR;
            };

            Varyings vert(Attributes IN) {
                Varyings OUT;
                OUT.positionHCS = UnityObjectToClipPos(IN.positionOS);
                OUT.color = IN.color;
                return OUT;
            }

            float4 _BaseColor;

            float4 frag(Varyings IN) : SV_Target {
                float3 albedo = IN.color.rgb * _BaseColor.rgb;
                return float4(albedo, 1.0);
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}