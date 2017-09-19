// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Mask"

{
	 SubShader {
         Tags {"Queue" = "Geometry-10" }       
         Lighting Off
         ZTest LEqual
         ZWrite On
         ColorMask 0
         Pass {}
     }

}