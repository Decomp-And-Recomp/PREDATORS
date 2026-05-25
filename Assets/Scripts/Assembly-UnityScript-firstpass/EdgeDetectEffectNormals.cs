using System;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Edge Detection (Geometry)")]
public class EdgeDetectEffectNormals : PostEffectsBase
{
	public EdgeDetectMode mode;

	public float sensitivityDepth;

	public float sensitivityNormals;

	public float edgesOnly;

	public Color edgesOnlyBgColor;

	public Shader edgeDetectShader;

	private Material _edgeDetectMaterial;

	public EdgeDetectEffectNormals()
	{
		mode = EdgeDetectMode.Thin;
		sensitivityDepth = 1f;
		sensitivityNormals = 1f;
		edgesOnlyBgColor = Color.white;
	}

	public virtual void CreateMaterials()
	{
		_edgeDetectMaterial = CheckShaderAndCreateMaterial(edgeDetectShader, _edgeDetectMaterial);
	}

	public virtual void Start()
	{
		CreateMaterials();
		CheckSupport(true);
	}

	public virtual void OnEnable()
	{
		GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
	}

	public override void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		CreateMaterials();
		Vector2 vector = default(Vector2);
		vector.x = sensitivityDepth;
		vector.y = sensitivityNormals;
		source.filterMode = FilterMode.Point;
		_edgeDetectMaterial.SetVector("sensitivity", new Vector4(vector.x, vector.y, 1f, vector.y));
		_edgeDetectMaterial.SetFloat("_BgFade", edgesOnly);
		Vector4 vector2 = edgesOnlyBgColor;
		_edgeDetectMaterial.SetVector("_BgColor", vector2);
		if (mode == EdgeDetectMode.Thin)
		{
			Graphics.Blit(source, destination, _edgeDetectMaterial, 0);
		}
		else
		{
			Graphics.Blit(source, destination, _edgeDetectMaterial, 1);
		}
	}

	public override void Main()
	{
	}
}
