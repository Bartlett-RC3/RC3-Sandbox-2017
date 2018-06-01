/*
* Notes
*/

Shader "RC3/RimPulsePixel"
{
	Properties
	{
		_texture("Texture", 2D) = ""{}
		_power0("Power0", Float) = 2.0
		_power1("Power1", Float) = 4.0
		_period("Period", Float) = 1.0
	}

		SubShader
		{
			Pass
			{
				Tags
				{
					"RenderType" = "Transparent"
					"Queue" = "Transparent"
				}

			Blend SrcAlpha OneMinusSrcAlpha // typical alpha blending
			ZWrite Off
			Cull Off
			LOD 200

			CGPROGRAM
			#pragma vertex vertMain
			#pragma fragment fragMain
			#include "UnityCG.cginc"

			/*
			Globals
			*/

			#define PI 3.14159274101257
			#define TWOPI (PI * 2.0)

			sampler2D _texture;
			float _power0;
			float _power1;
			float _period; // seconds per pulse

			static float _t = cos(_Time.y / _period * TWOPI) * 0.5 + 0.5;
			static float _power = lerp(_power0, _power1, _t);

			
			/*
			Structs
			*/

			struct vertIn
			{
				float4 pos : POSITION;
				float4 norm : NORMAL;
			};


			struct fragIn
			{
				float4 pos : POSITION0;
				float3 viewPos : NORMAL0;
				float3 viewNorm : NORMAL1;
			};

			/*
			Helper functions
			*/

			inline float cosAngle(float3 v0, float3 v1)
			{
				return dot(v0, v1) / sqrt(dot(v0, v0) * dot(v1, v1));
			}

			/*
			Shader programs
			*/
			
			fragIn vertMain(vertIn v)
			{
				fragIn f;
				f.pos = UnityObjectToClipPos(v.pos);
				f.viewPos = mul(UNITY_MATRIX_MV, v.pos);
				f.viewNorm = mul(UNITY_MATRIX_IT_MV, v.norm);
				return f;
			}


			float4 fragMain(fragIn f) : COLOR
			{
				float ca = abs(cosAngle(-f.viewPos, f.viewNorm));
				float u = pow(1.0 - saturate(ca), _power);
				return tex2D(_texture, float2(u,0.0));
			}

			ENDCG
		}
	}
}
