using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Script : MonoBehaviour {
    public int life = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "asteroid")
        {
            Debug.Log("The planet was hit by an asteroid");

            life--;
            if (life == 2)
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 0.30196078f, 0.30196078f);
            }

            if (life == 1)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.388235229f, 0.3372549f, 1f);
            }

            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
