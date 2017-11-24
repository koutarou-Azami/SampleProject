using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Type03State
{
    Idle,
    Walk,
    Chasing,
    ChasingButLosed,
    Attack,
    Damage
}

public class EnemyType03 : MonoBehaviour
{
    public Type03State _state;

    public GameObject[] _dropObj;

    public int _hp;
    // 消滅時のエフェクト
    public GameObject _explosion;

    public EnemyPatroll _patrol;
    // Use this for initialization
    void Start()
    {
        _state = Type03State.Idle;

        _patrol = GameObject.FindGameObjectWithTag("EnemyPatrol").GetComponent<EnemyPatroll>();
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
            Change_State(Type03State.Damage);
        }
    }
    private void Update_State()
    {
        switch (_state)
        {
            case Type03State.Idle: Idle(); break;
            case Type03State.Walk: Walk(); break;
            case Type03State.Chasing: Chasing(); break;
            case Type03State.ChasingButLosed: ChasingButLosed(); break;
            case Type03State.Attack: Attack(); break;
            case Type03State.Damage: Damage(); break;
            default: break;
        }
    }
    public void Change_State(Type03State state)
    {
        _state = state;
    }
    private void Idle()
    {
        // 目的地に到着したら、次の巡回ポイントを目的地に設定する
        if (_patrol.HasArrived())
        {
            Change_State(Type03State.Walk);
        }
        Change_State(Type03State.Idle);
    }
    private void Walk()
    {
        if (_patrol.CanSeePlayer())
        {
            // 追跡状態に状態変更
            Change_State(Type03State.Chasing);
            _patrol._agent.destination = _patrol._player.transform.position;
        }
        // プレイヤーが見えなくて、目的地に到着したとき
        else if (_patrol.HasArrived())
        {
            // 目的地を次の巡回ポイントに切り替える
            _patrol.SetNewPatrolPointIndex();
        }
    }
    private void Chasing()
    {
        // プレイヤーが見えている場合
        if (_patrol.CanSeePlayer())
        {
            // プレイヤーの場所へ向かう
            _patrol._agent.destination = _patrol._player.transform.position;
        }
        // 見失った場合
        else
        {
            // 追跡中(見失い中)に状態変更
            Change_State(Type03State.ChasingButLosed);
        }
    }
    private void ChasingButLosed()
    {
        // プレイヤーが見えた場合
        if (_patrol.CanSeePlayer())
        {
            // 追跡中に状態変更
            Change_State(Type03State.Chasing);
            _patrol._agent.destination = _patrol._player.transform.position;
        }
        // プレイヤーを見つけられないまま目的地に到着
        else if (_patrol.HasArrived())
        {
            // 巡回中に状態遷移
            Idle();
        }
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
        Idle();
    }
}