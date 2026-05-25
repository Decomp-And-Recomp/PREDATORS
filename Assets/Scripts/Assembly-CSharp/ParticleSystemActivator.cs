using UnityEngine;

public class ParticleSystemActivator : MonoBehaviour
{
	public ParticleEmitter particleEmitter1;

	public ParticleEmitter particleEmitter2;

	public ParticleEmitter particleEmitter3;

	public ParticleEmitter particleEmitter4;

	public bool emmiter1OneShot = true;

	public void Activate()
	{
		if ((bool)particleEmitter1)
		{
			if (emmiter1OneShot)
			{
				particleEmitter1.Emit();
			}
			else
			{
				particleEmitter1.emit = true;
			}
		}
		if ((bool)particleEmitter2)
		{
			particleEmitter2.Emit();
		}
		if ((bool)particleEmitter3)
		{
			particleEmitter3.Emit();
		}
		if ((bool)particleEmitter4)
		{
			particleEmitter4.Emit();
		}
	}

	public void Deactivate()
	{
		if ((bool)particleEmitter1)
		{
			particleEmitter1.emit = false;
		}
		if ((bool)particleEmitter2)
		{
			particleEmitter2.emit = false;
		}
		if ((bool)particleEmitter3)
		{
			particleEmitter3.emit = false;
		}
		if ((bool)particleEmitter4)
		{
			particleEmitter4.emit = false;
		}
	}
}
