Shader "dot3_refract" {
Properties {
 _NormalMap ("Normal Map", 2D) = "bump" {}
 _NormalMap2 ("Normal Map2", 2D) = "bump" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "RenderType"="Transparent" }
  ColorMask 0
 }
 Pass {
  Tags { "QUEUE"="Transparent" "RenderType"="Transparent" }
  Blend Zero SrcColor
  SetTexture [_NormalMap] { combine texture }
  SetTexture [_NormalMap2] { combine texture dot3 previous }
 }
}
}