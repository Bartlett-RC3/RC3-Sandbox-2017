/*
* Notes
*/

Shader "RC3/VertexColor"
{
	Properties
	{
	}

		SubShader
		{
			Pass
			{
				Tags
				{
					"RenderType" = "Opaque"
				}

			LOD 100

			CGPROGRAM
			#pragma vertex vertMain
			#pragma fragment fragMain
			#include "UnityCG.cginc"

			/*
			Structs
			*/

			struct vertIn
			{
				float4 pos : POSITION;
				float4 color : COLOR;
			};


			struct fragIn
			{
				float4 pos : POSITION;
				float4 color : COLOR;
			};

			/*
			Shader programs
			*/
			
			fragIn vertMain(vertIn v)
			{
				fragIn f;
				f.pos = UnityObjectToClipPos(v.pos);
				f.color = v.color;
				return f;
			}


			float4 fragMain(fragIn f) : COLOR
			{
				return f.color;
			}

			ENDCG
		}
	}
}
