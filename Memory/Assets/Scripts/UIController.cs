using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField] private Text scoreLabel;

	void Start()
	{

	}

	void Update()
	{
		scoreLabel.text = Time.realtimeSinceStartup.ToString();
	}

	public void OnOpenSettings()
	{
		Debug.Log("Open Settings");
	}

	public void OnMouseOver()
	{
		Image sprite = GetComponent<Image>();
		if (sprite != null)
		{
			// sprite.color = Color.cyan;
			sprite.color = Color.red;

		}
		Debug.Log("OnMouseOver");
	}

	public void OnMouseExit()
	{
		Image sprite = GetComponent<Image>();
		if (sprite != null)
		{
			sprite.color = Color.cyan;

		}
		Debug.Log("OnMouseExit");
	}
}
