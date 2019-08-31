using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller_P1 : MonoBehaviour
{
    [Range(100f, 600f)]
    public float velocidadBala = 400f;

    private float vectorDisparoX;
    private float vectorDisparoY;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        vectorDisparoX = Mathf.Cos(transform.rotation.eulerAngles.z*Mathf.Deg2Rad);
        vectorDisparoY = Mathf.Sin(transform.rotation.eulerAngles.z*Mathf.Deg2Rad);
    }

    // Update is called once per frame
    void Update()
    {

        rb2d.velocity = new Vector2( (vectorDisparoX*velocidadBala*Time.deltaTime) , (vectorDisparoY*velocidadBala*Time.deltaTime) );
    }

    void OnTriggerEnter2D(Collider2D otro){
      
      if (otro.gameObject.name == "Hills") {
         Destroy(gameObject);
      }
      
      if (otro.gameObject.name == "BubbleShiled") {
         Destroy(gameObject);
         Destroy(otro.gameObject);
      }
      if (otro.gameObject.name == "Biplane2") {
         Jugador2.damage = -10.0f;
         Destroy(gameObject);
         //Destroy(otro.gameObject);
      }
      
    }


    void OnCollisionEnter2D(Collision2D otro){
       /* 
      if (otro.gameObject.name == "Hills") {
         Destroy(gameObject);
      }
      if (otro.gameObject.name == "Biplane2") {
         Jugador2.damage = -10.0f;
         Destroy(gameObject);
      }
      
      if (otro.gameObject.name == "BubbleShiled") {
         Debug.Log("col bullet2 -> BubbleShiled");
         Destroy(gameObject);
      }
      */
    }
    void OnBecameInvisible(){
      Destroy(gameObject);
    }
}
