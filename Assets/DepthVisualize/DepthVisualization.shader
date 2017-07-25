Shader "Hidden/DepthVisualization"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Seperate ("Seperate", range(0, 1)) = 0.5
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _CameraDepthTexture;
			float4 _CameraDepthTexture_TexelSize;
			half _Seperate;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = float4(1, 0, 0, 1);
				if (i.vertex.x > _CameraDepthTexture_TexelSize.z / (1 / _Seperate)) {
					float depth = tex2D(_CameraDepthTexture, i.uv).r;
					col = float4(depth, depth, depth, 1 );
				} else {
					col = tex2D(_MainTex, i.uv);
				}
				return col;
			}
			ENDCG
		}
	}
}
