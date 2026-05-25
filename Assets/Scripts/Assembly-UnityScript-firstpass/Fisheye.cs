using System;
using UnityEngine;

[Serializable]
[AddComponentMenu("Image Effects/Fisheye")]
[ExecuteInEditMode]
public class Fisheye : PostEffectsBase
{
	public float strengthX;

	public float strengthY;

	public Shader fishEyeShader;

	private Material _fisheyeMaterial;

	public Fisheye()
	{
		strengthX = 0.05f;
		strengthY = 0.05f;
	}

	public virtual void CreateMaterials()
	{
		_fisheyeMaterial = CheckShaderAndCreateMaterial(fishEyeShader, _fisheyeMaterial);
	}

	public virtual void Start()
	{
		CreateMaterials();
		CheckSupport(false);
	}

	public override void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		CreateMaterials();
		float num = (float)source.width * 1f / ((float)source.height * 1f);
		_fisheyeMaterial.SetVector("intensity", new Vector4(strengthX * num, strengthY * num, strengthX * num, strengthY * num));
		Graphics.Blit(source, destination, _fisheyeMaterial);
	}

	public override void Main()
	{
	}
}
