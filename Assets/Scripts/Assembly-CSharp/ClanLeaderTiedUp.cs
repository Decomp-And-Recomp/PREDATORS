using System.Collections;
using UnityEngine;

public class ClanLeaderTiedUp : MonoBehaviour
{
	private const string animationDie = "die";

	public float hitPoints;

	public AudioClip soundDie;

	public AudioClip soundHit;

	public AudioClip soundHit2;

	public Material healthBarMaterial;

	private PlayerController playerController;

	private ParticleEmitter particleBloodSpray;

	private Vector3 bloodSprayOffset = new Vector3(0f, 1f, -0.7f);

	private bool dead;

	private Animation anim;

	private Transform xForm;

	private float maxHealth;

	private AttackInfo thisAttackInfo;

	private void Start()
	{
		maxHealth = hitPoints;
		Camera.main.SendMessage("FocusOnSceneElement", base.transform, SendMessageOptions.DontRequireReceiver);
		playerController = (PlayerController)GameObject.FindWithTag("Player").GetComponent(typeof(PlayerController));
		particleBloodSpray = (ParticleEmitter)GameObject.Find("PredatorBloodSpray3OneShot").GetComponent(typeof(ParticleEmitter));
		healthBarMaterial.SetTextureOffset("_MainTex", new Vector2((1f - hitPoints / maxHealth) * 0.5f, 0f));
		xForm = base.transform;
		dead = false;
		AManager.instance.predatorTargets.Add(base.transform);
		anim = base.GetComponent<Animation>();
		anim["idle"].wrapMode = WrapMode.Loop;
		anim["die"].wrapMode = WrapMode.ClampForever;
		anim["idle"].time = 0f;
		anim.CrossFade("idle");
	}

	private void UpdateHealth()
	{
		healthBarMaterial.SetTextureOffset("_MainTex", new Vector2((1f - hitPoints / maxHealth) * 0.5f, 0f));
	}

	private IEnumerator GetHurt()
	{
		if (Random.value < 0.5f)
		{
			if (Utils.SfxOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(soundHit);
			}
		}
		else if (Utils.SfxOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(soundHit2);
		}
		Vector3 hitDirection = (xForm.position - thisAttackInfo.AttackerPosition).normalized;
		if (AManager.BloodOn && (bool)particleBloodSpray)
		{
			particleBloodSpray.transform.position = xForm.TransformPoint(bloodSprayOffset);
			particleBloodSpray.transform.rotation = Quaternion.LookRotation(hitDirection);
			particleBloodSpray.Emit();
		}
		Vector3 clanLeaderRightVector = xForm.TransformDirection(Vector3.right).normalized;
		Vector3 clanLeaderForwardVector = xForm.TransformDirection(Vector3.forward).normalized;
		Vector3 enemyClanLeaderVector = (xForm.position - thisAttackInfo.AttackerPosition).normalized;
		float scalarProductBetweenClanLeaderRightVectorAndEnemyClanLeaderVector = Vector3.Dot(clanLeaderRightVector, enemyClanLeaderVector);
		float scalarProductBetweenClanLeaderForwardVectorAndEnemyClanLeaderVector = Vector3.Dot(clanLeaderForwardVector, enemyClanLeaderVector);
		if (Mathf.Abs(scalarProductBetweenClanLeaderForwardVectorAndEnemyClanLeaderVector) > 0.7f)
		{
			anim.CrossFade("get_hit_back", 0.1f);
		}
		else if (scalarProductBetweenClanLeaderRightVectorAndEnemyClanLeaderVector > 0f)
		{
			anim.CrossFade("get_hit_right", 0.1f);
		}
		else
		{
			anim.CrossFade("get_hit_left", 0.1f);
		}
		yield return new WaitForSeconds(0.2f);
		anim.CrossFade("idle");
	}

	private IEnumerator SprayBlood(Vector3 bloodPosition)
	{
		Transform bloodEffect = AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodSprayPredatorAnimated);
		if ((bool)bloodEffect)
		{
			bloodEffect.position = bloodPosition;
			bloodEffect.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
		}
		Transform bloodSplat = AManager.instance.GetPoolObject(AManager.PoolObjectType.BloodSplatPredator);
		if ((bool)bloodSplat)
		{
			yield return new WaitForSeconds(0.2f);
			bloodSplat.position = new Vector3(bloodPosition.x, 0.01f, bloodPosition.z);
			float randomFloat = Random.Range(0.1f, 0.5f);
			bloodSplat.localScale = new Vector3(randomFloat, randomFloat, randomFloat);
			bloodSplat.localEulerAngles = new Vector3(270f, Random.Range(0f, 360f), 0f);
			bloodSplat.gameObject.active = true;
		}
	}

	private IEnumerator DieAnimation()
	{
		if (Utils.SfxOn)
		{
			AudioSource.PlayClipAtPoint(soundDie, xForm.position);
		}
		anim.Stop();
		anim.Play("die", PlayMode.StopAll);
		Camera.main.SendMessage("FocusOnSceneElement", base.transform, SendMessageOptions.DontRequireReceiver);
		yield return new WaitForSeconds(2f);
		playerController.setMissionFailedToTrue();
	}

	public void ApplyDamage(AttackInfo attackInfo)
	{
		thisAttackInfo = attackInfo;
		if (!dead)
		{
			StopAllCoroutines();
			hitPoints -= attackInfo.Damage;
			if (hitPoints <= 0f)
			{
				dead = true;
				StartCoroutine(DieAnimation());
				AManager.instance.predatorTargets.Remove(base.transform);
			}
			else
			{
				StartCoroutine("GetHurt");
			}
			if (AManager.BloodOn)
			{
				StartCoroutine(SprayBlood(xForm.position + Vector3.up));
			}
		}
		UpdateHealth();
	}
}
