Shader "Portals/URP_PortalShader"
{
    Properties
    {
        _LeftTex("Left Texture", 2D) = "white" {}
        _RightTex("Right Texture", 2D) = "white" {}
        [Toggle] _RecursiveRender("Recursive Render", Float) = 0
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
            LOD 100

            Pass
            {
                Name "PortalPass"
                Tags { "LightMode" = "UniversalForward" }

                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                CBUFFER_START(UnityPerMaterial)
                    float _RecursiveRender;
                CBUFFER_END

                TEXTURE2D(_LeftTex);
                SAMPLER(sampler_LeftTex);

                TEXTURE2D(_RightTex);
                SAMPLER(sampler_RightTex);

                int RenderingEye;
                int OpenVRRender;

                struct Attributes
                {
                    float4 positionOS : POSITION;
                };

                struct Varyings
                {
                    float4 positionHCS : SV_POSITION;
                    float4 screenPos : TEXCOORD0;
                };

                Varyings vert(Attributes IN)
                {
                    Varyings OUT;
                    OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                    OUT.screenPos = ComputeScreenPos(OUT.positionHCS);
                    return OUT;
                }

                half4 frag(Varyings IN) : SV_Target
                {
                    float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

                    bool leftEye;
                    #ifdef UNITY_STEREO_INSTANCING_ENABLED
                        leftEye = unity_StereoEyeIndex == 0;
                    #else
                        leftEye = (unity_CameraProjection[0][2] <= 0);
                    #endif

                    if (OpenVRRender != 0)
                    {
                        leftEye = RenderingEye == 0;
                    }

                    half4 col;
                    if (leftEye || _RecursiveRender == 1)
                    {
                        col = SAMPLE_TEXTURE2D(_LeftTex, sampler_LeftTex, screenUV);
                    }
                    else
                    {
                        col = SAMPLE_TEXTURE2D(_RightTex, sampler_RightTex, screenUV);
                    }

                    return col;
                }
                ENDHLSL
            }
        }
}