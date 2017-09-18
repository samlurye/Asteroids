Shader "Unlit/Static"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Distortion ("Distortion", Range(0, 0.03)) = 0.01 
		_DistortionSpeed ("Distortion Speed", Range(0, 3000)) = 1000 
		_Thickness ("Static Thickness", Range(0, 1000)) = 500
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" }

		GrabPass {
			"_BackgroundTexture"
		}

		Pass {
			CGPROGRAM

			float _Distortion;
			float _DistortionSpeed;
			float _Thickness;

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float2 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);
                return o;
            }

            sampler2D _BackgroundTexture;

            float4 frag(v2f i) : SV_Target {
                float y = floor(i.grabPos.y * _Thickness);
                if ((i.grabPos.y * _Thickness) - y >= 0.5) {
                	y = y + 1;
                }
                float x = floor(i.grabPos.x * _Thickness);
                if ((i.grabPos.x * _Thickness) - x >= 0.5) {
                	x = x + 1;
                }
                float4 bgcolor;
                if (fmod(y, 2) == 1 || fmod(x, 2) == 1) {
                	bgcolor = float4(0, 0, 0, 0);
                } else {
                	float dx;
                	float dy;
                	if (fmod(y, 4) == 0) {
                		dx = _Distortion * sin(_DistortionSpeed * _Time[0]);
                	} else {
                		dx = -_Distortion * sin(_DistortionSpeed * _Time[0]);
                	}
                	if (fmod(x, 4) == 0) {
                		dy = _Distortion * sin(_DistortionSpeed * _Time[0]);
                	} else {
                		dy = -_Distortion * sin(_DistortionSpeed * _Time[0]);
                	}
                	bgcolor = tex2D(_BackgroundTexture, i.grabPos + float2(dx, dy));
                }
                return bgcolor;
            }
            ENDCG
		}
	}
}
