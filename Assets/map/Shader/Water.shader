// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:1,cusa:False,bamd:0,cgin:,cpap:True,lico:0,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:14,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:True,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32781,y:32798,varname:node_4013,prsc:2|emission-4767-OUT,voffset-6315-OUT;n:type:ShaderForge.SFN_Time,id:1490,x:31864,y:32846,varname:node_1490,prsc:2;n:type:ShaderForge.SFN_Append,id:9682,x:31861,y:32713,varname:node_9682,prsc:2|A-5804-OUT,B-5230-OUT;n:type:ShaderForge.SFN_Multiply,id:1576,x:32019,y:32713,varname:node_1576,prsc:2|A-9682-OUT,B-1490-T;n:type:ShaderForge.SFN_Add,id:680,x:32172,y:32713,varname:node_680,prsc:2|A-1576-OUT,B-9548-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9548,x:32022,y:32836,varname:node_9548,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_Tex2d,id:768,x:32356,y:32713,ptovrint:False,ptlb:Albedo1,ptin:_Albedo1,varname:_Albedo1,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False|UVIN-680-OUT;n:type:ShaderForge.SFN_Add,id:4767,x:32616,y:32897,varname:node_4767,prsc:2|A-768-RGB,B-5660-RGB,C-5301-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5301,x:32356,y:33056,ptovrint:False,ptlb:Intensive Color,ptin:_IntensiveColor,varname:_IntensiveColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-1;n:type:ShaderForge.SFN_Color,id:5660,x:32356,y:32893,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Slider,id:5804,x:31543,y:32714,ptovrint:False,ptlb:U_Speed1,ptin:_U_Speed1,varname:node_5804,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:5230,x:31543,y:32792,ptovrint:False,ptlb:V_Speed1,ptin:_V_Speed1,varname:node_5230,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:4160,x:32277,y:33298,ptovrint:False,ptlb:Slider_Vertex,ptin:_Slider_Vertex,varname:node_4160,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:6315,x:32615,y:33158,varname:node_6315,prsc:2|A-5738-RGB,B-4160-OUT;n:type:ShaderForge.SFN_Tex2d,id:5738,x:32356,y:33125,ptovrint:False,ptlb:Vertex,ptin:_Vertex,varname:node_5738,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False|UVIN-680-OUT;proporder:768-5660-5301-5804-5230-5738-4160;pass:END;sub:END;*/

Shader "EgorShader/Water" {
    Properties {
        _Albedo1 ("Albedo1", 2D) = "black" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _IntensiveColor ("Intensive Color", Float ) = -1
        _U_Speed1 ("U_Speed1", Range(-1, 1)) = 0
        _V_Speed1 ("V_Speed1", Range(-1, 1)) = 0
        _Vertex ("Vertex", 2D) = "black" {}
        _Slider_Vertex ("Slider_Vertex", Range(-1, 1)) = 0
        _Stencil ("Stencil ID", Float) = 0
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilComp ("Stencil Comparison", Float) = 8
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilOpFail ("Stencil Fail Operation", Float) = 0
        _StencilOpZFail ("Stencil Z-Fail Operation", Float) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _Albedo1; uniform float4 _Albedo1_ST;
            uniform sampler2D _Vertex; uniform float4 _Vertex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _IntensiveColor)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _U_Speed1)
                UNITY_DEFINE_INSTANCED_PROP( float, _V_Speed1)
                UNITY_DEFINE_INSTANCED_PROP( float, _Slider_Vertex)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float _U_Speed1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _U_Speed1 );
                float _V_Speed1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _V_Speed1 );
                float4 node_1490 = _Time;
                float2 node_680 = ((float2(_U_Speed1_var,_V_Speed1_var)*node_1490.g)+o.uv0);
                float4 _Vertex_var = tex2Dlod(_Vertex,float4(TRANSFORM_TEX(node_680, _Vertex),0.0,0));
                float _Slider_Vertex_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Slider_Vertex );
                v.vertex.xyz += (_Vertex_var.rgb+_Slider_Vertex_var);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
////// Lighting:
////// Emissive:
                float _U_Speed1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _U_Speed1 );
                float _V_Speed1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _V_Speed1 );
                float4 node_1490 = _Time;
                float2 node_680 = ((float2(_U_Speed1_var,_V_Speed1_var)*node_1490.g)+i.uv0);
                float4 _Albedo1_var = tex2D(_Albedo1,TRANSFORM_TEX(node_680, _Albedo1));
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float _IntensiveColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _IntensiveColor );
                float3 emissive = (_Albedo1_var.rgb+_Color_var.rgb+_IntensiveColor_var);
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
            Cull Back
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _Vertex; uniform float4 _Vertex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _U_Speed1)
                UNITY_DEFINE_INSTANCED_PROP( float, _V_Speed1)
                UNITY_DEFINE_INSTANCED_PROP( float, _Slider_Vertex)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float4 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float _U_Speed1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _U_Speed1 );
                float _V_Speed1_var = UNITY_ACCESS_INSTANCED_PROP( Props, _V_Speed1 );
                float4 node_1490 = _Time;
                float2 node_680 = ((float2(_U_Speed1_var,_V_Speed1_var)*node_1490.g)+o.uv0);
                float4 _Vertex_var = tex2Dlod(_Vertex,float4(TRANSFORM_TEX(node_680, _Vertex),0.0,0));
                float _Slider_Vertex_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Slider_Vertex );
                v.vertex.xyz += (_Vertex_var.rgb+_Slider_Vertex_var);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
