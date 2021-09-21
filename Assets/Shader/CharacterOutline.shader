Shader "Custom/CharacterOutline"
{
	Properties{
		_MainTex("Albedo", 2D) = "white" {}
		_BumpMap("BumpMap", 2D) = "bump" {}
		_OutlineColor("OutlineColor", Color) = (1,1,1,1)
		_Outline("Outline", Range(0.0005, 0.01)) = 0.01
	}

		SubShader{
			Tags { "RenderType" = "Opaque" }
			Cull front

			// Pass1
			CGPROGRAM
			#pragma surface surf NoLighting vertex:vert noshadow noambient

			sampler2D _MainTex;
			sampler2D _BumpMap;
			struct Input {
				float2 uv_MainTex;
				float2 uv_BumpMap;
				fixed4 color : Color;
			};

			fixed4 _OutlineColor;
			float _Outline;

			void vert(inout appdata_full v)
			{
				v.vertex.xyz += v.normal.xyz * _Outline;
			}

			void surf(Input In, inout SurfaceOutput o)
			{
			}

			fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
			{
				return _OutlineColor;
			}
			ENDCG

				// Pass2
				Cull back
				CGPROGRAM
				#pragma surface surf Lambert

				fixed4 _Color;
				sampler2D _MainTex;
				sampler2D _BumpMap;
				struct Input {
					float2 uv_MainTex;
					float2 uv_BumpMap;
					fixed4 color : Color;
				};

				void surf(Input In, inout SurfaceOutput o)
				{
					fixed4 c = tex2D(_MainTex, In.uv_MainTex);
					o.Albedo = c.rgb;
					o.Normal = UnpackNormal(tex2D(_BumpMap, In.uv_BumpMap));
					o.Alpha = c.a;
				}
				ENDCG
		}
			FallBack "Diffuse"

	//Properties{
	//   _MainTex("Albedo", 2D) = "white" {}
	//   _BumpMap("BumpMap", 2D) = "bump" {}
	//   _OutlineColor("OutlineColor", Color) = (1,1,1,1)
	//   _Outline("Outline", Range(0.0005, 0.01)) = 0.01
	//}

	//	SubShader{
	//		Tags { "RenderType" = "Opaque" }
	//		Cull front

	//	   // Pass1
	//	   CGPROGRAM
	//	   #pragma surface surf NoLighting vertex:vert noshadow noambient

	//	   sampler2D _MainTex;
	//	   sampler2D _BumpMap;
	//	   struct Input {
	//		   float2 uv_MainTex;
	//		   float2 uv_BumpMap;
	//	   };

	//	   fixed4 _OutlineColor;
	//	   float _Outline;

	//	   void vert(inout appdata_full v)
	//	   {
	//		   v.vertex.xyz += v.normal.xyz * _Outline;
	//	   }

	//	   void surf(Input In, inout SurfaceOutput o)
	//	   {
	//	   }

	//	   fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
	//	   {
	//		   return _OutlineColor;
	//	   }
	//	   ENDCG

	//		   // Pass2
	//		   Cull back
	//		   CGPROGRAM
	//		   #pragma surface surf Toon

	//		   fixed4 _Color;
	//		   sampler2D _MainTex;
	//		   sampler2D _BumpMap;
	//		   struct Input {
	//			   float2 uv_MainTex;
	//			   float2 uv_BumpMap;
	//			   fixed4 color : Color;
	//		   };

	//		   void surf(Input In, inout SurfaceOutput o)
	//		   {
	//			   fixed4 c = tex2D(_MainTex, In.uv_MainTex);
	//			   o.Albedo = c.rgb;
	//			   o.Normal = UnpackNormal(tex2D(_BumpMap, In.uv_BumpMap));
	//			   o.Alpha = c.a;
	//		   }

	//		   fixed4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed atten)
	//		   {
	//			   fixed halfLambert = dot(s.Normal, lightDir) * 0.5 + 0.5;
	//			   halfLambert = ceil(halfLambert * 5) / 5;

	//			   fixed4 final;
	//			   final.rgb = s.Albedo * halfLambert *_LightColor0.rgb;
	//			   final.a = s.Alpha;
	//			   return final;
	//		   }
	//		   ENDCG
	//   }
	//	   FallBack "Diffuse"

	//Properties{
	//	_Color("Main Color", Color) = (0.5,0.5,0.5,1)
	//	_MainTex("Texture", 2D) = "white" {}
	//	_OutlineColor("Outline Color", Color) = (0,0,0,1)
	//	_OutlineWidth("Outline Width", Range(1.0,5.0)) = 1.01
	//}

	//CGINCLUDE
	//#include "UnityCG.cginc"

	//struct appdata
	//{
	//	float4 vertex : POSITION;
	//	float3 normal : NORMAL;
	//};

	//struct v2f
	//{
	//	float4 pos : POSITION;
	//	float3 normal : NOMAL;
	//};

	//float _OutlineWidth;
	//float4 _OutlineColor;

	//v2f vert(appdata v) {
	//	v.vertex.xyz *= _OutlineWidth;

	//	v2f o;
	//	o.pos = UnityObjectToClipPos(v.vertex);
	//	return o;
	//}

	//ENDCG

	//SubShader{
	//	Tags { "Queue" = "Transparent"}
	//	Pass // 아웃라인 그리기
	//	{
	//		ZWrite Off

	//		CGPROGRAM
	//		#pragma vertex vert
	//		#pragma fragment frag

	//		half4 frag(v2f i) : COLOR
	//		{
	//			return _OutlineColor;
	//		}

	//		ENDCG
	//	}

	//	Pass // 일반 렌더
	//	{
	//		ZWrite On
	//		Material
	//		{
	//			Diffuse[_Color]
	//			Ambient[_Color]
	//		}

	//		Lighting On


	//		SetTexture[_MainTex]
	//		{
	//			ConstantColor[_Color]
	//		}

	//		SetTexture[_MainTex]
	//		{
	//			Combine previous * primary DOUBLE
	//		}
	//	}
	//	
	//}


	//Properties{
	//   _MainTex("Albedo", 2D) = "white" {}
	//   _BumpMap("BumpMap", 2D) = "bump" {}
	//   _OutlineColor("OutlineColor", Color) = (1,1,1,1)
	//   _Outline("Outline", Range(0.1, 0.4)) = 0.2
	//}

	//	SubShader{
	//		Tags { "Queue" = "Transparent"}
	//		Cull back
	//		CGPROGRAM
	//		#pragma surface surf Toon 

	//		fixed4 _Color;
	//		sampler2D _MainTex;
	//		sampler2D _BumpMap;
	//		struct Input {
	//			float2 uv_MainTex;
	//			float2 uv_BumpMap;
	//		};

	//		fixed4 _OutlineColor;
	//		fixed _Outline;

	//		void surf(Input In, inout SurfaceOutput o)
	//		{
	//			fixed4 c = tex2D(_MainTex, In.uv_MainTex);
	//			o.Albedo = c.rgb;
	//			o.Normal = UnpackNormal(tex2D(_BumpMap, In.uv_BumpMap));
	//			o.Alpha = c.a;
	//		}

	//		fixed4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
	//		{
	//			float halfLambert = dot(s.Normal, lightDir) * 0.5 + 0.5;
	//			if (halfLambert > 0.7) {
	//				halfLambert = 1;
	//			}
	//			else {
	//				halfLambert = 0.3;
	//			}

	//			fixed4 final;
	//			float rim = abs(dot(s.Normal, viewDir));
	//			if (rim > _Outline) {
	//				rim = 1;
	//				final.rgb = s.Albedo;
	//			}
	//			else {
	//				rim = -1;
	//				final.rgb = _OutlineColor.rgb * rim;
	//			}

	//			final.rgb = final.rgb * halfLambert *_LightColor0.rgb * rim;
	//			final.a = s.Alpha;
	//			return final;
	//		}
	//		ENDCG
	//   }
	//	   FallBack "Diffuse"
}
