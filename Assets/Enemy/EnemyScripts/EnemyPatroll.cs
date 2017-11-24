using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.ObjectModel;

public class EnemyPatroll : MonoBehaviour {
    // 巡回ポイント
    public Transform[] _patrolPoints;

    public NavMeshAgent _agent;
    // 現在の巡回ポイントのインデックス
    private int _currentPatrolPointIndex = -1;

    // 見える距離
    public float _viewingDistance;
    // 見える視野角
    public float _viewingAngle;
    // プレイヤーの参照
    public GameObject _player;
    // プレイヤーへの注視点
    public Transform _playerLookPoint;
    // 子にあるエネミー本体の参照
    public GameObject _enemy;
    // 自身の目の位置
    public Transform _eyePoint;
    // Use this for initialization
    void Start () {
        _agent = GetComponent<NavMeshAgent>();

        // 目的地を設定する
        SetNewPatrolPointIndex();

        // タグでプレイヤーオブジェクトを検索して保持
        _player = GameObject.FindGameObjectWithTag("Player");
        // プレイヤーの注視点を名前で検索して保持
        _playerLookPoint = _player.transform.Find("LookPoint");
    }
	
	// Update is called once per frame
	void Update () {
        // 目的地に到着したら、次の巡回ポイントを目的地に設定する
        //if (HasArrived())
        //{
        //    SetNewPatrolPointIndexRandom();
        //}
    }
    // 次の巡回ポイントを目的地に設定する
    public void SetNewPatrolPointIndex()
    {
        _currentPatrolPointIndex
            = (_currentPatrolPointIndex + 1) % _patrolPoints.Length;

        _agent.destination = _patrolPoints[_currentPatrolPointIndex].position;
    }
    // 次の巡回ポイントを目的地に設定する(ランダム式)
    public void SetNewPatrolPointIndexRandom()
    {
        Transform currentPatrolPoint = _patrolPoints[Random.Range(0, _patrolPoints.Length)];

        _agent.destination = currentPatrolPoint.position;
        Debug.Log(currentPatrolPoint);
    }
    // 目的地に到着したか
    public bool HasArrived()
    {
        return (Vector3.Distance(_agent.destination, transform.position) < 0.5f);
    }
    // プレイヤーが見える距離内にいるか？
    public bool IsPlayerInViewingDistance()
    {
        // 自身からプレイヤーまでの距離
        float distanceToPlayer
            = Vector3.Distance(_playerLookPoint.position, _eyePoint.position);
        // プレイヤーが見える距離内にいるかどうかを返却する
        return (distanceToPlayer <= _viewingDistance);
    }
    // プレイヤーが見える視野角内にいるか？
    public bool IsPlayerInViewingAngle()
    {
        // 自分からプレイヤーへの方向ベクトル(ワールド座標系)
        Vector3 directionToPlayer = _playerLookPoint.position - _eyePoint.position;
        // 自分の正面向きベクトルとプレイヤーへの方向ベクトルの差分角度
        float angleToPlayer = Vector3.Angle(_eyePoint.forward, directionToPlayer);
        // 見える視野角の範囲内にプレイヤーがいるかどうかを返却する
        return (Mathf.Abs(angleToPlayer) <= _viewingAngle);
    }
    // プレイヤーにRayを飛ばしたら当たるか？
    public bool CanHitRayToPlayer()
    {
        // 自分からプレイヤーへの方向ベクトル(ワールド座標系)
        Vector3 directionToPlayer = _playerLookPoint.position - _eyePoint.position;
        // 壁の向こう側などにいる場合は見えない
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(_eyePoint.position, directionToPlayer, out hitInfo);
        // プレイヤーにRayが当たったかどうかを返却する
        return (hit && hitInfo.collider.tag == "Player");
    }
    // プレイヤーが見えるか？
    public bool CanSeePlayer()
    {
        // 見える距離の範囲内にプレイヤーがいない場合→見えない
        if (!IsPlayerInViewingDistance())
            return false;
        // 見える視野角内の範囲内にプレイヤーがいない場合→見えない
        if (!IsPlayerInViewingAngle())
            return false;
        // Rayを飛ばして、それがプレイヤーん当たらない場合→見えない
        if (!CanHitRayToPlayer())
            return false;
        // ここまで到達したら、それはプレイヤーが見えるということ
        return true;
    }
}
