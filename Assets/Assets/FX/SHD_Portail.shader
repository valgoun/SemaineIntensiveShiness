// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9754,x:32719,y:32712,varname:node_9754,prsc:2|diff-2493-RGB,emission-4747-OUT,alpha-5263-OUT;n:type:ShaderForge.SFN_Tex2d,id:2493,x:32253,y:32600,ptovrint:False,ptlb:Smoke,ptin:_Smoke,varname:node_2493,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5eb36dd93de83b043ae2cf85022693db,ntxv:0,isnm:False|UVIN-1732-OUT;n:type:ShaderForge.SFN_Tex2d,id:9736,x:31626,y:32537,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_9736,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-8320-UVOUT;n:type:ShaderForge.SFN_Panner,id:8320,x:31403,y:32537,varname:node_8320,prsc:2,spu:-0.3,spv:-0.2|UVIN-444-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:444,x:31251,y:32368,varname:node_444,prsc:2,uv:0;n:type:ShaderForge.SFN_Lerp,id:1732,x:32057,y:32560,varname:node_1732,prsc:2|A-5300-UVOUT,B-2814-OUT,T-8867-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2814,x:31816,y:32537,varname:node_2814,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-9736-RGB;n:type:ShaderForge.SFN_Slider,id:8867,x:31693,y:32725,ptovrint:False,ptlb:node_8867,ptin:_node_8867,varname:node_8867,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.219171,max:1;n:type:ShaderForge.SFN_Panner,id:5300,x:31816,y:32368,varname:node_5300,prsc:2,spu:0.7,spv:0|UVIN-8140-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8140,x:31644,y:32368,varname:node_8140,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:5263,x:32195,y:32788,varname:node_5263,prsc:2|A-6561-A,B-2493-A;n:type:ShaderForge.SFN_Tex2d,id:6561,x:32038,y:32344,ptovrint:False,ptlb:Rond,ptin:_Rond,varname:node_6561,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:4747,x:32163,y:33053,ptovrint:False,ptlb:Emissive power,ptin:_Emissivepower,varname:node_4747,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.05515805,max:1;proporder:2493-9736-8867-6561-4747;pass:END;sub:END;*/

Shader "Custom/SHD_Smoke" {
    Properties {
        _Smoke ("Smoke", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        _node_8867 ("node_8867", Range(0, 1)) = 0.219171
        _Rond ("Rond", 2D) = "white" {}
        _Emissivepower ("Emissive power", Range(0, 1)) = 0.05515805
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Smoke; uniform float4 _Smoke_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _node_8867;
            uniform sampler2D _Rond; uniform float4 _Rond_ST;
            uniform float _Emissivepower;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_2625 = _Time + _TimeEditor;
                float2 node_8320 = (i.uv0+node_2625.g*float2(-0.3,-0.2));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_8320, _Noise));
                float3 node_1732 = lerp(float3((i.uv0+node_2625.g*float2(0.7,0)),0.0),_Noise_var.rgb.rgb,_node_8867);
                float4 _Smoke_var = tex2D(_Smoke,TRANSFORM_TEX(node_1732, _Smoke));
                float3 diffuseColor = _Smoke_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = float3(_Emissivepower,_Emissivepower,_Emissivepower);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float4 _Rond_var = tex2D(_Rond,TRANSFORM_TEX(i.uv0, _Rond));
                fixed4 finalRGBA = fixed4(finalColor,(_Rond_var.a*_Smoke_var.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Smoke; uniform float4 _Smoke_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _node_8867;
            uniform sampler2D _Rond; uniform float4 _Rond_ST;
            uniform float _Emissivepower;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_5785 = _Time + _TimeEditor;
                float2 node_8320 = (i.uv0+node_5785.g*float2(-0.3,-0.2));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_8320, _Noise));
                float3 node_1732 = lerp(float3((i.uv0+node_5785.g*float2(0.7,0)),0.0),_Noise_var.rgb.rgb,_node_8867);
                float4 _Smoke_var = tex2D(_Smoke,TRANSFORM_TEX(node_1732, _Smoke));
                float3 diffuseColor = _Smoke_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float4 _Rond_var = tex2D(_Rond,TRANSFORM_TEX(i.uv0, _Rond));
                fixed4 finalRGBA = fixed4(finalColor * (_Rond_var.a*_Smoke_var.a),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
