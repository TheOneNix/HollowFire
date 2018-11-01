using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D hitInfo){
        if (hitInfo.gameObject.GetComponent<Enemy>())
            Destroy(gameObject);
    }
}
