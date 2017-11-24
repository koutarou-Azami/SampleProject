using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liner : MonoBehaviour
{

    [SerializeField, Range(0, 10)]
    private float time = 1;
    [SerializeField]
    private Vector3 _endPosition;
    [SerializeField]
    private AnimationCurve _curve;

    private float _startTime;
    private Vector3 _startPosition;

    public GameObject _player;

    public float _radius;
    // Use this for initialization
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (time <= 0)
        {
            _player.transform.position = _endPosition;
            enabled = false;
            return;
        }

        _startTime = Time.timeSinceLevelLoad;
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ItemMove();
        }
    }

    private void ItemMove()
    {
        var diff = Time.timeSinceLevelLoad - _startTime;
        if (diff > time)
        {
            transform.position = _endPosition;
        }

        var rate = diff / time;
        var pos = _curve.Evaluate(rate);

        //transform.position = Vector3.Lerp(_startPosition, _endPosition, rate);
        transform.position = Vector3.Lerp(_startPosition, _endPosition, pos);
    }
}
