±	Shader "Hidden/TextureCopy" {
Properties {
 _MainTex ("Main Texture", 2D) = "white" {}
}
SubShader { 
 Tags { "QUEUE"="Background-999" }
 Pass {
  Tags { "QUEUE"="Background-999" }
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_TextureMatrix0 glstate_matrix_texture0
uniform mat4 glstate_matrix_texture0;

varying mediump vec2 xlv_TEXCOORD0;

attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  mediump vec2 tmpvar_1;
  highp vec2 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3.zw = vec2(0.0, 0.0);
  tmpvar_3.x = _glesMultiTexCoord0.x;
  tmpvar_3.y = _glesMultiTexCoord0.y;
  tmpvar_2 = (gl_TextureMatrix0 * tmpvar_3).xy;
  tmpvar_1 = tmpvar_2;
  gl_Position = _glesVertex;
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

varying mediump vec2 xlv_TEXCOORD0;
uniform sampler2D _MainTex;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_1 = tmpvar_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
"!!GLES"
}
}
 }
}
Fallback Off
}