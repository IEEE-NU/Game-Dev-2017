using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Attackable {

    // Maximum speed allowed for player
    [SerializeField] private float m_MaxSpeed;

    // Amount of force applied to the player every frame
    [SerializeField] public float MoveSpeed = 10;
    [SerializeField] public float RotateSpeed = 40;

    // Amount of force to apply to shot projectile
    [SerializeField] private float m_Projectile_Force = 3;

    // Gets the game object so that the game knows what layers not collide in
    [SerializeField] private GameObject m_Planet;

    private Rigidbody2D m_RigidBody;
    //List of projectiles when they are instantiated
    public List<GameObject> Projectile_List = new List<GameObject>();

    //Gets the specific projectile prefabs to use
    public GameObject m_Projectile_Prefab_One;
    public GameObject m_Projectile_Prefab_Two;
    public GameObject m_Projectile_Prefab_Three;
    public GameObject m_Projectile_Prefab_Four;

    //The bounds that the player can move in
    [SerializeField] private float m_Xedge = 50;
    [SerializeField] private float m_Yedge = 50;


    private LineRenderer lineRenderer;
    //Keeps track of where the raycastHit is actually encountering a collider
    [SerializeField] private Transform laserHit;

    [SerializeField] public Overheat overheatScript;
    [SerializeField] public GameManager gameManager;

    public bool paused = false;

    private void Start()
    {
        m_RigidBody = this.GetComponent<Rigidbody2D>();

        //Ignores collision between player and planet layers
        Physics2D.IgnoreLayerCollision(8, 9);


    }


    // Update is called once per frame
    void Update () {

        //if (overheatScript.getHeat() >= 100)
        //{
        //    Debug.Log("Gun is overheated");
        //    Debug.Log(overheatScript.getHeat());
        //}
        //else
        //{
        //    Debug.Log("Gun is ready to fire");
        //}

        if (paused)
            return;

        //Debug.Log(overheatScript.getHeat());

        //Rotates the player 
        float MoveRotate = Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;


        float MoveForward = 0;
        GameObject Projectile = null;

        //applies a foward force to the player depending on what button is hit
        if (Input.GetButton("Fire.25"))
        {

            MoveForward = (float).25 * MoveSpeed * Time.deltaTime;
            Projectile = Instantiate(m_Projectile_Prefab_One, transform.TransformPoint(-Vector3.up), Quaternion.identity);

            Projectile.GetComponent<Rigidbody2D>().AddForce(-transform.up * m_Projectile_Force);

            Projectile_List.Add(Projectile);
            overheatScript.AddHeat(.25f);
            //Debug.Log("you are pressing the fire .25 key");
        }
        else if (Input.GetButton("Fire.50"))
        {
            MoveForward = (float).50 * MoveSpeed * Time.deltaTime;
            Projectile = Instantiate(m_Projectile_Prefab_Two, transform.TransformPoint(-Vector3.up), Quaternion.identity);

            Projectile.GetComponent<Rigidbody2D>().AddForce(-transform.up * m_Projectile_Force);
            Projectile_List.Add(Projectile);
            overheatScript.AddHeat(.5f);
        }
        else if (Input.GetButton("Fire.75"))
        {
            MoveForward = (float).75 *  MoveSpeed * Time.deltaTime;
            Projectile = Instantiate(m_Projectile_Prefab_Three, transform.TransformPoint(-Vector3.up), Quaternion.identity);

            Projectile.GetComponent<Rigidbody2D>().AddForce(-transform.up * m_Projectile_Force);

            Projectile_List.Add(Projectile);
            overheatScript.AddHeat(.75f);
        }
        else if (Input.GetButton("Fire1.00"))
        {
            MoveForward =  MoveSpeed * Time.deltaTime;
            Projectile = Instantiate(m_Projectile_Prefab_Four, transform.TransformPoint(-Vector3.up), Quaternion.identity);

            Projectile.GetComponent<Rigidbody2D>().AddForce(-transform.up * m_Projectile_Force);

            Projectile_List.Add(Projectile);

            overheatScript.AddHeat(1f);
        }

        gameManager.updateOverheatText(overheatScript.getHeat());

        if (overheatScript.isOverheated())
        {
            StartCoroutine("Delay");
            overheatScript.resetHeat();
        }

        // Actually move the player
        m_RigidBody.AddRelativeForce(Vector2.up * MoveForward);
        transform.Rotate(Vector3.back * MoveRotate);

        
        //Deletes projectiles based on position relative to camera
        for (int i = 0; i < Projectile_List.Count; i++)
        {
            GameObject goProjectile = Projectile_List[i];
            if (goProjectile != null)
            {
                //legacy way of moving projectile with transforms
                //goProjectile.transform.Translate(-transform.up * Time.deltaTime * m_Projectile_Speed);

                Vector3 projectileScreenPosition = Camera.main.WorldToScreenPoint(goProjectile.transform.position);

                if (projectileScreenPosition.y >= Screen.height || projectileScreenPosition.y <= 0)
                {
                    DestroyObject(goProjectile);
                    Projectile_List.Remove(goProjectile);
                    //Debug.Log("Your projectile got destroyed from the y bounds");
                }

                if (projectileScreenPosition.x >= Screen.width|| projectileScreenPosition.x <= 0)
                {
                    DestroyObject(goProjectile);
                    Projectile_List.Remove(goProjectile);
                    //Debug.Log("Your projectile got destroyed from the x bounds");
                }

            }
        }

       

        //// X axis bounds for player
        //if (transform.position.x <= -m_Xedge)
        //{
        //    transform.position = new Vector2(-m_Xedge, transform.position.y);
        //}
        //else if (transform.position.x >= m_Xedge)
        //{
        //    transform.position = new Vector2(m_Xedge, transform.position.y);
        //}

        //// Y axis bounds for player
        //if (transform.position.y <= -m_Yedge)
        //{
        //    transform.position = new Vector2(transform.position.x, -m_Yedge);
        //}
        //else if (transform.position.y >= m_Yedge)
        //{
        //    transform.position = new Vector2(transform.position.x, m_Yedge);
        //}

    }

    IEnumerator Delay()
    {
        paused = true;
        yield return new WaitForSeconds(3);
        paused = false;
    }

	public override void TakeDamage(float damage)
	{
		m_currHealth -= damage;
		if (m_currHealth <= 0) {
			gameObject.SetActive(false);
			gameManager.GameOver ();
		}
	}
}
