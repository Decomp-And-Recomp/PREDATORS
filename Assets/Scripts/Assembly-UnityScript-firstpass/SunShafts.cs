using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Sun Shafts")]
[ExecuteInEditMode]
public class SunShafts : PostEffectsBase
{
	public SunShaftsResolution resolution;

	public Transform sunTransform;

	public int radialBlurIterations;

	public Color sunColor;

	public float sunShaftBlurRadius;

	public float sunShaftIntensity;

	public float useSkyBoxAlpha;

	public float maxRadius;

	public bool useDepthTexture;

	public Shader clearShader;

	private Material _clearMaterial;

	public Shader depthDecodeShader;

	private Material _encodeDepthRGBA8Material;

	public Shader depthBlurShader;

	private Material _radialDepthBlurMaterial;

	public Shader sunShaftsShader;

	private Material _sunShaftsMaterial;

	public Shader simpleClearShader;

	private Material _simpleClearMaterial;

	public Shader compShader;

	private Material _compMaterial;

	public SunShafts()
	{
		radialBlurIterations = 2;
		sunColor = Color.white;
		sunShaftBlurRadius = 0.0164f;
		sunShaftIntensity = 1.25f;
		useSkyBoxAlpha = 0.75f;
		maxRadius = 1.25f;
		useDepthTexture = true;
	}

	public virtual void CreateMaterials()
	{
		_clearMaterial = CheckShaderAndCreateMaterial(clearShader, _clearMaterial);
		_sunShaftsMaterial = CheckShaderAndCreateMaterial(sunShaftsShader, _sunShaftsMaterial);
		_encodeDepthRGBA8Material = CheckShaderAndCreateMaterial(depthDecodeShader, _encodeDepthRGBA8Material);
		_radialDepthBlurMaterial = CheckShaderAndCreateMaterial(depthBlurShader, _radialDepthBlurMaterial);
		_simpleClearMaterial = CheckShaderAndCreateMaterial(simpleClearShader, _simpleClearMaterial);
		_compMaterial = CheckShaderAndCreateMaterial(compShader, _compMaterial);
	}

	public virtual void Start()
	{
		CreateMaterials();
		CheckSupport(useDepthTexture);
		if (useDepthTexture)
		{
			GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
		}
	}

	public override void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		CreateMaterials();
		float num = 4f;
		if (resolution == SunShaftsResolution.Normal)
		{
			num = 2f;
		}
		if (resolution == SunShaftsResolution.High)
		{
			num = 1f;
		}
		RenderTexture temporary = RenderTexture.GetTemporary((int)((float)source.width / num), (int)((float)source.height / num), 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)source.width / num), (int)((float)source.height / num), 0);
		Graphics.Blit(source, destination);
		if (!useDepthTexture)
		{
			RenderTexture renderTexture = (RenderTexture.active = RenderTexture.GetTemporary(source.width, source.height, 0));
			GL.ClearWithSkybox(false, GetComponent<Camera>());
			_compMaterial.SetTexture("_Skybox", renderTexture);
			Graphics.Blit(source, source, _compMaterial);
			RenderTexture.ReleaseTemporary(renderTexture);
		}
		else
		{
			Graphics.Blit(source, source, _clearMaterial);
		}
		_encodeDepthRGBA8Material.SetFloat("noSkyBoxMask", 1f - useSkyBoxAlpha);
		_encodeDepthRGBA8Material.SetFloat("dontUseSkyboxBrightness", 0f);
		Graphics.Blit(source, temporary2, _encodeDepthRGBA8Material);
		DrawBorder(temporary2, _simpleClearMaterial);
		Vector3 vector = Vector3.one * 0.5f;
		vector = ((!sunTransform) ? new Vector3(0.5f, 0.5f, 0f) : GetComponent<Camera>().WorldToViewportPoint(sunTransform.position));
		_radialDepthBlurMaterial.SetVector("blurRadius4", new Vector4(1f, 1f, 0f, 0f) * sunShaftBlurRadius);
		_radialDepthBlurMaterial.SetVector("sunPosition", new Vector4(vector.x, vector.y, vector.z, maxRadius));
		if (radialBlurIterations < 1)
		{
			radialBlurIterations = 1;
		}
		for (int i = 0; i < radialBlurIterations; i++)
		{
			Graphics.Blit(temporary2, temporary, _radialDepthBlurMaterial);
			Graphics.Blit(temporary, temporary2, _radialDepthBlurMaterial);
		}
		_sunShaftsMaterial.SetFloat("sunShaftIntensity", sunShaftIntensity);
		if (!(vector.z < 0f))
		{
			_sunShaftsMaterial.SetVector("sunColor", new Vector4(sunColor.r, sunColor.g, sunColor.b, sunColor.a));
		}
		else
		{
			_sunShaftsMaterial.SetVector("sunColor", new Vector4(0f, 0f, 0f, 0f));
		}
		_sunShaftsMaterial.SetTexture("_ColorBuffer", source);
		Graphics.Blit(temporary2, destination, _sunShaftsMaterial);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary);
	}

	public override void Main()
	{
	}
}
