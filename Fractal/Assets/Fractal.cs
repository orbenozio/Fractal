using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{
	[SerializeField] private Mesh _mesh;
	[SerializeField] private Material _material;

	[SerializeField] private int _maxDepth;
	[SerializeField] private float _childScale;
	private int _depth;
	
	private void Start ()
	{
		gameObject.AddComponent<MeshFilter>().mesh = _mesh;
		gameObject.AddComponent<MeshRenderer>().material = _material;

		if (_depth < _maxDepth)
		{
			StartCoroutine(CreateChildren());			
		}
	}

	private void Initialize(Fractal parent, Vector3 direction, Quaternion orientation)
	{
		_mesh = parent._mesh;
		_material = parent._material;
		_maxDepth = parent._maxDepth;
		_childScale = parent._childScale;
		_depth = parent._depth + 1;
		
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * _childScale;
		transform.localPosition = direction * (0.5f + 0.5f * _childScale);
		transform.localRotation = orientation;
	}

	private IEnumerator CreateChildren()
	{
		yield return new WaitForSeconds(0.5f);
		new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.up, Quaternion.identity);
		yield return new WaitForSeconds(0.5f);
		new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.right, Quaternion.Euler(0f, 0f, -90f));
		yield return new WaitForSeconds(0.5f);
		new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.left, Quaternion.Euler(0f, 0f, 90f));
	}
}
