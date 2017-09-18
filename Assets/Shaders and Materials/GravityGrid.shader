Shader "Unlit/GravityGrid"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Intensity ("Intensity", Range(0, 0.03)) = 0.03
	}
	SubShader
	{
		Pass {
			CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
            	float2 uv : TEXCOORD0;
            	float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
            	float4 vertex : POSITION;
            };

            v2f vert(appdata v) {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            uniform float4 _FieldPositions[10];
            uniform float _Intensity;

            float4 frag(v2f i) : SV_Target {
            	float2 finalDeltaUV = float2(0, 0);
            	float4 col = float4(0, 0, 0, 1);
            	for (int j = 0; j < 10; j++) {
            		float2 signedDist = i.uv - float2(_FieldPositions[j].x, _FieldPositions[j].y);
            		float dist = distance(i.uv, float2(_FieldPositions[j].x, _FieldPositions[j].y));
            		if (dist >= 0.2) {
            			continue;
            		} else if (dist >= 0.00) {
            			finalDeltaUV += _Intensity * (.2 - dist) / (.2 + dist) * normalize(signedDist);
            		} else {
            			return col;
            		}
            	}
            	col = tex2D(_MainTex, i.uv + finalDeltaUV);
                return col;
            }
            ENDCG
		}
	}
}

