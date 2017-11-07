using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Walk,
    Attack,
    Damage
}

public class Enemy : MonoBehaviour
{
    public State _state;

    public GameObject _dropObj;
    public bool _dead;

    public GameObject _Player;

    public float _ViewingDistance;
    public float _ViewingAngle;

    // Use this for initialization
    void Start()
    {
        _state = State.Idle;

        _Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Update_State();
    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.other.tag == "Player")
        {
            _dead = true;
            Change_State(State.Damage);
        }
    }

    private void Update_State()
    {
        switch (_state)
        {
            case State.Idle: Idle(); break;
            case State.Walk: Walk(); break;
            case State.Attack: Attack(); break;
            case State.Damage: Damage(); break;
            default:break;
        }
    }

    private void Change_State(State state)
    {
        _state = state;
    }
    private void Idle()
    {

    }
    
    private void Walk()
    {

    }

    private void Attack()
    {

    }
    private void Damage()
    {
        int itemCount = 1;
        for(int i = 0; i < itemCount; ++i)
        {
            GameObject.Instantiate(_dropObj, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
