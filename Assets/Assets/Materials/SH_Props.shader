// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:8729,x:33033,y:32710,varname:node_8729,prsc:2|diff-6647-RGB,emission-2262-OUT,amdfl-1762-OUT,clip-4234-A;n:type:ShaderForge.SFN_Tex2d,id:6647,x:32104,y:32627,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_6647,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:248ebc410e4379d4db689a3c3efeb837,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4023,x:31797,y:32751,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_4023,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:22bd82eaea1e9e04e831d31043ddb6e7,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6025,x:32270,y:32847,varname:node_6025,prsc:2|A-4023-A,B-1602-OUT;n:type:ShaderForge.SFN_Slider,id:1602,x:31797,y:32946,ptovrint:False,ptlb:Emission Power,ptin:_EmissionPower,varname:node_1602,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5535492,max:1;n:type:ShaderForge.SFN_Tex2d,id:4234,x:32026,y:33081,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_4234,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:00147c47cb3159049a3f7ad2f76756db,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:1762,x:32235,y:32987,ptovrint:False,ptlb:Diffuse ambient_power,ptin:_Diffuseambient_power,varname:node_1762,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_TexCoord,id:525,x:32012,y:32450,varname:node_525,prsc:2,uv:0;n:type:ShaderForge.SFN_Step,id:3262,x:32355,y:32469,varname:node_3262,prsc:2|A-525-V,B-1601-OUT;n:type:ShaderForge.SFN_Vector1,id:1601,x:32149,y:32531,varname:node_1601,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:509,x:32355,y:32725,varname:node_509,prsc:2|A-3262-OUT,B-4023-A;n:type:ShaderForge.SFN_Add,id:2262,x:32747,y:32806,varname:node_2262,prsc:2|A-3559-OUT,B-6025-OUT;n:type:ShaderForge.SFN_Slider,id:3424,x:32521,y:32420,ptovrint:False,ptlb:node_3424,ptin:_node_3424,varname:node_3424,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:50;n:type:ShaderForge.SFN_Multiply,id:3559,x:32848,y:32504,varname:node_3559,prsc:2|A-3424-OUT,B-9807-RGB,C-509-OUT;n:type:ShaderForge.SFN_Color,id:9807,x:32618,y:32539,ptovrint:False,ptlb:node_9807,ptin:_node_9807,varname:node_9807,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:6647-4023-1602-4234-1762-3424-9807;pass:END;sub:END;*/

Shader "Custom/SH_Props" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Emission ("Emission", 2D) = "white" {}
        _EmissionPower ("Emission Power", Range(0, 1)) = 0.5535492
        _Opacity ("Opacity", 2D) = "white" {}
        _Diffuseambient_power ("Diffuse ambient_power", Range(0, 1)) = 0
        _node_3424 ("node_3424", Range(0, 50)) = 0
        _node_9807 ("node_9807", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float _EmissionPower;
            uniform sampler2D _Opacity; uniform float4 _Opacity_ST;
            uniform float _Diffuseambient_power;
            uniform float _node_3424;
            uniform float4 _node_9807;
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
                float4 _Opacity_var = tex2D(_Opacity,TRANSFORM_TEX(i.uv0, _Opacity));
                clip(_Opacity_var.a - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                indirectDiffuse += float3(_Diffuseambient_power,_Diffuseambient_power,_Diffuseambient_power); // Diffuse Ambient Light
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffuseColor = _Diffuse_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _Emission_var = tex2D(_Emission,TRANSFORM_TEX(i.uv0, _Emission));
                float3 emissive = ((_node_3424*_node_9807.rgb*(step(i.uv0.g,0.5)*_Emission_var.a))+(_Emission_var.a*_EmissionPower));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Emission; uniform float4 _Emission_ST;
            uniform float _EmissionPower;
            uniform sampler2D _Opacity; uniform float4 _Opacity_ST;
            uniform float _node_3424;
            uniform float4 _node_9807;
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
                float4 _Opacity_var = tex2D(_Opacity,TRANSFORM_TEX(i.uv0, _Opacity));
                clip(_Opacity_var.a - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffuseColor = _Diffuse_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Opacity; uniform float4 _Opacity_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _Opacity_var = tex2D(_Opacity,TRANSFORM_TEX(i.uv0, _Opacity));
                clip(_Opacity_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
