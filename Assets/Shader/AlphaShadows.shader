οτShader "AlphaWithShadows" {
Properties {
 _TintColor ("_TintColor", Color) = (0.109592,0.626866,0,1)
 _MainTex ("_MainTex", 2D) = "black" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
  ZWrite Off
  Blend SrcAlpha OneMinusSrcAlpha
Program "vp" {
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  lowp vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  mat3 tmpvar_5;
  tmpvar_5[0] = _Object2World[0].xyz;
  tmpvar_5[1] = _Object2World[1].xyz;
  tmpvar_5[2] = _Object2World[2].xyz;
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_3 = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 1.0;
  tmpvar_9.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = (tmpvar_5 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_11;
  mediump vec4 normal;
  normal = tmpvar_10;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_12;
  tmpvar_12 = dot (unity_SHAr, normal);
  x1.x = tmpvar_12;
  highp float tmpvar_13;
  tmpvar_13 = dot (unity_SHAg, normal);
  x1.y = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAb, normal);
  x1.z = tmpvar_14;
  mediump vec4 tmpvar_15;
  tmpvar_15 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_16;
  tmpvar_16 = dot (unity_SHBr, tmpvar_15);
  x2.x = tmpvar_16;
  highp float tmpvar_17;
  tmpvar_17 = dot (unity_SHBg, tmpvar_15);
  x2.y = tmpvar_17;
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBb, tmpvar_15);
  x2.z = tmpvar_18;
  mediump float tmpvar_19;
  tmpvar_19 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_19;
  highp vec3 tmpvar_20;
  tmpvar_20 = (unity_SHC.xyz * vC);
  x3 = tmpvar_20;
  tmpvar_11 = ((x1 + x2) + x3);
  shlight = tmpvar_11;
  tmpvar_4 = shlight;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_7 * (((_World2Object * tmpvar_9).xyz * unity_Scale.w) - _glesVertex.xyz));
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _MainTex;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  highp vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD3);
  mediump vec3 lightDir;
  lightDir = xlv_TEXCOORD1;
  mediump vec3 viewDir;
  viewDir = tmpvar_6;
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_7;
  tmpvar_7 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir + viewDir))));
  nh = tmpvar_7;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_8;
  tmpvar_8 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_9;
  tmpvar_9 = (pow (nh, 0.0) * tmpvar_8);
  res.w = tmpvar_9;
  mediump vec4 tmpvar_10;
  tmpvar_10 = (res * 2.0);
  res = tmpvar_10;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_10.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  mediump vec3 tmpvar_11;
  tmpvar_11 = (c.xyz + (tmpvar_1 * xlv_TEXCOORD2));
  c.xyz = tmpvar_11;
  mediump vec3 tmpvar_12;
  tmpvar_12 = c.xyz;
  c.xyz = tmpvar_12;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _MainTex_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform highp vec4 _TintColor;
uniform sampler2D _MainTex;
void main ()
{
  lowp vec4 c;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  c = vec4(0.0, 0.0, 0.0, 0.0);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD1).xyz);
  mediump vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_1 * tmpvar_6);
  c.xyz = tmpvar_7;
  c.w = tmpvar_2;
  mediump vec3 tmpvar_8;
  tmpvar_8 = c.xyz;
  c.xyz = tmpvar_8;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  lowp vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (gl_ModelViewProjectionMatrix * _glesVertex);
  mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp mat3 tmpvar_7;
  tmpvar_7[0] = tmpvar_1.xyz;
  tmpvar_7[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_7[2] = tmpvar_2;
  mat3 tmpvar_8;
  tmpvar_8[0].x = tmpvar_7[0].x;
  tmpvar_8[0].y = tmpvar_7[1].x;
  tmpvar_8[0].z = tmpvar_7[2].x;
  tmpvar_8[1].x = tmpvar_7[0].y;
  tmpvar_8[1].y = tmpvar_7[1].y;
  tmpvar_8[1].z = tmpvar_7[2].y;
  tmpvar_8[2].x = tmpvar_7[0].z;
  tmpvar_8[2].y = tmpvar_7[1].z;
  tmpvar_8[2].z = tmpvar_7[2].z;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_3 = tmpvar_9;
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 1.0;
  tmpvar_11.xyz = (tmpvar_6 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_12;
  mediump vec4 normal;
  normal = tmpvar_11;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_13;
  tmpvar_13 = dot (unity_SHAr, normal);
  x1.x = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAg, normal);
  x1.y = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAb, normal);
  x1.z = tmpvar_15;
  mediump vec4 tmpvar_16;
  tmpvar_16 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_17;
  tmpvar_17 = dot (unity_SHBr, tmpvar_16);
  x2.x = tmpvar_17;
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBg, tmpvar_16);
  x2.y = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBb, tmpvar_16);
  x2.z = tmpvar_19;
  mediump float tmpvar_20;
  tmpvar_20 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21 = (unity_SHC.xyz * vC);
  x3 = tmpvar_21;
  tmpvar_12 = ((x1 + x2) + x3);
  shlight = tmpvar_12;
  tmpvar_4 = shlight;
  highp vec4 o_i0_i1;
  highp vec4 tmpvar_22;
  tmpvar_22 = (tmpvar_5 * 0.5);
  o_i0_i1 = tmpvar_22;
  highp vec2 tmpvar_23;
  tmpvar_23.x = tmpvar_22.x;
  tmpvar_23.y = (tmpvar_22.y * _ProjectionParams.x);
  o_i0_i1.xy = (tmpvar_23 + tmpvar_22.w);
  o_i0_i1.zw = tmpvar_5.zw;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_8 * (((_World2Object * tmpvar_10).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_i0_i1;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  lowp float tmpvar_6;
  tmpvar_6 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD4).x;
  highp vec3 tmpvar_7;
  tmpvar_7 = normalize (xlv_TEXCOORD3);
  mediump vec3 lightDir;
  lightDir = xlv_TEXCOORD1;
  mediump vec3 viewDir;
  viewDir = tmpvar_7;
  mediump float atten_i0;
  atten_i0 = tmpvar_6;
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_8;
  tmpvar_8 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir + viewDir))));
  nh = tmpvar_8;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_9;
  tmpvar_9 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_10;
  tmpvar_10 = (pow (nh, 0.0) * tmpvar_9);
  res.w = tmpvar_10;
  mediump vec4 tmpvar_11;
  tmpvar_11 = (res * (atten_i0 * 2.0));
  res = tmpvar_11;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_11.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  mediump vec3 tmpvar_12;
  tmpvar_12 = (c.xyz + (tmpvar_1 * xlv_TEXCOORD2));
  c.xyz = tmpvar_12;
  mediump vec3 tmpvar_13;
  tmpvar_13 = c.xyz;
  c.xyz = tmpvar_13;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _ProjectionParams;
uniform highp vec4 _MainTex_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp vec4 o_i0_i1;
  highp vec4 tmpvar_2;
  tmpvar_2 = (tmpvar_1 * 0.5);
  o_i0_i1 = tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3.x = tmpvar_2.x;
  tmpvar_3.y = (tmpvar_2.y * _ProjectionParams.x);
  o_i0_i1.xy = (tmpvar_3 + tmpvar_2.w);
  o_i0_i1.zw = tmpvar_1.zw;
  gl_Position = tmpvar_1;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD2 = o_i0_i1;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform highp vec4 _TintColor;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
void main ()
{
  lowp vec4 c;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  c = vec4(0.0, 0.0, 0.0, 0.0);
  lowp vec3 tmpvar_6;
  tmpvar_6 = min ((2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD1).xyz), vec3((texture2DProj (_ShadowMapTexture, xlv_TEXCOORD2).x * 2.0)));
  mediump vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_1 * tmpvar_6);
  c.xyz = tmpvar_7;
  c.w = tmpvar_2;
  mediump vec3 tmpvar_8;
  tmpvar_8 = c.xyz;
  c.xyz = tmpvar_8;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  lowp vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  mat3 tmpvar_5;
  tmpvar_5[0] = _Object2World[0].xyz;
  tmpvar_5[1] = _Object2World[1].xyz;
  tmpvar_5[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_5 * (tmpvar_2 * unity_Scale.w));
  highp mat3 tmpvar_7;
  tmpvar_7[0] = tmpvar_1.xyz;
  tmpvar_7[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_7[2] = tmpvar_2;
  mat3 tmpvar_8;
  tmpvar_8[0].x = tmpvar_7[0].x;
  tmpvar_8[0].y = tmpvar_7[1].x;
  tmpvar_8[0].z = tmpvar_7[2].x;
  tmpvar_8[1].x = tmpvar_7[0].y;
  tmpvar_8[1].y = tmpvar_7[1].y;
  tmpvar_8[1].z = tmpvar_7[2].y;
  tmpvar_8[2].x = tmpvar_7[0].z;
  tmpvar_8[2].y = tmpvar_7[1].z;
  tmpvar_8[2].z = tmpvar_7[2].z;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_3 = tmpvar_9;
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 1.0;
  tmpvar_11.xyz = tmpvar_6;
  mediump vec3 tmpvar_12;
  mediump vec4 normal;
  normal = tmpvar_11;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_13;
  tmpvar_13 = dot (unity_SHAr, normal);
  x1.x = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAg, normal);
  x1.y = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAb, normal);
  x1.z = tmpvar_15;
  mediump vec4 tmpvar_16;
  tmpvar_16 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_17;
  tmpvar_17 = dot (unity_SHBr, tmpvar_16);
  x2.x = tmpvar_17;
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBg, tmpvar_16);
  x2.y = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBb, tmpvar_16);
  x2.z = tmpvar_19;
  mediump float tmpvar_20;
  tmpvar_20 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21 = (unity_SHC.xyz * vC);
  x3 = tmpvar_21;
  tmpvar_12 = ((x1 + x2) + x3);
  shlight = tmpvar_12;
  tmpvar_4 = shlight;
  highp vec3 tmpvar_22;
  tmpvar_22 = (_Object2World * _glesVertex).xyz;
  highp vec4 tmpvar_23;
  tmpvar_23 = (unity_4LightPosX0 - tmpvar_22.x);
  highp vec4 tmpvar_24;
  tmpvar_24 = (unity_4LightPosY0 - tmpvar_22.y);
  highp vec4 tmpvar_25;
  tmpvar_25 = (unity_4LightPosZ0 - tmpvar_22.z);
  highp vec4 tmpvar_26;
  tmpvar_26 = (((tmpvar_23 * tmpvar_23) + (tmpvar_24 * tmpvar_24)) + (tmpvar_25 * tmpvar_25));
  highp vec4 tmpvar_27;
  tmpvar_27 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_23 * tmpvar_6.x) + (tmpvar_24 * tmpvar_6.y)) + (tmpvar_25 * tmpvar_6.z)) * inversesqrt (tmpvar_26))) * (1.0/((1.0 + (tmpvar_26 * unity_4LightAtten0)))));
  highp vec3 tmpvar_28;
  tmpvar_28 = (tmpvar_4 + ((((unity_LightColor[0].xyz * tmpvar_27.x) + (unity_LightColor[1].xyz * tmpvar_27.y)) + (unity_LightColor[2].xyz * tmpvar_27.z)) + (unity_LightColor[3].xyz * tmpvar_27.w)));
  tmpvar_4 = tmpvar_28;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_8 * (((_World2Object * tmpvar_10).xyz * unity_Scale.w) - _glesVertex.xyz));
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _MainTex;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  highp vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD3);
  mediump vec3 lightDir;
  lightDir = xlv_TEXCOORD1;
  mediump vec3 viewDir;
  viewDir = tmpvar_6;
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_7;
  tmpvar_7 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir + viewDir))));
  nh = tmpvar_7;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_8;
  tmpvar_8 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_9;
  tmpvar_9 = (pow (nh, 0.0) * tmpvar_8);
  res.w = tmpvar_9;
  mediump vec4 tmpvar_10;
  tmpvar_10 = (res * 2.0);
  res = tmpvar_10;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_10.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  mediump vec3 tmpvar_11;
  tmpvar_11 = (c.xyz + (tmpvar_1 * xlv_TEXCOORD2));
  c.xyz = tmpvar_11;
  mediump vec3 tmpvar_12;
  tmpvar_12 = c.xyz;
  c.xyz = tmpvar_12;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  lowp vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (gl_ModelViewProjectionMatrix * _glesVertex);
  mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_2 * unity_Scale.w));
  highp mat3 tmpvar_8;
  tmpvar_8[0] = tmpvar_1.xyz;
  tmpvar_8[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_8[2] = tmpvar_2;
  mat3 tmpvar_9;
  tmpvar_9[0].x = tmpvar_8[0].x;
  tmpvar_9[0].y = tmpvar_8[1].x;
  tmpvar_9[0].z = tmpvar_8[2].x;
  tmpvar_9[1].x = tmpvar_8[0].y;
  tmpvar_9[1].y = tmpvar_8[1].y;
  tmpvar_9[1].z = tmpvar_8[2].y;
  tmpvar_9[2].x = tmpvar_8[0].z;
  tmpvar_9[2].y = tmpvar_8[1].z;
  tmpvar_9[2].z = tmpvar_8[2].z;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_3 = tmpvar_10;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 1.0;
  tmpvar_11.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_7;
  mediump vec3 tmpvar_13;
  mediump vec4 normal;
  normal = tmpvar_12;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAr, normal);
  x1.x = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAg, normal);
  x1.y = tmpvar_15;
  highp float tmpvar_16;
  tmpvar_16 = dot (unity_SHAb, normal);
  x1.z = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBr, tmpvar_17);
  x2.x = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBg, tmpvar_17);
  x2.y = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHBb, tmpvar_17);
  x2.z = tmpvar_20;
  mediump float tmpvar_21;
  tmpvar_21 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_21;
  highp vec3 tmpvar_22;
  tmpvar_22 = (unity_SHC.xyz * vC);
  x3 = tmpvar_22;
  tmpvar_13 = ((x1 + x2) + x3);
  shlight = tmpvar_13;
  tmpvar_4 = shlight;
  highp vec3 tmpvar_23;
  tmpvar_23 = (_Object2World * _glesVertex).xyz;
  highp vec4 tmpvar_24;
  tmpvar_24 = (unity_4LightPosX0 - tmpvar_23.x);
  highp vec4 tmpvar_25;
  tmpvar_25 = (unity_4LightPosY0 - tmpvar_23.y);
  highp vec4 tmpvar_26;
  tmpvar_26 = (unity_4LightPosZ0 - tmpvar_23.z);
  highp vec4 tmpvar_27;
  tmpvar_27 = (((tmpvar_24 * tmpvar_24) + (tmpvar_25 * tmpvar_25)) + (tmpvar_26 * tmpvar_26));
  highp vec4 tmpvar_28;
  tmpvar_28 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_24 * tmpvar_7.x) + (tmpvar_25 * tmpvar_7.y)) + (tmpvar_26 * tmpvar_7.z)) * inversesqrt (tmpvar_27))) * (1.0/((1.0 + (tmpvar_27 * unity_4LightAtten0)))));
  highp vec3 tmpvar_29;
  tmpvar_29 = (tmpvar_4 + ((((unity_LightColor[0].xyz * tmpvar_28.x) + (unity_LightColor[1].xyz * tmpvar_28.y)) + (unity_LightColor[2].xyz * tmpvar_28.z)) + (unity_LightColor[3].xyz * tmpvar_28.w)));
  tmpvar_4 = tmpvar_29;
  highp vec4 o_i0_i1;
  highp vec4 tmpvar_30;
  tmpvar_30 = (tmpvar_5 * 0.5);
  o_i0_i1 = tmpvar_30;
  highp vec2 tmpvar_31;
  tmpvar_31.x = tmpvar_30.x;
  tmpvar_31.y = (tmpvar_30.y * _ProjectionParams.x);
  o_i0_i1.xy = (tmpvar_31 + tmpvar_30.w);
  o_i0_i1.zw = tmpvar_5.zw;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_9 * (((_World2Object * tmpvar_11).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_i0_i1;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  lowp float tmpvar_6;
  tmpvar_6 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD4).x;
  highp vec3 tmpvar_7;
  tmpvar_7 = normalize (xlv_TEXCOORD3);
  mediump vec3 lightDir;
  lightDir = xlv_TEXCOORD1;
  mediump vec3 viewDir;
  viewDir = tmpvar_7;
  mediump float atten_i0;
  atten_i0 = tmpvar_6;
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_8;
  tmpvar_8 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir + viewDir))));
  nh = tmpvar_8;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_9;
  tmpvar_9 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_10;
  tmpvar_10 = (pow (nh, 0.0) * tmpvar_9);
  res.w = tmpvar_10;
  mediump vec4 tmpvar_11;
  tmpvar_11 = (res * (atten_i0 * 2.0));
  res = tmpvar_11;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_11.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  mediump vec3 tmpvar_12;
  tmpvar_12 = (c.xyz + (tmpvar_1 * xlv_TEXCOORD2));
  c.xyz = tmpvar_12;
  mediump vec3 tmpvar_13;
  tmpvar_13 = c.xyz;
  c.xyz = tmpvar_13;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
}
 }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardAdd" "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
  ZWrite Off
  Fog {
   Color (0,0,0,0)
  }
  Blend SrcAlpha One
Program "vp" {
SubProgram "gles " {
Keywords { "POINT" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _MainTex;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD1);
  lightDir = tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = vec2(dot (xlv_TEXCOORD3, xlv_TEXCOORD3));
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_LightTexture0, tmpvar_7);
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = tmpvar_8.w;
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_9;
  tmpvar_9 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + normalize (xlv_TEXCOORD2)))));
  nh = tmpvar_9;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_10;
  tmpvar_10 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_11;
  tmpvar_11 = (pow (nh, 0.0) * tmpvar_10);
  res.w = tmpvar_11;
  mediump vec4 tmpvar_12;
  tmpvar_12 = (res * (atten * 2.0));
  res = tmpvar_12;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_12.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _MainTex_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
}



#endif
#ifdef FRAGMENT

varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _MainTex;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  lightDir = xlv_TEXCOORD1;
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_6;
  tmpvar_6 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + normalize (xlv_TEXCOORD2)))));
  nh = tmpvar_6;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_7;
  tmpvar_7 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_8;
  tmpvar_8 = (pow (nh, 0.0) * tmpvar_7);
  res.w = tmpvar_8;
  mediump vec4 tmpvar_9;
  tmpvar_9 = (res * 2.0);
  res = tmpvar_9;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_9.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "SPOT" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _MainTex;
uniform sampler2D _LightTextureB0;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD1);
  lightDir = tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_LightTexture0, ((xlv_TEXCOORD3.xy / xlv_TEXCOORD3.w) + 0.5));
  highp vec3 LightCoord_i0;
  LightCoord_i0 = xlv_TEXCOORD3.xyz;
  highp vec2 tmpvar_8;
  tmpvar_8 = vec2(dot (LightCoord_i0, LightCoord_i0));
  lowp vec4 tmpvar_9;
  tmpvar_9 = texture2D (_LightTextureB0, tmpvar_8);
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = ((float((xlv_TEXCOORD3.z > 0.0)) * tmpvar_7.w) * tmpvar_9.w);
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_10;
  tmpvar_10 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + normalize (xlv_TEXCOORD2)))));
  nh = tmpvar_10;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_11;
  tmpvar_11 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_12;
  tmpvar_12 = (pow (nh, 0.0) * tmpvar_11);
  res.w = tmpvar_12;
  mediump vec4 tmpvar_13;
  tmpvar_13 = (res * (atten * 2.0));
  res = tmpvar_13;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_13.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _MainTex;
uniform sampler2D _LightTextureB0;
uniform samplerCube _LightTexture0;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD1);
  lightDir = tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = vec2(dot (xlv_TEXCOORD3, xlv_TEXCOORD3));
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_LightTextureB0, tmpvar_7);
  lowp vec4 tmpvar_9;
  tmpvar_9 = textureCube (_LightTexture0, xlv_TEXCOORD3);
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = (tmpvar_8.w * tmpvar_9.w);
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_10;
  tmpvar_10 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + normalize (xlv_TEXCOORD2)))));
  nh = tmpvar_10;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_11;
  tmpvar_11 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_12;
  tmpvar_12 = (pow (nh, 0.0) * tmpvar_11);
  res.w = tmpvar_12;
  mediump vec4 tmpvar_13;
  tmpvar_13 = (res * (atten * 2.0));
  res = tmpvar_13;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_13.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xy;
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _MainTex;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  lightDir = xlv_TEXCOORD1;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_LightTexture0, xlv_TEXCOORD3);
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = tmpvar_6.w;
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_7;
  tmpvar_7 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + normalize (xlv_TEXCOORD2)))));
  nh = tmpvar_7;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_8;
  tmpvar_8 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_9;
  tmpvar_9 = (pow (nh, 0.0) * tmpvar_8);
  res.w = tmpvar_9;
  mediump vec4 tmpvar_10;
  tmpvar_10 = (res * (atten * 2.0));
  res = tmpvar_10;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_10.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp mat4 unity_World2Shadow;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex));
  xlv_TEXCOORD4 = (unity_World2Shadow * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _LightTextureB0;
uniform sampler2D _LightTexture0;
uniform highp vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD1);
  lightDir = tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = normalize (xlv_TEXCOORD2);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_LightTexture0, ((xlv_TEXCOORD3.xy / xlv_TEXCOORD3.w) + 0.5));
  highp vec3 LightCoord_i0;
  LightCoord_i0 = xlv_TEXCOORD3.xyz;
  highp vec2 tmpvar_9;
  tmpvar_9 = vec2(dot (LightCoord_i0, LightCoord_i0));
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_LightTextureB0, tmpvar_9);
  lowp float tmpvar_11;
  mediump float shadow;
  lowp vec4 tmpvar_12;
  tmpvar_12 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD4);
  highp float tmpvar_13;
  if ((tmpvar_12.x < (xlv_TEXCOORD4.z / xlv_TEXCOORD4.w))) {
    tmpvar_13 = _LightShadowData.x;
  } else {
    tmpvar_13 = 1.0;
  };
  shadow = tmpvar_13;
  tmpvar_11 = shadow;
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = (((float((xlv_TEXCOORD3.z > 0.0)) * tmpvar_8.w) * tmpvar_10.w) * tmpvar_11);
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_14;
  tmpvar_14 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + tmpvar_7))));
  nh = tmpvar_14;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_15;
  tmpvar_15 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_16;
  tmpvar_16 = (pow (nh, 0.0) * tmpvar_15);
  res.w = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = (res * (atten * 2.0));
  res = tmpvar_17;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_17.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 _MainTex_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_3 = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 1.0;
  tmpvar_9.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_7 * (((_World2Object * tmpvar_9).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_10;
  highp vec4 o_i0_i1;
  highp vec4 tmpvar_11;
  tmpvar_11 = (tmpvar_5 * 0.5);
  o_i0_i1 = tmpvar_11;
  highp vec2 tmpvar_12;
  tmpvar_12.x = tmpvar_11.x;
  tmpvar_12.y = (tmpvar_11.y * _ProjectionParams.x);
  o_i0_i1.xy = (tmpvar_12 + tmpvar_11.w);
  o_i0_i1.zw = tmpvar_5.zw;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = o_i0_i1;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  lightDir = xlv_TEXCOORD1;
  lowp float tmpvar_6;
  tmpvar_6 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD3).x;
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = tmpvar_6;
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_7;
  tmpvar_7 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + normalize (xlv_TEXCOORD2)))));
  nh = tmpvar_7;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_8;
  tmpvar_8 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_9;
  tmpvar_9 = (pow (nh, 0.0) * tmpvar_8);
  res.w = tmpvar_9;
  mediump vec4 tmpvar_10;
  tmpvar_10 = (res * (atten * 2.0));
  res = tmpvar_10;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_10.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (gl_ModelViewProjectionMatrix * _glesVertex);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_3 = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 1.0;
  tmpvar_9.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_7 * (((_World2Object * tmpvar_9).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_10;
  highp vec4 o_i0_i1;
  highp vec4 tmpvar_11;
  tmpvar_11 = (tmpvar_5 * 0.5);
  o_i0_i1 = tmpvar_11;
  highp vec2 tmpvar_12;
  tmpvar_12.x = tmpvar_11.x;
  tmpvar_12.y = (tmpvar_11.y * _ProjectionParams.x);
  o_i0_i1.xy = (tmpvar_12 + tmpvar_11.w);
  o_i0_i1.zw = tmpvar_5.zw;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xy;
  xlv_TEXCOORD4 = o_i0_i1;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  lightDir = xlv_TEXCOORD1;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_LightTexture0, xlv_TEXCOORD3);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD4);
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = (tmpvar_6.w * tmpvar_7.x);
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_8;
  tmpvar_8 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + normalize (xlv_TEXCOORD2)))));
  nh = tmpvar_8;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_9;
  tmpvar_9 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_10;
  tmpvar_10 = (pow (nh, 0.0) * tmpvar_9);
  res.w = tmpvar_10;
  mediump vec4 tmpvar_11;
  tmpvar_11 = (res * (atten * 2.0));
  res = tmpvar_11;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_11.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "POINT" "SHADOWS_CUBE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _LightPositionRange;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
  xlv_TEXCOORD4 = ((_Object2World * _glesVertex).xyz - _LightPositionRange.xyz);
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform samplerCube _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _LightTexture0;
uniform highp vec4 _LightShadowData;
uniform highp vec4 _LightPositionRange;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD1);
  lightDir = tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = normalize (xlv_TEXCOORD2);
  highp vec2 tmpvar_8;
  tmpvar_8 = vec2(dot (xlv_TEXCOORD3, xlv_TEXCOORD3));
  lowp vec4 tmpvar_9;
  tmpvar_9 = texture2D (_LightTexture0, tmpvar_8);
  highp float tmpvar_10;
  tmpvar_10 = ((length (xlv_TEXCOORD4) * _LightPositionRange.w) * 0.97);
  highp vec4 packDist;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_ShadowMapTexture, xlv_TEXCOORD4);
  packDist = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = dot (packDist, vec4(1.0, 0.00392157, 1.53787e-005, 0.0));
  highp float tmpvar_13;
  if ((tmpvar_12 < tmpvar_10)) {
    tmpvar_13 = _LightShadowData.x;
  } else {
    tmpvar_13 = 1.0;
  };
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = (tmpvar_9.w * tmpvar_13);
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_14;
  tmpvar_14 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + tmpvar_7))));
  nh = tmpvar_14;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_15;
  tmpvar_15 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_16;
  tmpvar_16 = (pow (nh, 0.0) * tmpvar_15);
  res.w = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = (res * (atten * 2.0));
  res = tmpvar_17;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_17.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _LightPositionRange;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
  xlv_TEXCOORD4 = ((_Object2World * _glesVertex).xyz - _LightPositionRange.xyz);
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform samplerCube _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _LightTextureB0;
uniform samplerCube _LightTexture0;
uniform highp vec4 _LightShadowData;
uniform highp vec4 _LightPositionRange;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD1);
  lightDir = tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = normalize (xlv_TEXCOORD2);
  highp vec2 tmpvar_8;
  tmpvar_8 = vec2(dot (xlv_TEXCOORD3, xlv_TEXCOORD3));
  lowp vec4 tmpvar_9;
  tmpvar_9 = texture2D (_LightTextureB0, tmpvar_8);
  lowp vec4 tmpvar_10;
  tmpvar_10 = textureCube (_LightTexture0, xlv_TEXCOORD3);
  highp float tmpvar_11;
  tmpvar_11 = ((length (xlv_TEXCOORD4) * _LightPositionRange.w) * 0.97);
  highp vec4 packDist;
  lowp vec4 tmpvar_12;
  tmpvar_12 = textureCube (_ShadowMapTexture, xlv_TEXCOORD4);
  packDist = tmpvar_12;
  highp float tmpvar_13;
  tmpvar_13 = dot (packDist, vec4(1.0, 0.00392157, 1.53787e-005, 0.0));
  highp float tmpvar_14;
  if ((tmpvar_13 < tmpvar_11)) {
    tmpvar_14 = _LightShadowData.x;
  } else {
    tmpvar_14 = 1.0;
  };
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = ((tmpvar_9.w * tmpvar_10.w) * tmpvar_14);
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_15;
  tmpvar_15 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + tmpvar_7))));
  nh = tmpvar_15;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_16;
  tmpvar_16 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_17;
  tmpvar_17 = (pow (nh, 0.0) * tmpvar_16);
  res.w = tmpvar_17;
  mediump vec4 tmpvar_18;
  tmpvar_18 = (res * (atten * 2.0));
  res = tmpvar_18;
  mediump vec4 c_i0;
  c_i0.xyz = (tmpvar_1 * tmpvar_18.xyz);
  c_i0.w = tmpvar_2;
  c = c_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp mat4 unity_World2Shadow;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex));
  xlv_TEXCOORD4 = (unity_World2Shadow * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform highp vec4 _ShadowOffsets[4];
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _LightTextureB0;
uniform sampler2D _LightTexture0;
uniform highp vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD1);
  lightDir = tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = normalize (xlv_TEXCOORD2);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_LightTexture0, ((xlv_TEXCOORD3.xy / xlv_TEXCOORD3.w) + 0.5));
  highp vec3 LightCoord_i0;
  LightCoord_i0 = xlv_TEXCOORD3.xyz;
  highp vec2 tmpvar_9;
  tmpvar_9 = vec2(dot (LightCoord_i0, LightCoord_i0));
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_LightTextureB0, tmpvar_9);
  lowp float tmpvar_11;
  mediump vec4 shadows;
  highp vec4 shadowVals;
  highp vec3 tmpvar_12;
  tmpvar_12 = (xlv_TEXCOORD4.xyz / xlv_TEXCOORD4.w);
  lowp vec4 tmpvar_13;
  tmpvar_13 = texture2D (_ShadowMapTexture, (tmpvar_12.xy + _ShadowOffsets[0].xy));
  shadowVals.x = tmpvar_13.x;
  lowp vec4 tmpvar_14;
  tmpvar_14 = texture2D (_ShadowMapTexture, (tmpvar_12.xy + _ShadowOffsets[1].xy));
  shadowVals.y = tmpvar_14.x;
  lowp vec4 tmpvar_15;
  tmpvar_15 = texture2D (_ShadowMapTexture, (tmpvar_12.xy + _ShadowOffsets[2].xy));
  shadowVals.z = tmpvar_15.x;
  lowp vec4 tmpvar_16;
  tmpvar_16 = texture2D (_ShadowMapTexture, (tmpvar_12.xy + _ShadowOffsets[3].xy));
  shadowVals.w = tmpvar_16.x;
  bvec4 tmpvar_17;
  tmpvar_17 = lessThan (shadowVals, tmpvar_12.zzzz);
  highp float tmpvar_18;
  if (tmpvar_17.x) {
    tmpvar_18 = _LightShadowData.x;
  } else {
    tmpvar_18 = 1.0;
  };
  highp float tmpvar_19;
  if (tmpvar_17.y) {
    tmpvar_19 = _LightShadowData.x;
  } else {
    tmpvar_19 = 1.0;
  };
  highp float tmpvar_20;
  if (tmpvar_17.z) {
    tmpvar_20 = _LightShadowData.x;
  } else {
    tmpvar_20 = 1.0;
  };
  highp float tmpvar_21;
  if (tmpvar_17.w) {
    tmpvar_21 = _LightShadowData.x;
  } else {
    tmpvar_21 = 1.0;
  };
  highp vec4 tmpvar_22;
  tmpvar_22.x = tmpvar_18;
  tmpvar_22.y = tmpvar_19;
  tmpvar_22.z = tmpvar_20;
  tmpvar_22.w = tmpvar_21;
  shadows = tmpvar_22;
  mediump float tmpvar_23;
  tmpvar_23 = dot (shadows, vec4(0.25, 0.25, 0.25, 0.25));
  tmpvar_11 = tmpvar_23;
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = (((float((xlv_TEXCOORD3.z > 0.0)) * tmpvar_8.w) * tmpvar_10.w) * tmpvar_11);
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_24;
  tmpvar_24 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + tmpvar_7))));
  nh = tmpvar_24;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_25;
  tmpvar_25 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_26;
  tmpvar_26 = (pow (nh, 0.0) * tmpvar_25);
  res.w = tmpvar_26;
  mediump vec4 tmpvar_27;
  tmpvar_27 = (res * (atten * 2.0));
  res = tmpvar_27;
  mediump vec4 c_i0_i0;
  c_i0_i0.xyz = (tmpvar_1 * tmpvar_27.xyz);
  c_i0_i0.w = tmpvar_2;
  c = c_i0_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _LightPositionRange;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
  xlv_TEXCOORD4 = ((_Object2World * _glesVertex).xyz - _LightPositionRange.xyz);
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform samplerCube _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _LightTexture0;
uniform highp vec4 _LightShadowData;
uniform highp vec4 _LightPositionRange;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD1);
  lightDir = tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = normalize (xlv_TEXCOORD2);
  highp vec2 tmpvar_8;
  tmpvar_8 = vec2(dot (xlv_TEXCOORD3, xlv_TEXCOORD3));
  lowp vec4 tmpvar_9;
  tmpvar_9 = texture2D (_LightTexture0, tmpvar_8);
  highp float tmpvar_10;
  mediump vec4 shadows;
  highp vec4 shadowVals;
  highp vec3 vec_i0;
  vec_i0 = (xlv_TEXCOORD4 + vec3(0.0078125, 0.0078125, 0.0078125));
  highp vec4 packDist;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_ShadowMapTexture, vec_i0);
  packDist = tmpvar_11;
  shadowVals.x = dot (packDist, vec4(1.0, 0.00392157, 1.53787e-005, 0.0));
  highp vec3 vec_i0_i1;
  vec_i0_i1 = (xlv_TEXCOORD4 + vec3(-0.0078125, -0.0078125, 0.0078125));
  highp vec4 packDist_i0;
  lowp vec4 tmpvar_12;
  tmpvar_12 = textureCube (_ShadowMapTexture, vec_i0_i1);
  packDist_i0 = tmpvar_12;
  shadowVals.y = dot (packDist_i0, vec4(1.0, 0.00392157, 1.53787e-005, 0.0));
  highp vec3 vec_i0_i1_i2;
  vec_i0_i1_i2 = (xlv_TEXCOORD4 + vec3(-0.0078125, 0.0078125, -0.0078125));
  highp vec4 packDist_i0_i1;
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_ShadowMapTexture, vec_i0_i1_i2);
  packDist_i0_i1 = tmpvar_13;
  shadowVals.z = dot (packDist_i0_i1, vec4(1.0, 0.00392157, 1.53787e-005, 0.0));
  highp vec3 vec_i0_i1_i2_i3;
  vec_i0_i1_i2_i3 = (xlv_TEXCOORD4 + vec3(0.0078125, -0.0078125, -0.0078125));
  highp vec4 packDist_i0_i1_i2;
  lowp vec4 tmpvar_14;
  tmpvar_14 = textureCube (_ShadowMapTexture, vec_i0_i1_i2_i3);
  packDist_i0_i1_i2 = tmpvar_14;
  shadowVals.w = dot (packDist_i0_i1_i2, vec4(1.0, 0.00392157, 1.53787e-005, 0.0));
  highp vec4 tmpvar_15;
  tmpvar_15 = vec4(((length (xlv_TEXCOORD4) * _LightPositionRange.w) * 0.97));
  bvec4 tmpvar_16;
  tmpvar_16 = lessThan (shadowVals, tmpvar_15);
  highp float tmpvar_17;
  if (tmpvar_16.x) {
    tmpvar_17 = _LightShadowData.x;
  } else {
    tmpvar_17 = 1.0;
  };
  highp float tmpvar_18;
  if (tmpvar_16.y) {
    tmpvar_18 = _LightShadowData.x;
  } else {
    tmpvar_18 = 1.0;
  };
  highp float tmpvar_19;
  if (tmpvar_16.z) {
    tmpvar_19 = _LightShadowData.x;
  } else {
    tmpvar_19 = 1.0;
  };
  highp float tmpvar_20;
  if (tmpvar_16.w) {
    tmpvar_20 = _LightShadowData.x;
  } else {
    tmpvar_20 = 1.0;
  };
  highp vec4 tmpvar_21;
  tmpvar_21.x = tmpvar_17;
  tmpvar_21.y = tmpvar_18;
  tmpvar_21.z = tmpvar_19;
  tmpvar_21.w = tmpvar_20;
  shadows = tmpvar_21;
  mediump float tmpvar_22;
  tmpvar_22 = dot (shadows, vec4(0.25, 0.25, 0.25, 0.25));
  tmpvar_10 = tmpvar_22;
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = (tmpvar_9.w * tmpvar_10);
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_23;
  tmpvar_23 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + tmpvar_7))));
  nh = tmpvar_23;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_24;
  tmpvar_24 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_25;
  tmpvar_25 = (pow (nh, 0.0) * tmpvar_24);
  res.w = tmpvar_25;
  mediump vec4 tmpvar_26;
  tmpvar_26 = (res * (atten * 2.0));
  res = tmpvar_26;
  mediump vec4 c_i0_i0;
  c_i0_i0.xyz = (tmpvar_1 * tmpvar_26.xyz);
  c_i0_i0.w = tmpvar_2;
  c = c_i0_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _LightPositionRange;
uniform highp mat4 _LightMatrix0;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  highp mat3 tmpvar_5;
  tmpvar_5[0] = tmpvar_1.xyz;
  tmpvar_5[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_5[2] = tmpvar_2;
  mat3 tmpvar_6;
  tmpvar_6[0].x = tmpvar_5[0].x;
  tmpvar_6[0].y = tmpvar_5[1].x;
  tmpvar_6[0].z = tmpvar_5[2].x;
  tmpvar_6[1].x = tmpvar_5[0].y;
  tmpvar_6[1].y = tmpvar_5[1].y;
  tmpvar_6[1].z = tmpvar_5[2].y;
  tmpvar_6[2].x = tmpvar_5[0].z;
  tmpvar_6[2].y = tmpvar_5[1].z;
  tmpvar_6[2].z = tmpvar_5[2].z;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_3 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 * (((_World2Object * tmpvar_8).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_4 = tmpvar_9;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
  xlv_TEXCOORD4 = ((_Object2World * _glesVertex).xyz - _LightPositionRange.xyz);
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD0;
uniform highp vec4 _TintColor;
uniform samplerCube _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _LightTextureB0;
uniform samplerCube _LightTexture0;
uniform highp vec4 _LightShadowData;
uniform highp vec4 _LightPositionRange;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  mediump vec3 tmpvar_1;
  mediump float tmpvar_2;
  highp vec4 Tex2D0;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  Tex2D0 = tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (Tex2D0 * _TintColor).xyz;
  tmpvar_1 = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = Tex2D0.w;
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD1);
  lightDir = tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = normalize (xlv_TEXCOORD2);
  highp vec2 tmpvar_8;
  tmpvar_8 = vec2(dot (xlv_TEXCOORD3, xlv_TEXCOORD3));
  lowp vec4 tmpvar_9;
  tmpvar_9 = texture2D (_LightTextureB0, tmpvar_8);
  lowp vec4 tmpvar_10;
  tmpvar_10 = textureCube (_LightTexture0, xlv_TEXCOORD3);
  highp float tmpvar_11;
  mediump vec4 shadows;
  highp vec4 shadowVals;
  highp vec3 vec_i0;
  vec_i0 = (xlv_TEXCOORD4 + vec3(0.0078125, 0.0078125, 0.0078125));
  highp vec4 packDist;
  lowp vec4 tmpvar_12;
  tmpvar_12 = textureCube (_ShadowMapTexture, vec_i0);
  packDist = tmpvar_12;
  shadowVals.x = dot (packDist, vec4(1.0, 0.00392157, 1.53787e-005, 0.0));
  highp vec3 vec_i0_i1;
  vec_i0_i1 = (xlv_TEXCOORD4 + vec3(-0.0078125, -0.0078125, 0.0078125));
  highp vec4 packDist_i0;
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_ShadowMapTexture, vec_i0_i1);
  packDist_i0 = tmpvar_13;
  shadowVals.y = dot (packDist_i0, vec4(1.0, 0.00392157, 1.53787e-005, 0.0));
  highp vec3 vec_i0_i1_i2;
  vec_i0_i1_i2 = (xlv_TEXCOORD4 + vec3(-0.0078125, 0.0078125, -0.0078125));
  highp vec4 packDist_i0_i1;
  lowp vec4 tmpvar_14;
  tmpvar_14 = textureCube (_ShadowMapTexture, vec_i0_i1_i2);
  packDist_i0_i1 = tmpvar_14;
  shadowVals.z = dot (packDist_i0_i1, vec4(1.0, 0.00392157, 1.53787e-005, 0.0));
  highp vec3 vec_i0_i1_i2_i3;
  vec_i0_i1_i2_i3 = (xlv_TEXCOORD4 + vec3(0.0078125, -0.0078125, -0.0078125));
  highp vec4 packDist_i0_i1_i2;
  lowp vec4 tmpvar_15;
  tmpvar_15 = textureCube (_ShadowMapTexture, vec_i0_i1_i2_i3);
  packDist_i0_i1_i2 = tmpvar_15;
  shadowVals.w = dot (packDist_i0_i1_i2, vec4(1.0, 0.00392157, 1.53787e-005, 0.0));
  highp vec4 tmpvar_16;
  tmpvar_16 = vec4(((length (xlv_TEXCOORD4) * _LightPositionRange.w) * 0.97));
  bvec4 tmpvar_17;
  tmpvar_17 = lessThan (shadowVals, tmpvar_16);
  highp float tmpvar_18;
  if (tmpvar_17.x) {
    tmpvar_18 = _LightShadowData.x;
  } else {
    tmpvar_18 = 1.0;
  };
  highp float tmpvar_19;
  if (tmpvar_17.y) {
    tmpvar_19 = _LightShadowData.x;
  } else {
    tmpvar_19 = 1.0;
  };
  highp float tmpvar_20;
  if (tmpvar_17.z) {
    tmpvar_20 = _LightShadowData.x;
  } else {
    tmpvar_20 = 1.0;
  };
  highp float tmpvar_21;
  if (tmpvar_17.w) {
    tmpvar_21 = _LightShadowData.x;
  } else {
    tmpvar_21 = 1.0;
  };
  highp vec4 tmpvar_22;
  tmpvar_22.x = tmpvar_18;
  tmpvar_22.y = tmpvar_19;
  tmpvar_22.z = tmpvar_20;
  tmpvar_22.w = tmpvar_21;
  shadows = tmpvar_22;
  mediump float tmpvar_23;
  tmpvar_23 = dot (shadows, vec4(0.25, 0.25, 0.25, 0.25));
  tmpvar_11 = tmpvar_23;
  mediump vec3 lightDir_i0;
  lightDir_i0 = lightDir;
  mediump float atten;
  atten = ((tmpvar_9.w * tmpvar_10.w) * tmpvar_11);
  mediump vec4 res;
  highp float nh;
  mediump float tmpvar_24;
  tmpvar_24 = max (0.0, dot (vec3(0.0, 0.0, 1.0), normalize ((lightDir_i0 + tmpvar_7))));
  nh = tmpvar_24;
  res.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_i0, vec3(0.0, 0.0, 1.0))));
  lowp float tmpvar_25;
  tmpvar_25 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_26;
  tmpvar_26 = (pow (nh, 0.0) * tmpvar_25);
  res.w = tmpvar_26;
  mediump vec4 tmpvar_27;
  tmpvar_27 = (res * (atten * 2.0));
  res = tmpvar_27;
  mediump vec4 c_i0_i0;
  c_i0_i0.xyz = (tmpvar_1 * tmpvar_27.xyz);
  c_i0_i0.w = tmpvar_2;
  c = c_i0_i0;
  c.w = tmpvar_2;
  gl_FragData[0] = c;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "POINT" "SHADOWS_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "SPOT" "SHADOWS_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" "SHADOWS_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "SPOT" "SHADOWS_DEPTH" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" "SHADOWS_SCREEN" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "POINT" "SHADOWS_CUBE" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "SPOT" "SHADOWS_DEPTH" "SHADOWS_SOFT" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "POINT" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" "SHADOWS_CUBE" "SHADOWS_SOFT" }
"!!GLES"
}
}
 }
 Pass {
  Name "SHADOWCASTER"
  Tags { "LIGHTMODE"="SHADOWCASTER" "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
  Cull Off
  Fog { Mode Off }
  Offset 1, 1
Program "vp" {
SubProgram "gles " {
Keywords { "SHADOWS_DEPTH" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

uniform highp vec4 unity_LightShadowBias;

attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_2 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_1 = tmpvar_2;
  tmpvar_1.z = (tmpvar_2.z + unity_LightShadowBias.x);
  tmpvar_1.z = mix (tmpvar_1.z, max (tmpvar_1.z, -(tmpvar_2.w)), unity_LightShadowBias.y);
  gl_Position = tmpvar_1;
}



#endif
#ifdef FRAGMENT

void main ()
{
  gl_FragData[0] = vec4(0.0, 0.0, 0.0, 0.0);
}



#endif"
}
SubProgram "gles " {
Keywords { "SHADOWS_CUBE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD0;

uniform highp mat4 _Object2World;
uniform highp vec4 _LightPositionRange;
attribute vec4 _glesVertex;
void main ()
{
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = ((_Object2World * _glesVertex).xyz - _LightPositionRange.xyz);
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD0;
uniform highp vec4 _LightPositionRange;
void main ()
{
  lowp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_2 = fract ((vec4(1.0, 255.0, 65025.0, 160580992.0) * (length (xlv_TEXCOORD0) * _LightPositionRange.w)));
  highp vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 - (tmpvar_2.yzww * 0.00392157));
  tmpvar_1 = tmpvar_3;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "SHADOWS_DEPTH" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "SHADOWS_CUBE" }
"!!GLES"
}
}
 }
 Pass {
  Name "SHADOWCOLLECTOR"
  Tags { "LIGHTMODE"="SHADOWCOLLECTOR" "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
Keywords { "SHADOWS_NONATIVE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;
#define gl_ModelViewMatrix glstate_matrix_modelview0
uniform mat4 glstate_matrix_modelview0;

varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD0;
uniform highp mat4 unity_World2Shadow[4];


uniform highp mat4 _Object2World;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_2 = (_Object2World * _glesVertex);
  tmpvar_1.xyz = tmpvar_2.xyz;
  tmpvar_1.w = -((gl_ModelViewMatrix * _glesVertex).z);
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (unity_World2Shadow[0] * tmpvar_2).xyz;
  xlv_TEXCOORD1 = (unity_World2Shadow[1] * tmpvar_2).xyz;
  xlv_TEXCOORD2 = (unity_World2Shadow[2] * tmpvar_2).xyz;
  xlv_TEXCOORD3 = (unity_World2Shadow[3] * tmpvar_2).xyz;
  xlv_TEXCOORD4 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD0;
uniform sampler2D _ShadowMapTexture;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 _LightSplitsNear;
uniform highp vec4 _LightSplitsFar;
uniform highp vec4 _LightShadowData;
void main ()
{
  lowp vec4 tmpvar_1;
  highp vec4 res;
  highp vec4 zFar;
  highp vec4 zNear;
  highp vec4 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD4.wwww;
  lowp vec4 tmpvar_3;
  tmpvar_3 = vec4(greaterThanEqual (tmpvar_2, _LightSplitsNear));
  zNear = tmpvar_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = vec4(lessThan (tmpvar_2, _LightSplitsFar));
  zFar = tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (zNear * zFar);
  highp float tmpvar_6;
  tmpvar_6 = clamp (((xlv_TEXCOORD4.w * _LightShadowData.z) + _LightShadowData.w), 0.0, 1.0);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = ((((xlv_TEXCOORD0 * tmpvar_5.x) + (xlv_TEXCOORD1 * tmpvar_5.y)) + (xlv_TEXCOORD2 * tmpvar_5.z)) + (xlv_TEXCOORD3 * tmpvar_5.w));
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_ShadowMapTexture, tmpvar_7.xy);
  highp float tmpvar_9;
  if ((tmpvar_8.x < tmpvar_7.z)) {
    tmpvar_9 = _LightShadowData.x;
  } else {
    tmpvar_9 = 1.0;
  };
  res.x = clamp ((tmpvar_9 + tmpvar_6), 0.0, 1.0);
  res.y = 1.0;
  highp vec2 enc;
  highp vec2 tmpvar_10;
  tmpvar_10 = fract ((vec2(1.0, 255.0) * (1.0 - (xlv_TEXCOORD4.w * _ProjectionParams.w))));
  enc = tmpvar_10;
  enc.x = (tmpvar_10.x - (tmpvar_10.y * 0.00392157));
  res.zw = enc;
  tmpvar_1 = res;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "SHADOWS_SPLIT_SPHERES" "SHADOWS_NONATIVE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;
#define gl_ModelViewMatrix glstate_matrix_modelview0
uniform mat4 glstate_matrix_modelview0;

varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD0;
uniform highp mat4 unity_World2Shadow[4];


uniform highp mat4 _Object2World;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_2 = (_Object2World * _glesVertex);
  tmpvar_1.xyz = tmpvar_2.xyz;
  tmpvar_1.w = -((gl_ModelViewMatrix * _glesVertex).z);
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = (unity_World2Shadow[0] * tmpvar_2).xyz;
  xlv_TEXCOORD1 = (unity_World2Shadow[1] * tmpvar_2).xyz;
  xlv_TEXCOORD2 = (unity_World2Shadow[2] * tmpvar_2).xyz;
  xlv_TEXCOORD3 = (unity_World2Shadow[3] * tmpvar_2).xyz;
  xlv_TEXCOORD4 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD0;
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform sampler2D _ShadowMapTexture;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 _LightShadowData;
void main ()
{
  lowp vec4 tmpvar_1;
  highp vec4 res;
  highp vec4 cascadeWeights;
  highp vec3 tmpvar_2;
  tmpvar_2 = (xlv_TEXCOORD4.xyz - unity_ShadowSplitSpheres[0].xyz);
  highp vec3 tmpvar_3;
  tmpvar_3 = (xlv_TEXCOORD4.xyz - unity_ShadowSplitSpheres[1].xyz);
  highp vec3 tmpvar_4;
  tmpvar_4 = (xlv_TEXCOORD4.xyz - unity_ShadowSplitSpheres[2].xyz);
  highp vec3 tmpvar_5;
  tmpvar_5 = (xlv_TEXCOORD4.xyz - unity_ShadowSplitSpheres[3].xyz);
  highp vec4 tmpvar_6;
  tmpvar_6.x = dot (tmpvar_2, tmpvar_2);
  tmpvar_6.y = dot (tmpvar_3, tmpvar_3);
  tmpvar_6.z = dot (tmpvar_4, tmpvar_4);
  tmpvar_6.w = dot (tmpvar_5, tmpvar_5);
  lowp vec4 tmpvar_7;
  tmpvar_7 = vec4(lessThan (tmpvar_6, unity_ShadowSplitSqRadii));
  cascadeWeights = tmpvar_7;
  cascadeWeights.yzw = clamp ((cascadeWeights.yzw - cascadeWeights.xyz), 0.0, 1.0);
  highp float tmpvar_8;
  tmpvar_8 = clamp (((distance (xlv_TEXCOORD4.xyz, unity_ShadowFadeCenterAndType.xyz) * _LightShadowData.z) + _LightShadowData.w), 0.0, 1.0);
  highp vec4 tmpvar_9;
  tmpvar_9.w = 1.0;
  tmpvar_9.xyz = ((((xlv_TEXCOORD0 * cascadeWeights.x) + (xlv_TEXCOORD1 * cascadeWeights.y)) + (xlv_TEXCOORD2 * cascadeWeights.z)) + (xlv_TEXCOORD3 * cascadeWeights.w));
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_ShadowMapTexture, tmpvar_9.xy);
  highp float tmpvar_11;
  if ((tmpvar_10.x < tmpvar_9.z)) {
    tmpvar_11 = _LightShadowData.x;
  } else {
    tmpvar_11 = 1.0;
  };
  res.x = clamp ((tmpvar_11 + tmpvar_8), 0.0, 1.0);
  res.y = 1.0;
  highp vec2 enc;
  highp vec2 tmpvar_12;
  tmpvar_12 = fract ((vec2(1.0, 255.0) * (1.0 - (xlv_TEXCOORD4.w * _ProjectionParams.w))));
  enc = tmpvar_12;
  enc.x = (tmpvar_12.x - (tmpvar_12.y * 0.00392157));
  res.zw = enc;
  tmpvar_1 = res;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "SHADOWS_NONATIVE" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "SHADOWS_SPLIT_SPHERES" "SHADOWS_NONATIVE" }
"!!GLES"
}
}
 }
}
Fallback "Diffuse"
}