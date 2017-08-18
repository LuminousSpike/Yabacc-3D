// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Taken from http://answers.unity3d.com/questions/1312800/3d-shader-text-receives-shadow-only-on-editor.html

Shader "GUI/Text Shader Card" {
    Properties {
        _MainTex ("Font Texture", 2D) = "white" {}
        _Color ("Text Color", Color) = (1,1,1,1)
		_Cutoff("Shadow Alpha cutoff", Range(0.25,0.9)) = 1.0
    }

         SubShader
     {
		 Tags {
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }

		Lighting On Cull Off ZTest Less ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha


         CGPROGRAM
         sampler2D _MainTex;
         half4 _Color;
         #pragma surface surf SimpleLambert alphatest:_Cutoff        
 
         half4 LightingWrapLambert(SurfaceOutput s, half3 lightDir, half atten) {
             half NdotL = dot(s.Normal, lightDir);
             half diff = NdotL * 0.5 + 0.5;
             half4 c;
             c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten);
             c.a = s.Alpha;
             return c;
         }
 
         half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten) {
             half NdotL = dot(s.Normal, lightDir);
             half4 c;
             c.rgb = s.Albedo * _LightColor0.rgb * ((NdotL + 1) * atten);
             c.a = s.Alpha;
             return c;
         }
 
         struct Input
         {
             //float2 uv_MainTex;
             float2 uv_MainTex;
             half4 color : Color;
         };
         void surf(Input IN, inout SurfaceOutput o) {
             half alpha = tex2D(_MainTex, IN.uv_MainTex).a;
             o.Alpha = alpha;
             //o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;            
             o.Albedo = IN.color.rgb;
         }
         ENDCG  
     }
     Fallback "Transparent/Cutout/Diffuse"
}
