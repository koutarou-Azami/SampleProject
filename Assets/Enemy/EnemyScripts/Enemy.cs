using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    Idle,
    Walk,
    Chasing,
    ChasingButLosed,
    Attack,
    Damage
}

public class Enemy : MonoBehaviour
{
    public State _state;

    public GameObject[] _dropObj;

    public int _hp;
    // 消滅時のエフェクト
    public GameObject _explosion;
    // Use this for initialization
    void Start()
    {
        _state = State.Idle;
    }
    // Update is called once per frame
    void Update()
    {
        Update_State();
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _hp--;
            Change_State(State.Damage);
        }
    }
    private void Update_State()
    {
        switch (_state)
        {
            case State.Idle: Idle(); break;
            case State.Walk: Walk(); break;
            case State.Chasing: Chasing(); break;
            case State.ChasingButLosed: ChasingButLosed(); break;
            case State.Attack: Attack(); break;
            case State.Damage: Damage(); break;
            default: break;
        }
    }
    private void Change_State(State state)
    {
        _state = state;
    }
    private void Idle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Change_State(State.Walk);
    }
    private void Walk()
    {
        
    }
    private void Chasing()
    {
    }
    private void ChasingButLosed()
    {

    }
    private void Attack()
    {

    }
    private void Damage()
    {
        Debug.Log("Damage呼ばれている");
        if (_hp <= 0)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            int itemCount = 1;
            for (int i = 0; i < itemCount; ++i)
            {
                GameObject element = _dropObj[Random.Range(0, _dropObj.Length)];
                Instantiate(element, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        Change_State(State.Idle);
    }
}