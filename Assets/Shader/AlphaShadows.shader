Shader "AlphaWithShadows" {
    Properties {
        _TintColor ("Tint Color", Color) = (0.109592, 0.626866, 0, 1)
        _MainTex ("Base Texture (RGBA)", 2D) = "black" {}
    }
    SubShader { 
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 200

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade

        fixed4 _TintColor;
        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _TintColor;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Transparent/Cutout/VertexLit"
}