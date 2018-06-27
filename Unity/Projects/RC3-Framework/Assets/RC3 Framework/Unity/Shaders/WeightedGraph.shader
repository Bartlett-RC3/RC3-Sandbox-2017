/*
* Notes
*
* Shaderlab culling semantics
* https://docs.unity3d.com/Manual/SL-CullAndDepth.html
*/

Shader "RC3/WeightedGraph"
{
	Properties
	{
		_texture("Texture", 2D) = ""{}
		_widthScale("Width Scale", Float) = 0.1
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
			Cull Off
			LOD 100

			CGPROGRAM
			#pragma vertex vertMain
			#pragma geometry geomMain
			#pragma fragment fragMain
			#include "UnityCG.cginc"

			sampler2D _texture;
			float _widthScale;

			/*
			Structs
			*/

			struct vertIn
			{
				float4 pos : POSITION;
				float2 tex : TEXCOORD0;
				float width : TEXCOORD1;
			};


			struct geomIn
			{
				float4 pos : POSITION;
				float2 tex : TEXCOORD0;
				float width : TEXCOORD1;
			};


			struct fragIn
			{
				float4 pos : POSITION;
				float2 tex : TEXCOORD0;
			};

			/*
			Shader programs
			*/

			geomIn vertMain(vertIn v)
			{
				geomIn g;
				g.pos = UnityObjectToClipPos(v.pos);
				g.tex = v.tex;
				g.width = v.width;
				return g;
			}


			[maxvertexcount(4)]
			void geomMain(line geomIn g[2], inout TriangleStream<fragIn> triStream)
			{
				float4 p0 = g[0].pos;
				float4 p1 = g[1].pos;

				float2 t0 = g[0].tex;
				float2 t1 = g[1].tex;

				float w0 = g[0].width * _widthScale;
				float w1 = g[1].width * _widthScale;

				float2 d = normalize((p1 - p0).xy);
				d = float2(-d.y, d.x); // perp CCW

				float4 d0 = float4(d * w0, 0.0, 0.0);
				float4 d1 = float4(d * w1, 0.0, 0.0);

				// append to stream
				// triangles streamed in strip order (i.e. 0,1,2/2,1,3/2,3,4/...)

				fragIn f;

				f.pos = p0 - d0;
				f.tex = t0;
				triStream.Append(f);

				f.pos = p1 - d1;
				f.tex = t1;
				triStream.Append(f);

				f.pos = p0 + d0;
				f.tex = t0;
				triStream.Append(f);

				f.pos = p1 + d1;
				f.tex = t1;
				triStream.Append(f);
			}


			float4 fragMain(fragIn f) : COLOR
			{
				return tex2D(_texture, f.tex);
			}

			ENDCG
		}
	}
}
