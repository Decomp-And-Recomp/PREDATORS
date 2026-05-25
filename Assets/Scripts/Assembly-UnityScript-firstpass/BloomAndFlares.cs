using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Bloom and Flares")]
[ExecuteInEditMode]
public class BloomAndFlares : PostEffectsBase
{
	public TweakMode tweakMode;

	public string bloomThisTag;

	public float sepBlurSpread;

	public float useSrcAlphaAsMask;

	public float bloomIntensity;

	public float bloomThreshhold;

	public int bloomBlurIterations;

	public bool lensflares;

	public int hollywoodFlareBlurIterations;

	public LensflareStyle lensflareMode;

	public float hollyStretchWidth;

	public float lensflareIntensity;

	public float lensflareThreshhold;

	public Color flareColorA;

	public Color flareColorB;

	public Color flareColorC;

	public Color flareColorD;

	public float blurWidth;

	public Shader addAlphaHackShader;

	private Material _alphaAddMaterial;

	public Shader lensFlareShader;

	private Material _lensFlareMaterial;

	public Shader vignetteShader;

	private Material _vignetteMaterial;

	public Shader separableBlurShader;

	private Material _separableBlurMaterial;

	public Shader addBrightStuffOneOneShader;

	private Material _addBrightStuffBlendOneOneMaterial;

	public Shader hollywoodFlareBlurShader;

	private Material _hollywoodFlareBlurMaterial;

	public Shader hollywoodFlareStretchShader;

	private Material _hollywoodFlareStretchMaterial;

	public Shader brightPassFilterShader;

	private Material _brightPassFilterMaterial;

	public BloomAndFlares()
	{
		tweakMode = TweakMode.Advanced;
		sepBlurSpread = 1.5f;
		useSrcAlphaAsMask = 0.5f;
		bloomIntensity = 1f;
		bloomThreshhold = 0.4f;
		bloomBlurIterations = 3;
		lensflares = true;
		hollywoodFlareBlurIterations = 4;
		hollyStretchWidth = 2.5f;
		lensflareIntensity = 0.75f;
		lensflareThreshhold = 0.5f;
		flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);
		flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);
		flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);
		flareColorD = new Color(0.8f, 0.4f, 0f, 0.75f);
		blurWidth = 1f;
	}

	public virtual void Start()
	{
		CreateMaterials();
		CheckSupport(false);
	}

	public virtual void CreateMaterials()
	{
		_lensFlareMaterial = CheckShaderAndCreateMaterial(lensFlareShader, _lensFlareMaterial);
		_vignetteMaterial = CheckShaderAndCreateMaterial(vignetteShader, _vignetteMaterial);
		_separableBlurMaterial = CheckShaderAndCreateMaterial(separableBlurShader, _separableBlurMaterial);
		_addBrightStuffBlendOneOneMaterial = CheckShaderAndCreateMaterial(addBrightStuffOneOneShader, _addBrightStuffBlendOneOneMaterial);
		_hollywoodFlareBlurMaterial = CheckShaderAndCreateMaterial(hollywoodFlareBlurShader, _hollywoodFlareBlurMaterial);
		_hollywoodFlareStretchMaterial = CheckShaderAndCreateMaterial(hollywoodFlareStretchShader, _hollywoodFlareStretchMaterial);
		_brightPassFilterMaterial = CheckShaderAndCreateMaterial(brightPassFilterShader, _brightPassFilterMaterial);
		_alphaAddMaterial = CheckShaderAndCreateMaterial(addAlphaHackShader, _alphaAddMaterial);
	}

	public override void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		CreateMaterials();
		if (!string.IsNullOrEmpty(bloomThisTag) && bloomThisTag != "Untagged")
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(bloomThisTag);
			int i = 0;
			GameObject[] array2 = array;
			for (int length = array2.Length; i < length; i++)
			{
				if ((bool)(MeshFilter)array2[i].GetComponent(typeof(MeshFilter)))
				{
					Mesh sharedMesh = (((MeshFilter)array2[i].GetComponent(typeof(MeshFilter))) as MeshFilter).sharedMesh;
					_alphaAddMaterial.SetPass(0);
					Graphics.DrawMeshNow(sharedMesh, array2[i].transform.localToWorldMatrix);
				}
			}
		}
		RenderTexture temporary = RenderTexture.GetTemporary((int)((float)source.width / 2f), (int)((float)source.height / 2f), 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)source.width / 4f), (int)((float)source.height / 4f), 0);
		RenderTexture temporary3 = RenderTexture.GetTemporary((int)((float)source.width / 4f), (int)((float)source.height / 4f), 0);
		RenderTexture temporary4 = RenderTexture.GetTemporary((int)((float)source.width / 4f), (int)((float)source.height / 4f), 0);
		Graphics.Blit(source, temporary);
		Graphics.Blit(temporary, temporary2);
		RenderTexture.ReleaseTemporary(temporary);
		_brightPassFilterMaterial.SetVector("threshhold", new Vector4(bloomThreshhold, 1f / (1f - bloomThreshhold), 0f, 0f));
		_brightPassFilterMaterial.SetFloat("useSrcAlphaAsMask", useSrcAlphaAsMask);
		Graphics.Blit(temporary2, temporary3, _brightPassFilterMaterial);
		if (bloomBlurIterations < 1)
		{
			bloomBlurIterations = 1;
		}
		Graphics.Blit(temporary3, temporary2);
		for (int j = 0; j < bloomBlurIterations; j++)
		{
			_separableBlurMaterial.SetVector("offsets", new Vector4(0f, sepBlurSpread * 1f / (float)temporary2.height, 0f, 0f));
			Graphics.Blit(temporary2, temporary4, _separableBlurMaterial);
			_separableBlurMaterial.SetVector("offsets", new Vector4(sepBlurSpread * 1f / (float)temporary2.width, 0f, 0f, 0f));
			Graphics.Blit(temporary4, temporary2, _separableBlurMaterial);
		}
		Graphics.Blit(source, destination);
		if (lensflares)
		{
			_brightPassFilterMaterial.SetVector("threshhold", new Vector4(lensflareThreshhold, 1f / (1f - lensflareThreshhold), 0f, 0f));
			_brightPassFilterMaterial.SetFloat("useSrcAlphaAsMask", 0f);
			Graphics.Blit(temporary3, temporary4, _brightPassFilterMaterial);
			if (lensflareMode == LensflareStyle.Ghosting)
			{
				_separableBlurMaterial.SetVector("offsets", new Vector4(0f, sepBlurSpread * 1f / (float)temporary2.height, 0f, 0f));
				Graphics.Blit(temporary4, temporary3, _separableBlurMaterial);
				_separableBlurMaterial.SetVector("offsets", new Vector4(sepBlurSpread * 1f / (float)temporary2.width, 0f, 0f, 0f));
				Graphics.Blit(temporary3, temporary4, _separableBlurMaterial);
				_vignetteMaterial.SetFloat("vignetteIntensity", 0.975f);
				Graphics.Blit(temporary4, temporary3, _vignetteMaterial);
				_lensFlareMaterial.SetVector("colorA", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * lensflareIntensity);
				_lensFlareMaterial.SetVector("colorB", new Vector4(flareColorB.r, flareColorB.g, flareColorB.b, flareColorB.a) * lensflareIntensity);
				_lensFlareMaterial.SetVector("colorC", new Vector4(flareColorC.r, flareColorC.g, flareColorC.b, flareColorC.a) * lensflareIntensity);
				_lensFlareMaterial.SetVector("colorD", new Vector4(flareColorD.r, flareColorD.g, flareColorD.b, flareColorD.a) * lensflareIntensity);
				Graphics.Blit(temporary3, temporary2, _lensFlareMaterial);
			}
			else
			{
				_hollywoodFlareBlurMaterial.SetVector("offsets", new Vector4(0f, sepBlurSpread * 1f / (float)temporary2.height, 0f, 0f));
				_hollywoodFlareBlurMaterial.SetTexture("_NonBlurredTex", temporary2);
				_hollywoodFlareBlurMaterial.SetVector("tintColor", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * flareColorA.a * lensflareIntensity);
				Graphics.Blit(temporary4, temporary3, _hollywoodFlareBlurMaterial);
				_hollywoodFlareStretchMaterial.SetVector("offsets", new Vector4(sepBlurSpread * 1f / (float)temporary2.width, 0f, 0f, 0f));
				_hollywoodFlareStretchMaterial.SetFloat("stretchWidth", hollyStretchWidth);
				Graphics.Blit(temporary3, temporary4, _hollywoodFlareStretchMaterial);
				if (lensflareMode == LensflareStyle.Hollywood)
				{
					for (int k = 0; k < hollywoodFlareBlurIterations; k++)
					{
						_separableBlurMaterial.SetVector("offsets", new Vector4(sepBlurSpread * 1f / (float)temporary2.width, 0f, 0f, 0f));
						Graphics.Blit(temporary4, temporary3, _separableBlurMaterial);
						_separableBlurMaterial.SetVector("offsets", new Vector4(sepBlurSpread * 1f / (float)temporary2.width, 0f, 0f, 0f));
						Graphics.Blit(temporary3, temporary4, _separableBlurMaterial);
					}
					_addBrightStuffBlendOneOneMaterial.SetFloat("intensity", 1f);
					Graphics.Blit(temporary4, temporary2, _addBrightStuffBlendOneOneMaterial);
				}
				else
				{
					for (int l = 0; l < hollywoodFlareBlurIterations; l++)
					{
						_separableBlurMaterial.SetVector("offsets", new Vector4(sepBlurSpread * 1f / (float)temporary2.width, 0f, 0f, 0f));
						Graphics.Blit(temporary4, temporary3, _separableBlurMaterial);
						_separableBlurMaterial.SetVector("offsets", new Vector4(sepBlurSpread * 1f / (float)temporary2.width, 0f, 0f, 0f));
						Graphics.Blit(temporary3, temporary4, _separableBlurMaterial);
					}
					_vignetteMaterial.SetFloat("vignetteIntensity", 1f);
					Graphics.Blit(temporary4, temporary3, _vignetteMaterial);
					_lensFlareMaterial.SetVector("colorA", new Vector4(flareColorA.r, flareColorA.g, flareColorA.b, flareColorA.a) * flareColorA.a * lensflareIntensity);
					_lensFlareMaterial.SetVector("colorB", new Vector4(flareColorB.r, flareColorB.g, flareColorB.b, flareColorB.a) * flareColorB.a * lensflareIntensity);
					_lensFlareMaterial.SetVector("colorC", new Vector4(flareColorC.r, flareColorC.g, flareColorC.b, flareColorC.a) * flareColorC.a * lensflareIntensity);
					_lensFlareMaterial.SetVector("colorD", new Vector4(flareColorD.r, flareColorD.g, flareColorD.b, flareColorD.a) * flareColorD.a * lensflareIntensity);
					Graphics.Blit(temporary3, temporary4, _lensFlareMaterial);
					_addBrightStuffBlendOneOneMaterial.SetFloat("intensity", 1f);
					Graphics.Blit(temporary4, temporary2, _addBrightStuffBlendOneOneMaterial);
				}
			}
		}
		_addBrightStuffBlendOneOneMaterial.SetFloat("intensity", bloomIntensity);
		Graphics.Blit(temporary2, destination, _addBrightStuffBlendOneOneMaterial);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
		RenderTexture.ReleaseTemporary(temporary4);
	}

	public override void Main()
	{
	}
}
