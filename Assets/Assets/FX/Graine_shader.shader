// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1351,x:32873,y:32628,varname:node_1351,prsc:2|emission-7927-OUT;n:type:ShaderForge.SFN_Slider,id:8918,x:31620,y:33061,ptovrint:False,ptlb:Fresnel power,ptin:_Fresnelpower,varname:node_8918,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_NormalVector,id:3166,x:31715,y:32874,prsc:2,pt:False;n:type:ShaderForge.SFN_Fresnel,id:9596,x:31930,y:32913,varname:node_9596,prsc:2|NRM-3166-OUT,EXP-8918-OUT;n:type:ShaderForge.SFN_OneMinus,id:9124,x:32102,y:32913,varname:node_9124,prsc:2|IN-9596-OUT;n:type:ShaderForge.SFN_Slider,id:1586,x:31930,y:33073,ptovrint:False,ptlb:Multiply_value,ptin:_Multiply_value,varname:node_1586,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.623857,max:1;n:type:ShaderForge.SFN_Multiply,id:1521,x:32320,y:32933,varname:node_1521,prsc:2|A-9124-OUT,B-1586-OUT;n:type:ShaderForge.SFN_Color,id:8139,x:32145,y:32420,ptovrint:False,ptlb:Base color,ptin:_Basecolor,varname:node_8139,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:3502,x:32078,y:32611,ptovrint:False,ptlb:Glow_color,ptin:_Glow_color,varname:node_3502,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8676471,c2:0.7001014,c3:0.1531142,c4:1;n:type:ShaderForge.SFN_Slider,id:1624,x:32000,y:32780,ptovrint:False,ptlb:Glow_intensity,ptin:_Glow_intensity,varname:node_1624,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.912294,max:10;n:type:ShaderForge.SFN_Multiply,id:9310,x:32338,y:32623,varname:node_9310,prsc:2|A-3502-RGB,B-1624-OUT;n:type:ShaderForge.SFN_Lerp,id:7927,x:32621,y:32697,varname:node_7927,prsc:2|A-8139-RGB,B-9310-OUT,T-1521-OUT;proporder:8139-8918-1586-3502-1624;pass:END;sub:END;*/

Shader "Custom/Graine_shader" {
    Properties {
        _Basecolor ("Base color", Color) = (0.5,0.5,0.5,1)
        _Fresnelpower ("Fresnel power", Range(0, 1)) = 1
        _Multiply_value ("Multiply_value", Range(0, 1)) = 0.623857
        _Glow_color ("Glow_color", Color) = (0.8676471,0.7001014,0.1531142,1)
        _Glow_intensity ("Glow_intensity", Range(0, 10)) = 1.912294
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _Fresnelpower;
            uniform float _Multiply_value;
            uniform float4 _Basecolor;
            uniform float4 _Glow_color;
            uniform float _Glow_intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_Basecolor.rgb,(_Glow_color.rgb*_Glow_intensity),((1.0 - pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Fresnelpower))*_Multiply_value));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
