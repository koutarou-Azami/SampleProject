using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour {
    public GameObject _gancho;
    private GameObject _auxGancho;

    public Camera _camera;

    public Transform _dirDoClique;
    private Transform _auxDirDoClique;

    private Vector3 _localDoCliqu;
    private Vector3 _posMouse;
    private Quaternion _olharParaDir;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _posMouse = Input.mousePosition;
        _posMouse.z = Vector3.Distance(_camera.transform.position, transform.position);
        _posMouse = _camera.ScreenToWorldPoint(_posMouse);

        if (_auxGancho == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _auxDirDoClique = Instantiate(_dirDoClique, _posMouse, Quaternion.identity) as Transform;
                _localDoCliqu = (_auxDirDoClique.transform.position - transform.position).normalized;
                _olharParaDir = Quaternion.LookRotation(_localDoCliqu);

                _auxGancho = Instantiate(_gancho, transform.position, _olharParaDir) as GameObject;
                Destroy(_auxDirDoClique.gameObject);
            }
        }
	}
}
