Shader "Lit/Diffuse With Ambient"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Max("Max", Float) = 0
		_StartTime("StarTime", Float) = 0
		_TimeFactor("TimeFactor", Float) = 1
    }
    SubShader
    {
        Pass
        {
            Tags {"LightMode"="ForwardBase"}
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 diff : COLOR0;
                float4 vertex : SV_POSITION;
            };
			
			float _StartTime;
			float _Max;
			float _TimeFactor;

            v2f vert (appdata_base v)
            {
                v2f o;				
				float4 offset = float4(0, 0, 0, 0);
				if (v.vertex.y > -0.2)
				{
					offset.x = 0.2 * sin(_Time.y * _TimeFactor) * _Max * (abs(v.vertex.y) + 0.1);
					offset.z = 0.2 * sin(_Time.y *  _TimeFactor) * _Max * (abs(v.vertex.y) + 0.1);
				}
				v.vertex = v.vertex + offset;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0;

                // the only difference from previous shader:
                // in addition to the diffuse lighting from the main light,
                // add illumination from ambient or light probes
                // ShadeSH9 function from UnityCG.cginc evaluates it,
                // using world space normal
                o.diff.rgb += ShadeSH9(half4(worldNormal,1));
                return o;
            }
            
            sampler2D _MainTex;
			float4 _Color;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                col *= i.diff;
                return col;
            }
            ENDCG
        }
         // shadow caster rendering pass, implemented manually
        // using macros from UnityCG.cginc
        Pass
        {
            Tags {"LightMode"="ShadowCaster"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f { 
                V2F_SHADOW_CASTER;
            };

			float _MoveSpeed;
			float4 _MoveDirection;
            v2f vert(appdata_base v)
            {
                v2f o;				
				float4 offset = float4(0, 0, 0, 0);
				if (v.vertex.y > -0.2)
				{
					offset.x = 0.3 * sin(3.1416 * _Time.y * _MoveSpeed  ) * (abs(v.vertex.y) + 0.1)  *  clamp(v.texcoord.y, 0, 1)* _MoveDirection.x;
					offset.z = 0.2 * sin(3.1416 * _Time.y * _MoveSpeed) * (abs(v.vertex.y) + 0.1) * clamp(v.texcoord.y, 0, 1)  * _MoveDirection.z;
				}
				v.vertex = v.vertex + offset;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}