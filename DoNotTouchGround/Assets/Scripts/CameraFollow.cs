using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player;

    public float shakeTimer;
    public float shakeAmount;

    void FixedUpdate()
    {


        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

    }


   void update()
   {

     if(shakeTimer > 0)
     {
       Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;

       transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);

       shakeTimer -= Time.deltaTime;
     }

     if (Input.GetKeyDown("space"))
     {
       ShakeCamera(0.1f, 1);
     }

   }

   public void ShakeCamera(float shakePwr, float shakeDur)
   {
     shakeAmount = shakePwr;
     shakeTimer = shakeDur;
   }
}
