Shader "iPhone/LightmapOnlyShader vertexlit" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _LightMap ("Lightmap (RGB)", 2D) = "white" {}
}
SubShader { 
 Pass {
  Name "BASE"
  Tags { "LIGHTMODE"="Always" }
  BindChannels {
   Bind "vertex", Vertex
   Bind "texcoord1", TexCoord0
   Bind "texcoord", TexCoord1
  }
  SetTexture [_LightMap] { ConstantColor [_Color] combine texture * constant }
  SetTexture [_MainTex] { combine texture * previous }
 }
}
}