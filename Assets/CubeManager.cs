using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject moveCube;
    public GameObject topCube;
    public GameObject fallDownCube;

    private GameObject _top;
    private Vector3 _size;

    private GameObject _moveCube;

    private bool _isRight = false;

    private float[] _colorArray = {1, 1, 1};
    private int[] _rgbIndex = {0, 1, 2};
    private bool _reduceColor = true;

    // Use this for initialization
    void Start()
    {
        Initialization();
        InitializationColor();
    }

    private void Update()
    {
        if (_moveCube == null)
            return;
        

        if (Input.anyKeyDown)
        {
            FallDown();
        }
    }

    private void SetCubeColor()
    {
        if (_reduceColor)
        {
            if (_colorArray[_rgbIndex[0]] > 0f)
            {
                _colorArray[_rgbIndex[0]] -= 0.1f;
            }
            else if (_colorArray[_rgbIndex[1]] > 0f)
            {
                _colorArray[_rgbIndex[1]] -= 0.1f;
            }
            else if (_colorArray[_rgbIndex[2]] > 0f)
            {
                _colorArray[_rgbIndex[2]] -= 0.1f;
                _reduceColor = !_reduceColor;
            }
        }
        else
        {
            if (_colorArray[_rgbIndex[0]] < 1f)
            {
                _colorArray[_rgbIndex[0]] += 0.1f;
            }
            else if (_colorArray[_rgbIndex[1]] < 1f)
            {
                _colorArray[_rgbIndex[1]] += 0.1f;
            }
            else if (_colorArray[_rgbIndex[2]] < 1f)
            {
                _colorArray[_rgbIndex[2]] += 0.1f;
                _reduceColor = !_reduceColor;
            }
        }
    }

    private void InitializationColor()
    {
        List<int> indexList = new List<int>();
        for (int i = 0; i < _rgbIndex.Length; i++)
        {
            indexList.Add(i);
        }

        for (int i = _rgbIndex.Length - 1; i >= 0; i--)
        {
            int index = Random.Range(0, i);
            _rgbIndex[i] = indexList[index];
            indexList.RemoveAt(index);
        }
    }

    private void Initialization()
    {
        SetCubeColor();
        _moveCube = GameObject.Instantiate(moveCube);

        _top = GameObject.Find("Top");
        _size = _top.transform.localScale;

        if (_isRight)
        {
            _moveCube.GetComponent<MoveCube>().isRight = _isRight;
        }

        _moveCube.GetComponent<MeshRenderer>().material.color =
            new Color(_colorArray[0], _colorArray[1], _colorArray[2]);
    }

    private void FallDown()
    {
        //若方块偏移量大于方块本身,则说明方块无法搭上去
        if (Mathf.Abs(_moveCube.transform.position.z - _top.transform.position.z) > _size.z ||
            Mathf.Abs(_moveCube.transform.position.x - _top.transform.position.x) >_size.x)
        {
            // TODO: 游戏结束
            BuildStackFail();
        }
        else
        {
            // TODO : 将方块搭上去
            CreateCubeAndFallDown();
            Destroy(_moveCube);
            Initialization();
        }
    }

    private void CreateCubeAndFallDown()
    {
        var topPos = _top.transform.position;
        var moveCubePos = _moveCube.transform.position;
       
        //创建一个顶层的Cube
        GameObject newTop = GameObject.Instantiate(topCube);
        newTop.name = _top.name;

        _top.name = _top.name + "_old";
        //创建边角料
        GameObject offcut = GameObject.Instantiate(fallDownCube);

        float zOffset = Mathf.Abs(topPos.z - moveCubePos.z);
        float xOffset = Mathf.Abs(topPos.x - moveCubePos.x);


        if (!_isRight)
        {

            newTop.transform.localScale =
                new Vector3(Mathf.Abs(_size.x - xOffset), _size.y, Mathf.Abs(_size.z - zOffset));
            offcut.transform.localScale = new Vector3(_size.x, _size.y, zOffset);

            if (moveCubePos.z < topPos.z)
            {
                newTop.transform.position = new Vector3(topPos.x, topPos.y + _size.y, topPos.z - zOffset / 2);
                offcut.transform.position =
                    new Vector3(topPos.x, topPos.y + _size.y, topPos.z - (_size.z / 2 + zOffset / 2));
            }
            else
            {
                newTop.transform.position = new Vector3(topPos.x, topPos.y + _size.y, topPos.z + zOffset / 2);
                offcut.transform.position =
                    new Vector3(topPos.x, topPos.y + _size.y, topPos.z + (_size.z / 2 + zOffset / 2));
            }
        }
        else
        {
            newTop.transform.localScale =
                new Vector3(Mathf.Abs(_size.x - xOffset), _size.y, Mathf.Abs(_size.z - zOffset));
            offcut.transform.localScale = new Vector3(xOffset, _size.y, _size.z);

            if (moveCubePos.x < topPos.x)
            {
                newTop.transform.position = new Vector3(topPos.x - xOffset / 2, topPos.y + _size.y, topPos.z);
                offcut.transform.position =
                    new Vector3(topPos.x - (_size.x / 2 + xOffset / 2), topPos.y + _size.y, topPos.z);
            }
            else
            {
                newTop.transform.position = new Vector3(topPos.x + xOffset / 2, topPos.y + _size.y, topPos.z);
                offcut.transform.position =
                    new Vector3(topPos.x + (_size.x / 2 + xOffset / 2), topPos.y + _size.y, topPos.z);
            }
        }

        newTop.GetComponent<MeshRenderer>().material.color = new Color(_colorArray[0], _colorArray[1], _colorArray[2]);
        offcut.GetComponent<MeshRenderer>().material.color = new Color(_colorArray[0], _colorArray[1], _colorArray[2]);

        _isRight = !_isRight;
    }

    private void BuildStackFail()
    {
        GameObject offcut = GameObject.Instantiate(fallDownCube);
        offcut.transform.position = _moveCube.transform.position;
        offcut.transform.localScale = _moveCube.transform.localScale;
        Destroy(_moveCube);
        _moveCube = null;
    }
}
