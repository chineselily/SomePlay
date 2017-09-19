// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UVAnimate"
{
	Properties {
        _MainTex ("Water Texture1", 2D) = "white" {}

		_MainColor("MainColor" , Color) =  (0,0,0,0)

		_BackColor("BackColor" , Color) =  (0,0,0,0)

		_TransVal ("Transparency Value", Range(0,1)) = 0.5

        _XScrollSpeed("X ScrollSpeed", float) = 0

        _YScrollSpeed("Y ScrollSpeed", float) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Transparent"}  
        LOD 200

		Cull front

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
		fixed4 _BackColor;
		float _TransVal;

        float _XScrollSpeed;
        float _YScrollSpeed;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            float2 uvScrolled = IN.uv_MainTex;

            uvScrolled += float2(_XScrollSpeed * _Time.y, _YScrollSpeed * _Time.y);

            half4 c = tex2D (_MainTex, uvScrolled);

            o.Albedo = c.rgb;
			o.Emission = tex2D (_MainTex, IN.uv_MainTex) * _BackColor ;
			//o.Emission = tex2D (_MainTex, uvScrolled);
            o.Alpha = c.a * _TransVal * 0.5;
        }
        ENDCG

		Cull back

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
		float _TransVal;
		fixed4 _MainColor;

        float _XScrollSpeed;
        float _YScrollSpeed;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            float2 uvScrolled = IN.uv_MainTex;

            uvScrolled += float2(_XScrollSpeed * _Time.y, _YScrollSpeed * _Time.y);

            half4 c = tex2D (_MainTex, uvScrolled);

            o.Albedo = c.rgb;
			o.Emission = tex2D (_MainTex, IN.uv_MainTex) * _MainColor;
			//o.Emission = tex2D (_MainTex, uvScrolled);
            o.Alpha = c.a * _TransVal;
        }
        ENDCG


    } 
    FallBack "Diffuse"
}
