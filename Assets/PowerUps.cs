using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private float timer;
    public List<GameObject> items;
    // Start is called before the first frame update
    void Start()
    {
        // If you store all your items that you want to load in the same folder (Assets/Resources/MyItemsToLoad).
        //items = Resources.LoadAll("Prefab_PowerUps") as GameObject[];
        //items = Resources.LoadAll<GameObject>("Prefab_PowerUps");
        items = new List<GameObject>(Resources.LoadAll<GameObject>("Prefab_PowerUps"));
        timer = Random.Range(1.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0){
            //var element = items[Random.Range(0, myArray.Length)];
            int prefabIndex = UnityEngine.Random.Range(0,items.Count);
            Vector3 randomPosition = new Vector3( Random.Range(-8.5f,8.5f) , 5.1f , 0);
            GameObject powerUp = Instantiate(items[prefabIndex], randomPosition, Quaternion.identity) as GameObject;
            //GameObject powerUp = Instantiate(items[prefabIndex], transform.position, Quaternion.identity) as GameObject;
            //Debug.Log(transform.position);
            //powerUp.transform.position += new Vector3(  , , 0);
            timer = Random.Range(1.0f, 10.0f);

        }
        
    }
}
