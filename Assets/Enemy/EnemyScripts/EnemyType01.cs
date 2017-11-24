using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Type01State
{
    Idle,
    Walk,
    Escape,
    Attack,
    Damage
}

public class EnemyType01 : MonoBehaviour
{
    public Type01State _state;

    public GameObject _battery;
    public GameObject _dryCell;
    public GameObject _ironPlate;
    public GameObject _memory;
    public GameObject _screw;
    public GameObject _semiconductor;

    public GameObject _dropObj;

    public int _hp;
    // 消滅時のエフェクト
    public GameObject _explosion;
    private int _rand;

    public GameObject _player;
    public GameObject _enemy;
    public EnemyPatroll _patrol;

    public bool _flag = false;
    // Use this for initialization
    void Start()
    {
        _state = Type01State.Idle;

        _hp = 8;

        _player = GameObject.FindGameObjectWithTag("Player");
        _enemy = GameObject.Find("EnemyType-01");
        _enemy.GetComponent<NavMeshAgent>().speed = 6;

        _patrol = _enemy.GetComponent<EnemyPatroll>();
        _patrol._enemy = GameObject.Find("EnemyType01");
        _patrol._eyePoint = _patrol._enemy.transform.Find("EyePoint");
    }
    // Update is called once per frame
    void Update()
    {
        Update_State();

        // デバッグ
        if (_flag)
        {
            _rand = Random.Range(0, 3);
            for (int i = 0; i < _rand; ++i)
            {
                DetermineFromDict();
                int randPos;
                randPos = Random.Range(-3, 3);
                Vector3 dropItemPosition;
                dropItemPosition.x = transform.position.x + randPos;
                dropItemPosition.y = transform.position.y;
                dropItemPosition.z = transform.position.z + randPos;
                Instantiate(_dropObj, dropItemPosition, Quaternion.identity);
            }
            Destroy(this._enemy);
            _flag = false;
        }
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Change_State(Type01State.Damage);
        }
    }
    private void Update_State()
    {
        switch (_state)
        {
            case Type01State.Idle: Idle(); break;
            case Type01State.Walk: Walk(); break;
            case Type01State.Escape: Escape(); break;
            case Type01State.Attack: Attack(); break;
            case Type01State.Damage: Damage(); break;
            default: break;
        }
    }
    public void Change_State(Type01State state)
    {
        _state = state;
    }
    private void Idle()
    {
        // 目的地に到着したら、次の巡回ポイントを目的地に設定する
        if (_patrol.HasArrived())
        {
            Change_State(Type01State.Walk);
        }
    }
    private void Walk()
    {
        // 目的地を次の巡回ポイントに切り替える
        if (_patrol.CanSeePlayer())
        {
            Change_State(Type01State.Escape);
            _enemy.GetComponent<NavMeshAgent>().destination = _player.transform.position * -1;
        }
        else if (_patrol.HasArrived())
        {
            _patrol.SetNewPatrolPointIndexRandom();
        }
        Change_State(Type01State.Walk);
    }
    private void Escape()
    {
        if (_patrol.CanSeePlayer())
        {
            _enemy.GetComponent<NavMeshAgent>().destination = _player.transform.position * -1;
        }
        else
        {
            Walk();
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
            _rand = Random.Range(0, 3);
            for (int i = 0; i < _rand; ++i)
            {
                DetermineFromDict();
                int randPos;
                randPos = Random.Range(-3, 3);
                Vector3 dropItemPosition;
                dropItemPosition.x = transform.position.x + randPos;
                dropItemPosition.y = transform.position.y;
                dropItemPosition.z = transform.position.z + randPos;
                Instantiate(_dropObj, dropItemPosition, Quaternion.identity);
            }
            Destroy(this._enemy);
        }
        Idle();
    }
    // 一個選ぶテスト
    private void DetermineFromDict()
    {
        if (_rand == 0)
        {
            Dictionary<GameObject, int> targetDicts = new Dictionary<GameObject, int>()
            {
                { _memory, 50 }, { _dryCell, 0 }, { _screw, 40 }, { _semiconductor, 95 }, { _ironPlate, 90 }, { _battery, 90 }
            };

            _dropObj = DropProbability.DetermineFromDict<GameObject>(targetDicts);

            Debug.Log(_dropObj);
        }
        else if (_rand == 1)
        {
            Dictionary<GameObject, int> targetDicts = new Dictionary<GameObject, int>()
            {
                { _memory, 50 }, { _dryCell, 20 }, { _screw, 60 }, { _semiconductor, 5 }, { _ironPlate, 10 }, { _battery, 10 }
            };

            _dropObj = DropProbability.DetermineFromDict<GameObject>(targetDicts);

            Debug.Log(_dropObj);
        }
        else if (_rand == 2)
        {
            Dictionary<GameObject, int> targetDicts = new Dictionary<GameObject, int>()
            {
                { _memory, 0 }, { _dryCell, 0 }, { _screw, 40 }, { _semiconductor, 0 }, { _ironPlate, 0 }, { _battery, 0 }
            };

            _dropObj = DropProbability.DetermineFromDict<GameObject>(targetDicts);

            Debug.Log(_dropObj);
        }
        else if (_rand == 3)
        {
            Dictionary<GameObject, int> targetDicts = new Dictionary<GameObject, int>()
            {
                { _memory, 0 }, { _dryCell, 0 }, { _screw, 0 }, { _semiconductor, 0 }, { _ironPlate, 0 }, { _battery, 0 }
            };

            _dropObj = DropProbability.DetermineFromDict<GameObject>(targetDicts);

            Debug.Log(_dropObj);
        }
    }
}