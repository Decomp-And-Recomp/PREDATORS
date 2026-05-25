using UnityEngine;

public class EnvironmentSceneLoader : MonoBehaviour
{
	public GameObject environmentThermal;

	public GameObject environmentNormal;

	private PlayerController playerController;

	private void Awake()
	{
		playerController = (PlayerController)GameObject.FindWithTag("Player").GetComponent(typeof(PlayerController));
		if (!playerController)
		{
			Debug.LogError("Player Controller not found in environment loading scene");
		}
		playerController.InitEnvironment(environmentNormal, environmentThermal);
	}
}
