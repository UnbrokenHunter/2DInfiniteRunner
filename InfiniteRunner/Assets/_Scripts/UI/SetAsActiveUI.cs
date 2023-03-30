using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetAsActiveUI : MonoBehaviour
{
	[SerializeField] private bool _setOnEnable = false;

	private void OnEnable()
	{
		if (_setOnEnable)
			EventSystem.current.SetSelectedGameObject(gameObject);
	}
}
