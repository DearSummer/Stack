using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    private GameObject _top;
    private Vector3 _birthPos;
    private Vector3 _size;

    private bool _isForward = true;

    public float speed = 5f;
    public float maxDistance = 20f;

    public bool isRight = false;

	// Use this for initialization
	void Start () {
		Initialization();
	}
	
	// Update is called once per frame
	void Update () {
		CubeMove();
	}

    private void Initialization()
    {
        _top = GameObject.Find("Top");
        var pos = _top.transform.position;

        transform.localScale = _top.transform.localScale;
        _size = transform.localScale;

        transform.position = !isRight
            ? new Vector3(pos.x, pos.y + _size.y, pos.z - 10)
            : new Vector3(pos.x - 10, pos.y + _size.y, pos.z);

        _birthPos = transform.position;
    }


    private void CubeMove()
    {
        if (!isRight)
        {
            if (_isForward)
            {

                this.transform.Translate(0, 0, Time.deltaTime * speed);
                if (Mathf.Abs(transform.position.z - _birthPos.z) > maxDistance)
                {
                    _isForward = !_isForward;
                }

            }
            else
            {
                this.transform.Translate(0, 0, -Time.deltaTime * speed);
                if (transform.position.z <= _birthPos.z)
                {
                    _isForward = !_isForward;
                }
            }
        }
        else
        {
            if (_isForward)
            {

                this.transform.Translate(Time.deltaTime * speed, 0, 0);
                if (Mathf.Abs(transform.position.x - _birthPos.x) > maxDistance)
                {
                    _isForward = !_isForward;
                }

            }
            else
            {
                this.transform.Translate(-Time.deltaTime * speed, 0, 0);
                if (transform.position.x <= _birthPos.x)
                {
                    _isForward = !_isForward;
                }
            }
        }
    }

}
