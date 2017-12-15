using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Maximum speed allowed for an enemy
    [SerializeField] private float m_MaxSpeed;

    [SerializeField] public float MoveSpeed = 10;
    [SerializeField] public float RotateSpeed = 40;

    [SerializeField] private float m_Projectile_Speed = 3;

    private Rigidbody2D m_RigidBody;
    public List<GameObject> Projectile_List = new List<GameObject>();
    public GameObject m_Projectile_Prefab;


    private void Start()
    {
        m_RigidBody = this.GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update () {
	    // Amount to Move
	    float MoveForward = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
	    float MoveRotate = Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;

        // Move the player
        //transform.Translate(Vector2.up * MoveForward);
	    m_RigidBody.AddRelativeForce(Vector2.up * MoveForward);
        transform.Rotate(Vector3.back * MoveRotate);

        if (Input.GetButton("Fire1"))
        {
            
            GameObject Projectile = Instantiate(m_Projectile_Prefab, transform.position, Quaternion.identity);
            Projectile_List.Add(Projectile);
            Debug.Log("you are pressing w bitch");
        }

        for (int i = 0; i < Projectile_List.Count; i++)
        {
            GameObject goProjectile = Projectile_List[i];
            if (goProjectile != null)
            {
                goProjectile.transform.Translate(-transform.up * Time.deltaTime * m_Projectile_Speed);

                Vector3 projectileScreenPosition = Camera.main.WorldToScreenPoint(goProjectile.transform.position);

                if (projectileScreenPosition.y >= Screen.height + 50 || projectileScreenPosition.y <= -50)
                {
                    DestroyObject(goProjectile);
                    Projectile_List.Remove(goProjectile);
                    Debug.Log("Your projectile got destroyed from the x bounds bitch");
                }

                if (projectileScreenPosition.x >= Screen.width + 50 || projectileScreenPosition.x <= -50)
                {
                    DestroyObject(goProjectile);
                    Projectile_List.Remove(goProjectile);
                    Debug.Log("Your projectile got destroyed from the x bounds bitch");
                }

            }
        }

        
    }

    void OnCollisionEnter2D(Collision2D col) // Destroy when collided with asteroid or player
    {
        Debug.Log("The Player got hit!");
        //Destroy(this.gameObject);
    }
}
