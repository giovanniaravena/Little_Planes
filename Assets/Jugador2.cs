using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jugador2 : MonoBehaviour
{
    public static bool shieldActive{ get; set;}
    public static float damage{ get; set;}
    public float life = 100f;

    [Range(100f, 300f)]
    public float velocidadMovimiento = 100f;

    [Tooltip("Actívalo si quieres que el sprite rote automáticamente en horizontal al cambiar de dirección")]
    public bool autoRotarSprite = true;

    public GameObject shieldPrefab;
    public GameObject bulletPrefab;

    [SerializeField]
    GameObject EndGame;

    public float offsetBalaX = 0;
    public float offsetBalaY = 0;

    [Range(0.0f, 10.0f)]
    public float smooth = 5.0f;

    private float tiltAroundZ;
    private Rigidbody2D rb2d2;
    private SpriteRenderer sprite2;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb2d2 = GetComponent<Rigidbody2D>();
        sprite2 = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d2.velocity = new Vector2( -velocidadMovimiento/2 * Time.deltaTime , velocidadMovimiento/2 * Time.deltaTime);
        damage = 0.0f;
        shieldActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if( damage != 0.0f ){
            if ( !shieldActive ){
                life += damage;
                HealthBar_P2.Health = life;
            }
            damage = 0.0f;
        }
        if (BubbleShield_ControllerP2.timer <= 0){
            shieldActive = false;
        }
        
        if (life <= 0){
            EndGame.gameObject.SetActive(true);
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        Mover();
    }

    private void Mover()
    {
        rb2d2.gravityScale = 0.2f;

        if (transform.position.x > -9.0f && transform.position.x < 9.0f){
            float velocidadY = rb2d2.velocity.y;
            if  (Input.GetKey("a")||Input.GetKey("d"))
                rb2d2.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime , velocidadY);
        }else{
            Vector3 temp = new Vector3(transform.position.x,transform.position.y,0);
            if (transform.position.x <= -9.0f )
                temp = new Vector3(9.0f,transform.position.y,0);
            if (transform.position.x >= 9.0f )
                temp = new Vector3(-9.0f,transform.position.y,0);
            transform.position = temp;
        }

        if (transform.position.y < 4.6f){
            float velocidadX =  rb2d2.velocity.x;
            if  (Input.GetKey("w")||Input.GetKey("s"))
                rb2d2.velocity = new Vector2(velocidadX , Input.GetAxis("Vertical") * velocidadMovimiento * Time.deltaTime);
        }

        //tiltAroundZ = Input.GetAxis("Vertical") * tiltAngle;
        if (rb2d2.velocity.x != 0)
            tiltAroundZ = Mathf.Rad2Deg * Mathf.Atan(rb2d2.velocity.y / rb2d2.velocity.x);
        

        if (autoRotarSprite)
        {
            if (rb2d2.velocity.x > 0f)
                sprite2.flipX = true;
            else if (rb2d2.velocity.x < 0f)
                sprite2.flipX = false;

            Quaternion target2 = Quaternion.Euler(0, 0, tiltAroundZ);
            transform.rotation = Quaternion.Slerp(target2, target2,  Time.fixedDeltaTime * smooth);
        }

        if  (Input.GetKeyDown(KeyCode.LeftControl)){
            animator.SetTrigger("DisparoPlayer2");
            BulletFire();
        }
    }

    void BulletFire(){
        GameObject bullet;   
        if (sprite2.flipX)
        {
            bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet.transform.position += new Vector3( Mathf.Cos(transform.rotation.eulerAngles.z*Mathf.Deg2Rad)*offsetBalaX , Mathf.Sin(transform.rotation.eulerAngles.z*Mathf.Deg2Rad)*offsetBalaX+offsetBalaY, 0);
        }else{
            Quaternion targ = Quaternion.Euler(0, 0, (tiltAroundZ+180));
            Quaternion rot = Quaternion.Slerp(targ,targ,Time.deltaTime*smooth);
            bullet = Instantiate(bulletPrefab, transform.position, rot) as GameObject;
            bullet.transform.position += new Vector3( Mathf.Cos(rot.eulerAngles.z*Mathf.Deg2Rad)*offsetBalaX , -Mathf.Sin(transform.rotation.eulerAngles.z*Mathf.Deg2Rad)*offsetBalaX+offsetBalaY , 0);
        }
    }

/*
    void OnTriggerEnter2D(Collider2D otro){
        if (otro.gameObject.name == "BulletP1") {
                Debug.Log("otro Jugador2: me ha llegado una bala! ********");
                Destroy(otro.gameObject);
        }
        /*
        if (otro.gameObject.name == "Hills") {
          Debug.Log("Hills Player02");
          //Destroy(otro.gameObject);
        }
    
    }   */

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.name == "BulletP1") {
                Debug.Log("Jugador2: me ha llegado una bala! ******");
                Destroy(col.gameObject);
        }

        if (col.gameObject.name == "Biplane1") {
            Debug.Log("choque con Biplane1 ");
        }
        if (col.gameObject.name == "SilverShield") {
            ApplyShield();
            Destroy(col.gameObject);
        }
        
        if (col.gameObject.name == "Hills") {
            if (!shieldActive){
                life -= 10;
                HealthBar_P2.Health = life;
            }
        }
    }

    public void ApplyShield(){
        shieldActive = true;
        GameObject shield = Instantiate(shieldPrefab, transform.position, transform.rotation) as GameObject;
        shield.transform.parent = rb2d2.transform;
    }

}