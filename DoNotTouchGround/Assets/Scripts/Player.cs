using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Maximum speed allowed for an enemy
    [SerializeField] private float m_MaxSpeed;

    [SerializeField] public float MoveSpeed = 10;
    [SerializeField] public float RotateSpeed = 40;

    [SerializeField] private float m_Projectile_Force = 3;

    private Rigidbody2D m_RigidBody;
    public List<GameObject> Projectile_List = new List<GameObject>();

    public GameObject m_Projectile_Prefab_One;
    public GameObject m_Projectile_Prefab_Two;
    public GameObject m_Projectile_Prefab_Three;
    public GameObject m_Projectile_Prefab_Four;


    private void Start()
    {
        m_RigidBody = this.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update () {

	    float MoveRotate = Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;
        float MoveForward = 0;
        GameObject Projectile = null;

        if (Input.GetButton("Fire.25"))
        {
            MoveForward = (float).25 * MoveSpeed * Time.deltaTime;
            Projectile = Instantiate(m_Projectile_Prefab_One, transform.TransformPoint(-Vector3.up), Quaternion.identity);

            Projectile.GetComponent<Rigidbody2D>().AddForce(-transform.up * m_Projectile_Force);

            Projectile_List.Add(Projectile);
            //Debug.Log("you are pressing the fire .25 key");
        }
        else if (Input.GetButton("Fire.50"))
        {
            MoveForward = (float).50 * MoveSpeed * Time.deltaTime;
            Projectile = Instantiate(m_Projectile_Prefab_Two, transform.TransformPoint(-Vector3.up), Quaternion.identity);

            Projectile.GetComponent<Rigidbody2D>().AddForce(-transform.up * m_Projectile_Force);
            Projectile_List.Add(Projectile);
        }
        else if (Input.GetButton("Fire.75"))
        {
            MoveForward = (float).75 *  MoveSpeed * Time.deltaTime;
            Projectile = Instantiate(m_Projectile_Prefab_Three, transform.TransformPoint(-Vector3.up), Quaternion.identity);

            Projectile.GetComponent<Rigidbody2D>().AddForce(-transform.up * m_Projectile_Force);

            Projectile_List.Add(Projectile);
        }
        else if (Input.GetButton("Fire1.00"))
        {
            MoveForward =  MoveSpeed * Time.deltaTime;
            Projectile = Instantiate(m_Projectile_Prefab_Four, transform.TransformPoint(-Vector3.up), Quaternion.identity);

            Projectile.GetComponent<Rigidbody2D>().AddForce(-transform.up * m_Projectile_Force);

            Projectile_List.Add(Projectile);
        }

        // Move the player
        m_RigidBody.AddRelativeForce(Vector2.up * MoveForward);
        transform.Rotate(Vector3.back * MoveRotate);

        

        for (int i = 0; i < Projectile_List.Count; i++)
        {
            GameObject goProjectile = Projectile_List[i];
            if (goProjectile != null)
            {
                //legacy way of moving projectile
                //goProjectile.transform.Translate(-transform.up * Time.deltaTime * m_Projectile_Speed);

                Vector3 projectileScreenPosition = Camera.main.WorldToScreenPoint(goProjectile.transform.position);

                if (projectileScreenPosition.y >= Screen.height + 50 || projectileScreenPosition.y <= -50)
                {
                    DestroyObject(goProjectile);
                    Projectile_List.Remove(goProjectile);
                    //Debug.Log("Your projectile got destroyed from the y bounds bitch");
                }

                if (projectileScreenPosition.x >= Screen.width + 50 || projectileScreenPosition.x <= -50)
                {
                    DestroyObject(goProjectile);
                    Projectile_List.Remove(goProjectile);
                    //Debug.Log("Your projectile got destroyed from the x bounds bitch");
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
