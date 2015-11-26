Shader "PlaySide/Lit/Diffuse" {
   Properties {
	  _MainTex ("Texture", 2D) = "white" {}
	  _Color ("Main Color", Color) = (1,1,1,1)
	}
	
	SubShader {
	  Tags { "RenderType"="Opaque" "Queue" = "Geometry" }
	 Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            uniform float4 _LightColor0; 
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed3 _Color;

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 color : COLOR;
				LIGHTING_COORDS(1,2)
			};

			v2f vert (appdata_full v)
			{				
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex); 
				o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
				o.color = v.color.rgb;
				TRANSFER_VERTEX_TO_FRAGMENT(o);
				return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
				half3 shadow = 1+(SHADOW_ATTENUATION(i)-1) * (SHADOW_ATTENUATION(i)-1) * (UNITY_LIGHTMODEL_AMBIENT.rgb-1);
				half3 col = tex2D(_MainTex, i.uv.xy).rgb* i.color * shadow;


				return half4(col, 1);
			}
			ENDCG
		}
	}
	FallBack "VertexLit"
}
