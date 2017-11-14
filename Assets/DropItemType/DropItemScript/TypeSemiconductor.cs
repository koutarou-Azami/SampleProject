using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeSemiconductor : MonoBehaviour {
    private ItemDatabase _database;
    public string _name;
    public int _id;
    public int _exterior;
    public int _machine;
    public int _fuel;
    public Item.ItemType _type;
    public float _deleteTime;
    // Use this for initialization
    void Start()
    {
        _database = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        StartCoroutine(DeleteItem());
        StartCoroutine(Item());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Item()
    {
        yield return new WaitForSeconds(1.0f);
        _name = _database.items[4]._itemName;
        _id = _database.items[4]._itemId;
        _exterior = _database.items[4]._exterior;
        _machine = _database.items[4]._machine;
        _fuel = _database.items[4]._fuel;
        _type = _database.items[4]._itemType;
    }

    // 指定時間経過したときアイテムを削除
    IEnumerator DeleteItem()
    {
        yield return new WaitForSeconds(_deleteTime);
        Destroy(this.gameObject);
    }
}
