using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShield_ControllerP2 : MonoBehaviour
{
    [Range(1f, 10f)]
    public static float timer = 6.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0){
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D otro){
       if (otro.gameObject.name == "BulletP1") {
          Debug.Log("bullet1 -> BubbleShiled trigger");
          Destroy(gameObject);
          Destroy(otro.gameObject);
       }
       if (otro.gameObject.name == "BulletP2") {
          Debug.Log("bullet2 -> BubbleShiled trigger");
          Destroy(gameObject);
          Destroy(otro.gameObject);
       }

    }
    void OnCollisionEnter2D(Collision2D otro){
       if (otro.gameObject.name == "BulletP1") {
          Debug.Log("bullet1 -> BubbleShiled");
          Destroy(gameObject);
       }
       if (otro.gameObject.name == "BulletP2") {
          Debug.Log("bullet2 -> BubbleShiled");
          Destroy(gameObject);
       }
    }
}
