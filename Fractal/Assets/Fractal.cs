using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{
	[SerializeField] private Mesh[] _meshes;
	[SerializeField] private Material _material;

	[SerializeField] private int _maxDepth;
	[SerializeField] private float _childScale;
	[SerializeField] private float _spawnProbability;

	[SerializeField] private float _maxRotationSpeed;
	[SerializeField] private float _maxTwist;

	private int _depth;
	private float _rotationSpeed;
	private Material[,] _materials;

	private static Vector3[] _childDirections = 
	{
		Vector3.up,
		Vector3.right,
		Vector3.left,
		Vector3.forward,
		Vector3.back
	};

	private static Quaternion[] _childOrientations = 
	{
		Quaternion.identity,
		Quaternion.Euler(0f, 0f, -90f),
		Quaternion.Euler(0f, 0f, 90f),
		Quaternion.Euler(90f, 0f, 0f),
		Quaternion.Euler(-90f, 0f, 0f)
	};
	
	private void Start ()
	{
		_rotationSpeed = Random.Range(-_maxRotationSpeed, _maxRotationSpeed);
		transform.Rotate(Random.Range(-_maxTwist, _maxTwist), 0f, 0f);
		
		if (_materials == null)
		{
			InitializeMaterials();
		}
		
		gameObject.AddComponent<MeshFilter>().mesh = _meshes[Random.Range(0, _meshes.Length)];
		gameObject.AddComponent<MeshRenderer>().material = _materials[_depth, Random.Range(0, 3)];
		
		if (_depth < _maxDepth)
		{
			StartCoroutine(CreateChildren());			
		}
	}
	
	private void Update () 
	{
		transform.Rotate(0f, _rotationSpeed * Time.deltaTime, 0f);
	}

	private void InitializeMaterials () 
	{
		_materials = new Material[_maxDepth + 1, 3];
	
		for (var i = 0; i <= _maxDepth; i++) 
		{
			var t = i / (_maxDepth - 1f);
			t *= t;
			
			_materials[i, 0] = new Material(_material);
			_materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
			
			_materials[i, 1] = new Material(_material);
			_materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
			
			_materials[i, 2] = new Material(_material);
			_materials[i, 2].color = Color.Lerp(Color.white, Color.green, t);
		}
		
		_materials[_maxDepth, 0].color = Color.magenta;
		_materials[_maxDepth, 1].color = Color.red;
		_materials[_maxDepth, 2].color = Color.blue;
	}
	
	private void Initialize(Fractal parent, int childIndex)
	{
		_meshes = parent._meshes;
		_materials = parent._materials;
		_maxDepth = parent._maxDepth;
		_childScale = parent._childScale;
		_depth = parent._depth + 1;
		_spawnProbability = parent._spawnProbability;
		_maxRotationSpeed = parent._maxRotationSpeed;
		_maxTwist = parent._maxTwist;
		
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * _childScale;
		transform.localPosition = _childDirections[childIndex] * (0.5f + 0.5f * _childScale);
		transform.localRotation = _childOrientations[childIndex];
	}

	private IEnumerator CreateChildren()
	{
		for (var i = 0; i < _childDirections.Length; i++)
		{
			if (Random.value < _spawnProbability)
			{
				yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
				new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
			}
		}
	}
}
