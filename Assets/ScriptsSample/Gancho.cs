using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gancho : MonoBehaviour
{

    public float _velLancar;
    public float _tamanhoCorda;
    public float _forcaCorda;
    public float peso;

    private GameObject _player;
    private Rigidbody _corpoRigido;
    private SpringJoint _efeitoCorda;

    private float _distanciaDoPlayer;

    private bool _atiraCorda;
    private bool _cordaColidiu;

    // Use this for initialization
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _corpoRigido = GetComponent<Rigidbody>();
        _efeitoCorda = _player.GetComponent<SpringJoint>();

        _atiraCorda = true;
        _cordaColidiu = false;
    }

    // Update is called once per frame
    void Update()
    {

        _distanciaDoPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            _atiraCorda = false;
        }

        if (_atiraCorda)
            AtirarGancho();
        else
            RecolherGancho();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            _cordaColidiu = true;
        }
    }

    public void AtirarGancho()
    {
        if (_distanciaDoPlayer <= _tamanhoCorda)
        {
            if (!_cordaColidiu)
            {
                transform.Translate(0, 0, _velLancar * Time.deltaTime);
            }
        }
        else
        {
            _efeitoCorda.connectedBody = _corpoRigido;
            _efeitoCorda.spring = _forcaCorda;
            _efeitoCorda.damper = peso;
        }
        if (_distanciaDoPlayer > _tamanhoCorda)
        {
            _atiraCorda = false;
        }
    }

    public void RecolherGancho()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 25 * Time.deltaTime);

        if (_distanciaDoPlayer <= 2)
        {
            Destroy(gameObject);
        }
    }
}
