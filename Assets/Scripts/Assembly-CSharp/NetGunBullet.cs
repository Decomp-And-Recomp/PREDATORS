using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NetGunBullet : MonoBehaviour
{
	public float explodeSecs = 1f;

	public Collider playerCollider;

	public AudioClip soundImpact;

	private Vector3 transformPosition;

	private Transform xForm;

	private Transform netOnEnemy;

	private void Awake()
	{
		xForm = base.transform;
	}

	private void Start()
	{
		if (!playerCollider)
		{
			playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
		}
	}

	private void OnEnable()
	{
		if (explodeSecs > -1f)
		{
			Invoke("DestroyNow", explodeSecs);
		}
		xForm.rotation = Quaternion.identity;
	}

	private void OnCollisionEnter(Collision collision)
	{
		transformPosition = xForm.position;
		if (collision.gameObject.layer == 11)
		{
			if (Utils.SfxOn)
			{
				AudioSource.PlayClipAtPoint(soundImpact, transformPosition);
			}
			collision.transform.SendMessage("NetGunCaptured", SendMessageOptions.DontRequireReceiver);
			base.gameObject.SetActiveRecursively(false);
		}
		else
		{
			base.gameObject.SetActiveRecursively(false);
		}
	}

	private void DestroyNow()
	{
		base.gameObject.SetActiveRecursively(false);
	}
}
