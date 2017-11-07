using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropItem
{
    Fuel
}

public class Item : MonoBehaviour {

    public DropItem _item;
    public float _deleteTime;

	// Use this for initialization
	void Start () {
        StartCoroutine(DeleteTime());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public DropItem GetItem()
    {
        return _item;
    }

    IEnumerator DeleteTime()
    {
        yield return new WaitForSeconds(_deleteTime);
        Destroy(this.gameObject);
    }
}
