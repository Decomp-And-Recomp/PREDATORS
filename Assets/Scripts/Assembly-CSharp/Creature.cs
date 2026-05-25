using UnityEngine;

public class Creature : MonoBehaviour
{
	public float hitPoints;

	protected Transform xForm;

	protected CharacterController characterController;

	protected bool isDead;

	protected virtual void Awake()
	{
		xForm = base.transform;
		characterController = GetComponent<CharacterController>();
	}
}
