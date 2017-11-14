using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeMemory : MonoBehaviour {
    public GameObject _player;
    // アイテムのデータ
    private ItemDatabase _database;
    public string _name;    // 名前
    public int _id;               // id
    public int _exterior;     // 外装値
    public int _machine;    // 機械値
    public int _fuel;            // 燃料
    public Item.ItemType _type; // アイテムのタイプ
    public float _deleteTime;
	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        _database = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        StartCoroutine(DeleteItem());
        StartCoroutine(Item());
	}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator Item()
    {
        yield return new WaitForSeconds(1.0f);
        _name = _database.items[0]._itemName;
        _id = _database.items[0]._itemId;
        _exterior = _database.items[0]._exterior;
        _machine = _database.items[0]._machine;
        _fuel = _database.items[0]._fuel;
        _type = _database.items[0]._itemType;
    }

    // 指定時間経過したときアイテムを削除
    IEnumerator DeleteItem()
    {
        yield return new WaitForSeconds(_deleteTime);
        Destroy(this.gameObject);
    }
}
