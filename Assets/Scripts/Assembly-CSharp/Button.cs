using System;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
	public TextMesh buttonText;

	public Rect rectangle;

	public AudioSource audioSource;

	public Collider theCollider;

	public Camera theCamera;

	public AudioClip soundButtonClick;

	public KeyCode pollForKey;

	public List<KeyCode> pollForKeys;

	public event EventHandler onPressBegin;

	public event EventHandler onPressEnd;

	private void Update()
	{
		bool flag = false;
		if (pollForKey != 0 && Input.GetKeyDown(pollForKey))
		{
			flag = true;
			ButtonClicked();
		}
		foreach (KeyCode pollForKey in pollForKeys)
		{
			if (Input.GetKeyDown(pollForKey))
			{
				flag = true;
				ButtonClicked();
				break;
			}
		}
		if (flag || InputGUI.touchCount <= 0)
		{
			return;
		}
		TouchGUI touch = InputGUI.GetTouch(0);
		Vector3 vector = touch.position;
		if (touch.phase == TouchPhase.Began)
		{
			if (theCollider != null)
			{
				Collider hitCollider = InputGUI.GetHitCollider(vector, theCamera);
				if (theCollider == hitCollider)
				{
					ButtonClicked();
				}
			}
			else if (rectangle.Contains(vector))
			{
				ButtonClicked();
			}
		}
		if (touch.phase != TouchPhase.Ended)
		{
			return;
		}
		if (theCollider != null)
		{
			Collider hitCollider2 = InputGUI.GetHitCollider(vector, theCamera);
			if (theCollider == hitCollider2)
			{
				ButtonClickedEnd();
			}
		}
		else if (rectangle.Contains(vector))
		{
			ButtonClickedEnd();
		}
	}

	private void ButtonClickedEnd()
	{
		if (this.onPressEnd != null)
		{
			if (GameConstants.sfxOn)
			{
				audioSource.PlayOneShot(soundButtonClick);
			}
			this.onPressEnd(this, EventArgs.Empty);
		}
	}

	private void ButtonClicked()
	{
		if (this.onPressBegin != null)
		{
			if (GameConstants.sfxOn)
			{
				audioSource.PlayOneShot(soundButtonClick);
			}
			this.onPressBegin(this, EventArgs.Empty);
		}
	}

	protected virtual void Awake()
	{
	}
}
