using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Type02State
{
    Idle,
    Walk,
    Attack,
    Damage
}

public class EnemyType02 : MonoBehaviour
{
    public Type02State _state;

    public GameObject _battery;            // バッテリー
    public GameObject _dryCell;             // 乾電池
    public GameObject _ironPlate;          //鉄板
    public GameObject _memory;           // メモリー
    public GameObject _screw;               // ネジ
    public GameObject _semiconductor; // 半導体

    public GameObject _dropObj;

    public int _hp;
    // 消滅時のエフェクト
    public GameObject _explosion;
    private int _rand;

    public GameObject _enemy;
    public EnemyPatroll _patrol;

    // デバッグ用
    public bool _flag;
    // Use this for initialization
    void Start()
    {
        _state = Type02State.Idle;

        _hp = 4;

        _enemy = GameObject.Find("EnemyType-02");
        _enemy.GetComponent<NavMeshAgent>().speed = 4;

        _patrol = _enemy.GetComponent<EnemyPatroll>();
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
            Change_State(Type02State.Damage);
        }
    }
    private void Update_State()
    {
        switch (_state)
        {
            case Type02State.Idle: Idle(); break;
            case Type02State.Walk: Walk(); break;
            case Type02State.Attack: Attack(); break;
            case Type02State.Damage: Damage(); break;
            default: break;
        }
    }
    public void Change_State(Type02State state)
    {
        _state = state;
    }
    private void Idle()
    {
        // 目的地に到着したら、次の巡回ポイントを目的地に設定する
        if (_patrol.HasArrived())
        {
            Change_State(Type02State.Walk);
        }
    }
    private void Walk()
    {
        if (_patrol.HasArrived())
        {
            // 目的地を次の巡回ポイントに切り替える
            _patrol.SetNewPatrolPointIndex();
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
                DetermineFromDict();
                int randPos;
                randPos = Random.Range(-3, 3);
                Vector3 dropItemPosition;
                dropItemPosition.x = transform.position.x + randPos;
                dropItemPosition.y = transform.position.y;
                dropItemPosition.z = transform.position.z + randPos;
                Instantiate(_dropObj, dropItemPosition, Quaternion.identity);
            }
            Destroy(gameObject);
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
                { _memory, 20 }, { _dryCell, 15 }, { _screw, 30 }, { _semiconductor, 85 }, { _ironPlate, 95 }, { _battery, 80 }
            };

            _dropObj = DropProbability.DetermineFromDict<GameObject>(targetDicts);

            Debug.Log(_dropObj);
        }
        else if (_rand == 1)
        {
            Dictionary<GameObject, int> targetDicts = new Dictionary<GameObject, int>()
            {
                { _memory, 60 }, { _dryCell, 20 }, { _screw, 50 }, { _semiconductor, 15 }, { _ironPlate, 5 }, { _battery, 20 }
            };

            _dropObj = DropProbability.DetermineFromDict<GameObject>(targetDicts);

            Debug.Log(_dropObj);
        }
        else if (_rand == 2)
        {
            Dictionary<GameObject, int> targetDicts = new Dictionary<GameObject, int>()
            {
                { _memory, 20 }, { _dryCell, 60 }, { _screw, 20 }, { _semiconductor, 0 }, { _ironPlate, 0 }, { _battery, 0 }
            };

            _dropObj = DropProbability.DetermineFromDict<GameObject>(targetDicts);

            Debug.Log(_dropObj);
        }
        else if (_rand == 3)
        {
            Dictionary<GameObject, int> targetDicts = new Dictionary<GameObject, int>()
            {
                { _memory, 0 }, { _dryCell, 5 }, { _screw, 0 }, { _semiconductor, 0 }, { _ironPlate, 0 }, { _battery, 0 }
            };

            _dropObj = DropProbability.DetermineFromDict<GameObject>(targetDicts);

            Debug.Log(_dropObj);
        }
    }
}