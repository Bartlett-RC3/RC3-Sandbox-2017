
/*
Geometry shader semantics
https://msdn.microsoft.com/en-us/library/windows/desktop/bb509609(v=vs.85).aspx
*/

Shader "RC3/SDFDemo/ScalarField"
{
	Properties
	{
		_gradient ("Gradient", 2D) = "" {}
		_threshold("Threshold", Float) = 1.0
		_offset("Offset", Float) = 1.0
		_scale("Scale", Float) = 1.0
		//_period("Period", Float) = 3.0
	}
	SubShader
	{
		Tags 
		{ 
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		}

		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Cull Off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma target 4.0
			#pragma vertex vertMain
			#pragma geometry geomMain
			#pragma fragment fragMain
			#include "UnityCG.cginc"

			sampler2D _gradient;
			float _threshold;
			float _offset;
			float _scale;
			//float _period;

			/*
			Structs
			*/

			struct vertIn
			{
				float4 pos : POSITION;
				float val : TEXCOORD0;
			};


			struct geomIn
			{
				float4 pos : POSITION;
				float val : TEXCOORD0;
			};


			struct fragIn
			{
				float4 pos : POSITION; // need position
				float2 tex : TEXCOORD0;
				float val : TEXCOORD1;
			};

			/*
			Helper functions
			*/

			inline float smoothPulse(float t, float center, float width)
			{
				return smoothstep(0.0, width, abs(t - center));
			}

			/*
			Shader programs
			*/

			geomIn vertMain(vertIn v)
			{
				geomIn g;
				g.pos = mul(UNITY_MATRIX_MV, v.pos);
				g.val = v.val;
				return g;
			}


			[maxvertexcount(4)]
			void geomMain(point geomIn g[1], inout TriangleStream<fragIn> triStream)
			{
				float3 p = g[0].pos;

				// append to stream
				// triangles streamed in strip order (i.e. 0,1,2/2,1,3/2,3,4/...)

				fragIn f;
				f.val = smoothstep(_threshold - _offset, _threshold + _offset, g[0].val);

				f.pos = mul(UNITY_MATRIX_P, float4(p.x - _scale, p.y - _scale, p.z, 1.0));
				f.tex = float2(0.0, 0.0);
				triStream.Append(f);

				f.pos = mul(UNITY_MATRIX_P, float4(p.x + _scale, p.y - _scale, p.z, 1.0));
				f.tex = float2(1.0, 0.0);
				triStream.Append(f);

				f.pos = mul(UNITY_MATRIX_P, float4(p.x - _scale, p.y + _scale, p.z, 1.0));
				f.tex = float2(0.0, 1.0);
				triStream.Append(f);

				f.pos = mul(UNITY_MATRIX_P, float4(p.x + _scale, p.y + _scale, p.z, 1.0));
				f.tex = float2(1.0, 1.0);
				triStream.Append(f);
			}


			float4 fragMain(fragIn f) : COLOR
			{
				float4 c = tex2D(_gradient, float2(f.val, 0.0));
				c.a *= smoothstep(0.5, 0.0, length(f.tex - float2(0.5, 0.5))); // alpha as function of texture coords

				return c;
			}
			
			ENDCG
		}
	}
}
