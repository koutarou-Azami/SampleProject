using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hool : MonoBehaviour {

    public Transform _cam;
    private RaycastHit _hit;
    private Rigidbody _rb;
    private bool _attached;
    public float _momentum;
    public float _speed;
    public float _step;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_cam.position, _cam.forward, out _hit))
            {
                _attached = true;
                _rb.isKinematic = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _attached = false;
            _rb.isKinematic = false;
            _rb.velocity = transform.forward * _momentum;
        }

        if (_attached)
        {
            _momentum += Time.deltaTime * _speed;
            _step = _momentum * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _hit.point, _step);
        }

        if (!_attached && _momentum >= 0)
        {
            _momentum += Time.deltaTime * 5;
            _step = 0;
        }
	}
}
