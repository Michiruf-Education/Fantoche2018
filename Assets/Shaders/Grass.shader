// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1320,x:33222,y:32620,varname:node_1320,prsc:2|emission-7372-RGB,clip-7372-A,voffset-6381-OUT;n:type:ShaderForge.SFN_Tex2d,id:7372,x:32105,y:32581,ptovrint:False,ptlb:mainTEx,ptin:_mainTEx,varname:node_7372,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:2516,x:32474,y:32746,varname:node_2516,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6381,x:32832,y:32771,varname:node_6381,prsc:2|A-2516-R,B-9710-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4348,x:31793,y:33197,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_4348,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Time,id:7877,x:31563,y:33539,varname:node_7877,prsc:2;n:type:ShaderForge.SFN_Cos,id:3,x:31759,y:33539,varname:node_3,prsc:2|IN-7877-T;n:type:ShaderForge.SFN_TexCoord,id:2710,x:31563,y:33342,varname:node_2710,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_OneMinus,id:9616,x:31805,y:33342,varname:node_9616,prsc:2|IN-2710-V;n:type:ShaderForge.SFN_Multiply,id:120,x:32084,y:33443,varname:node_120,prsc:2|A-9616-OUT,B-3-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:7686,x:31836,y:32832,varname:node_7686,prsc:2;n:type:ShaderForge.SFN_Sin,id:4675,x:32029,y:32871,varname:node_4675,prsc:2|IN-7686-X;n:type:ShaderForge.SFN_Multiply,id:6613,x:32205,y:32925,varname:node_6613,prsc:2|A-4675-OUT,B-4886-OUT;n:type:ShaderForge.SFN_Multiply,id:4886,x:32135,y:33162,varname:node_4886,prsc:2|A-4348-OUT,B-120-OUT;n:type:ShaderForge.SFN_Vector1,id:4011,x:32370,y:33042,varname:node_4011,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:9710,x:32564,y:32966,varname:node_9710,prsc:2|A-6613-OUT,B-4011-OUT,C-6613-OUT;proporder:7372-4348;pass:END;sub:END;*/

Shader "Unlit/Grass" {
    Properties {
        _mainTEx ("mainTEx", 2D) = "white" {}
        _Intensity ("Intensity", Float ) = 3
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 100
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles metal 
            #pragma target 3.0
            uniform sampler2D _mainTEx; uniform float4 _mainTEx_ST;
            uniform float _Intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                float4 node_7877 = _Time;
                float node_6613 = (sin(mul(unity_ObjectToWorld, v.vertex).r)*(_Intensity*((1.0 - o.uv0.g)*cos(node_7877.g))));
                v.vertex.xyz += (o.vertexColor.r*float3(node_6613,0.0,node_6613));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _mainTEx_var = tex2D(_mainTEx,TRANSFORM_TEX(i.uv0, _mainTEx));
                clip(_mainTEx_var.a - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _mainTEx_var.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles metal 
            #pragma target 3.0
            uniform sampler2D _mainTEx; uniform float4 _mainTEx_ST;
            uniform float _Intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                float4 node_7877 = _Time;
                float node_6613 = (sin(mul(unity_ObjectToWorld, v.vertex).r)*(_Intensity*((1.0 - o.uv0.g)*cos(node_7877.g))));
                v.vertex.xyz += (o.vertexColor.r*float3(node_6613,0.0,node_6613));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 _mainTEx_var = tex2D(_mainTEx,TRANSFORM_TEX(i.uv0, _mainTEx));
                clip(_mainTEx_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
