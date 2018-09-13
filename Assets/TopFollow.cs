using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopFollow : MonoBehaviour
{

    private Vector3 _offset;


    private void Start()
    {
        var top = GameObject.Find("Top");
        _offset = this.transform.position - top.transform.position;

    }


    // Update is called once per frame
	void Update ()
	{
	    var top = GameObject.Find("Top");
	    var targetPos = new Vector3(0, top.transform.position.y, 0) +
	                    _offset *
	                    (top.transform.localScale.sqrMagnitude > 1f ? 1f : top.transform.localScale.magnitude);

	    this.transform.position = Vector3.Lerp(this.transform.position, targetPos, 0.1f);	        
        //this.transform.LookAt(top.transform);
	}
}
