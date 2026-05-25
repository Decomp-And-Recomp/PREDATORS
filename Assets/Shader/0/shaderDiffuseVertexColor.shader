Shader "DiffuseVertexColorEmissive" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _RangeEmission ("Emission Strength", Range(0,1)) = 0.5
    }
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert

        sampler2D _MainTex;
        fixed4 _Color;
        float _RangeEmission;

        struct Input {
            float2 uv_MainTex;
            fixed4 vertexColor;
        };

        void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.vertexColor = v.color; // Pass vertex color
        }

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.vertexColor * _Color;
            o.Albedo = c.rgb;
            o.Emission = c.rgb * _RangeEmission;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}