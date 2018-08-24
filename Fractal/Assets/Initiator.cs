using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initiator : MonoBehaviour
{
	[SerializeField] private Fractal _baseFractal;

	private Fractal _base;
	
	private void Awake()
	{
		Initialize();
	}

	private void Initialize()
	{
		_base = Instantiate(_baseFractal);
		_base.name = "Fractal";
	}

	private void OnGUI()
	{
		if (GUILayout.Button("RESET", GUILayout.Width(100), GUILayout.Height(100)))
		{
			Destroy(_base.gameObject);
			Initialize();
		}
	}
}
