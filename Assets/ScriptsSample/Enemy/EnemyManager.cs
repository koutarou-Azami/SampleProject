using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject[] _Enemy;

    public GameObject[] _enemyCount;
    public int _instantiateEnemy;

    public int _timer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_timer);
        CheckTime();
    }

    public void CheckTime()
    {
        _timer++;
        if (_timer >= 300.0f)
        {
            Check("Enemy");
        }
    }

    // シーン内にある任意のタグのオブジェクトの数を数える
    public void Check(string tagName)
    {
        _enemyCount = GameObject.FindGameObjectsWithTag(tagName);
        if (_enemyCount.Length <= 0)
        {
            _timer = 0;
            EnemyInstantiate();
        }
    }

    public void EnemyInstantiate()
    {
        for(int i = 0; i < _instantiateEnemy; ++i)
        {
            if (_enemyCount.Length <= _instantiateEnemy)
            {
                // エネミーオブジェクトの配列内からランダムで1種類選出
                GameObject element = _Enemy[Random.Range(0, _Enemy.Length)];
                // 座標位置をランダムで決める
                float x = Random.Range(-100.0f, 100.0f);
                float z = Random.Range(-100.0f, 100.0f);
                // エネミーオブジェクトを生成
                GameObject.Instantiate(element, new Vector3(x, element.transform.position.y, z), Quaternion.identity);
            }
        }
    }
}
