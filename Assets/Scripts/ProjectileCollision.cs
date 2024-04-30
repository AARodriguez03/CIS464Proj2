using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour    // removes the projectile as well as something special :) 
{
    [SerializeField] private GameObject objectToSpawn; // for spawning an object on impact 
    private BoxCollider2D projectileBox;           //get the box collider
    private PlayerAbilities abilityCheck;          //get a reference to ability check to activate an ability 
    private GameObject player;                     // player reference 
    private Rigidbody2D playerRigid;               //players rigidbody 
    private FireProjectile bulletType;
    private GameObject pivotPoint;
    private bool canTeleport;                      //set to false for the moment 
    private bool canSpawn;                         //for spawning a rock 
    private bool didImpact;                        //did the projectile hit something?  
    private float playerOrigX;                     //get the og position of the player when it was shot 
    private SpriteRenderer render; 
    private float playerOrigY;
    private Color newCol;
    private int allowedCol = 0; 

    void Start()
    {

        projectileBox = GetComponent<BoxCollider2D>();         // get reference to box collider
        player = GameObject.Find("Player");                    //player
        pivotPoint = GameObject.Find("PivotPoint");
        playerRigid = player.GetComponent<Rigidbody2D>();      // players rigid
        abilityCheck = player.GetComponent<PlayerAbilities>(); //ability script attached to player ( important for accessing information)
        bulletType = pivotPoint.GetComponent<FireProjectile>();
        playerOrigX = player.transform.position.x;
        playerOrigY = player.transform.position.y;
        render = gameObject.GetComponent<SpriteRenderer>();

        if (bulletType.shotType == 1)                   // check to see if we can teleport when bullet spawns 
        {
            newCol = new Color(0.753f, 0.753f, 0.753f, .5f);
            canTeleport = true;                         // set to true 

        }
        else if (bulletType.shotType == 2)               //check to see if we spawn a rock 
        {
            newCol = new Color(0.824f, 0.706f, 0.549f);//brown
            canSpawn = true;

        }
        else
        {
            newCol = Color.white;
        }


    }

    void Update()
    {
        render.color = newCol;

        if (didImpact)                                        // kill the script when a collision happens. Bullet logic is applied elsewhere so no need to here 
        {
            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)     //in built function of Unity that determines when something a trigger hits something ( in this case the projectle) 
    {
                            
        GameObject objHit = collision.gameObject;
        if (objHit.tag == "Player")
        {
            return; 
        }
        didImpact = true;                                   //set to true to kill the script so it doesn't linger 
        projectileBox.enabled = false;


        if (canTeleport == true)                            // if true for either 
        {
            WaterWindCombo(collision.ClosestPoint(transform.position),objHit); // get where the collision happened and call water_wind combo
        }
        else if (canSpawn == true)
        {
            WaterRockCombo(collision.ClosestPoint(transform.position),objHit); //get where colision happend and call water_earth combo 

        }
        Destroy(gameObject);                                            // destory it because it was instantiated 
    }

    private void WaterWindCombo(Vector3 collisionLocation, GameObject obj)              //Function dealing with teleporting player
    {
        float offset = 1.5f;

        if (allowedCol > 1) //falsesafe to make sure the projectile only spawns ( or moves ) player once
        {
            allowedCol = 0;
            return;
        }
        switch (obj.tag)
        {
            case "Wall":
                if (playerOrigX > collisionLocation.x) // the wall position is behind the player, so therefore move to the right 
                {
                    offset = -offset;
                }
                playerRigid.position = new Vector2(collisionLocation.x - offset, collisionLocation.y);//teleport player to the spot a little away from wall 
                break;

            case "Ground":
                if ( playerOrigY < collisionLocation.y)
                {
                    offset = -offset; 
                }
                playerRigid.position = new Vector2(collisionLocation.x, collisionLocation.y + offset);//teleport player to the spot a little away from wall 
                break;


            default:
                playerRigid.position = collisionLocation;
                break; 

        }
      


    }

    private void WaterRockCombo(Vector3 collisionLocation, GameObject obj)              // function dealing with spawning a rock at bullet collision point 
    {
        float offset = 1f; // value for offset 
        allowedCol += 1; 

        if (allowedCol > 1)
        {
            allowedCol = 0;
            return; 
        }

        switch (obj.tag)
        {
            case "Wall":
                if (playerOrigX > collisionLocation.x) // the wall position is behind the player, so therefore move to the right 
                {
                    offset = -1f;
                }
                Instantiate(objectToSpawn, new Vector2(collisionLocation.x - offset, collisionLocation.y), Quaternion.identity); // spawn it away from wall 
                break;

            case "Ground":
                if (playerOrigY < collisionLocation.y)
                {
                    offset = -offset;
                }
                Instantiate(objectToSpawn, new Vector2(collisionLocation.x, collisionLocation.y + offset), Quaternion.identity); // spawn it off from wall 
                break;

            default:
                Instantiate(objectToSpawn, collisionLocation, Quaternion.identity); // spawn it 
                break;

        }



    }
}