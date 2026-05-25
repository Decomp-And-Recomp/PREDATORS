using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletReact : MonoBehaviour
{
	public float explodeSecs = 1f;

	public Animation anim;

	public int plasmaParticleLevel = 1;

	public float explosionRadius = 3.8f;

	public float explosionDamage = 100f;

	public Collider playerCollider;

	private bool exploded;

	public AudioClip soundExplode;

	private Vector3 transformPosition;

	private Transform xForm;

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
		if (!anim)
		{
			Debug.Log("error: no anim set for rocket");
		}
	}

	private void OnEnable()
	{
		exploded = false;
		anim.Play();
		if (explodeSecs > -1f)
		{
			Invoke("DestroyNow", explodeSecs);
		}
		xForm.rotation = Quaternion.identity;
	}

	private void OnCollisionEnter(Collision collision)
	{
		transformPosition = xForm.position;
		if (Utils.SfxOn)
		{
			AudioSource.PlayClipAtPoint(soundExplode, transformPosition);
		}
		Collider[] array = Physics.OverlapSphere(transformPosition, explosionRadius);
		Collider[] array2 = array;
		foreach (Collider collider in array2)
		{
			if (collider != playerCollider)
			{
				Vector3 a = collider.ClosestPointOnBounds(transformPosition);
				float num = Vector3.Distance(a, transformPosition);
				float num2 = 1f - Mathf.Clamp01(num / explosionRadius);
				num2 *= explosionDamage;
				collider.SendMessage("ApplyDamage", new AttackInfo(num2), SendMessageOptions.DontRequireReceiver);
			}
		}
		Transform transform = null;
		switch (plasmaParticleLevel)
		{
		case 1:
			transform = AManager.instance.GetPoolObject(AManager.PoolObjectType.ExplosionPlasmaGunBlue);
			break;
		case 2:
			transform = AManager.instance.GetPoolObject(AManager.PoolObjectType.ExplosionPlasmaGunYellow);
			break;
		case 3:
			transform = AManager.instance.GetPoolObject(AManager.PoolObjectType.ExplosionPlasmaGunRed);
			break;
		}
		if ((bool)transform)
		{
			transform.position = transformPosition;
			transform.rotation = xForm.rotation;
			transform.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
		}
		exploded = true;
		base.gameObject.SetActiveRecursively(false);
	}

	private void DestroyNow()
	{
		if (!exploded)
		{
			transformPosition = xForm.position;
			if (Utils.SfxOn)
			{
				AudioSource.PlayClipAtPoint(soundExplode, transformPosition);
			}
			Collider[] array = Physics.OverlapSphere(transformPosition, explosionRadius);
			Collider[] array2 = array;
			foreach (Collider collider in array2)
			{
				if (collider != playerCollider)
				{
					Vector3 a = collider.ClosestPointOnBounds(transformPosition);
					float num = Vector3.Distance(a, transformPosition);
					float num2 = 1f - Mathf.Clamp01(num / explosionRadius);
					num2 *= explosionDamage;
					collider.SendMessageUpwards("ApplyDamage", new AttackInfo(num2), SendMessageOptions.DontRequireReceiver);
				}
			}
			Transform poolObject = AManager.instance.GetPoolObject(AManager.PoolObjectType.ExplosionPlasmaGunBlue);
			if ((bool)poolObject)
			{
				poolObject.position = transformPosition;
				poolObject.rotation = xForm.rotation;
				poolObject.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
			}
		}
		base.gameObject.SetActiveRecursively(false);
	}
}
