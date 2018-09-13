using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFallDown : MonoBehaviour
{

    private MeshRenderer _meshRenderer;
    private Color _color;

	// Use this for initialization
	void Start ()
	{
	    _meshRenderer = GetComponent<MeshRenderer>();
	    _color = _meshRenderer.material.color;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _color.a -= Time.deltaTime * 0.75f;
	    _meshRenderer.material.color = _color;

	    if (_color.a < 0)
	    {
            Destroy(this.gameObject);
	    }
	}
}
