using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initiator : MonoBehaviour
{
	[SerializeField] private bool _useCLC;
	[SerializeField] private Fractal _baseFractal;
	[SerializeField] private Fractal_CLC _baseFractalCLC;

	private GameObject _base;
	
	private void Awake()
	{
		Initialize();
	}

	private void Initialize()
	{
		if (_useCLC)
		{
			_base = Instantiate(_baseFractalCLC).gameObject;	
		}
		else
		{
			_base = Instantiate(_baseFractal).gameObject;			
		}
		_base.name = "Fractal";
	}

	private void OnGUI()
	{
		if (GUILayout.Button("RESET", GUILayout.Width(100), GUILayout.Height(100)))
		{
			Destroy(_base);
			Initialize();
		}
	}
}
