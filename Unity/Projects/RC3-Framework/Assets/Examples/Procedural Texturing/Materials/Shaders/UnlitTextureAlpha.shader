/*
* Notes
*/

Shader "RC3/UnlitTextureAlpha"
{
	Properties
	{
		_texture("Texture", 2D) = ""{}
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
			Variables
			*/

			sampler2D _texture;


			/*
			Structs
			*/

			struct vertIn
			{
				float4 pos : POSITION;
				float2 tex : TEXCOORD0;
			};


			struct fragIn
			{
				float4 pos : POSITION;
				float2 tex : TEXCOORD0;
			};

			/*
			Shader programs
			*/
			
			fragIn vertMain(vertIn v)
			{
				fragIn f;
				f.pos = UnityObjectToClipPos(v.pos);
				f.tex = v.tex;
				return f;
			}


			float4 fragMain(fragIn f) : COLOR
			{
				return tex2D(_texture, f.tex);
			}

			ENDCG
		}
	}
}
