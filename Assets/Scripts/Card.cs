using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Card : MonoBehaviour             //simple script to pickup a card 
{
    public int orderInLayer;
    private CardHand holder;
    public int flag; //used to determine if being used as a mixing card
    public int arrayIndex;    // -1 to indicate not in array.Initial value 
    public int timeAttackInit = 0; // will be used for the cards specifically spawned during time attack that will always be in players inventory. By default 0
    public float posX;
    public float posY;

    private void Start()
    {
        holder = GameObject.FindGameObjectWithTag("Player").GetComponent<CardHand>();  // get access to the array that will hold cards 
     
    }   
    
    private void OnTriggerEnter2D(Collider2D other) //whenever any collision is detected 
    {
        GameObject hitObject = other.gameObject;     //grab the object 

        if (hitObject.tag == "Player")              //if player 
        {
            if (holder.cardIndex + 1 <= 9)                         //if the player wont have more than they can hold 
            {
                Rigidbody2D cardRigid = gameObject.GetComponent<Rigidbody2D>(); //get the rigid body 
                cardRigid.Sleep();                                              //make it sleep 
                BoxCollider2D cardBox = gameObject.GetComponent<BoxCollider2D>();   //get the box collider
                cardBox.enabled = false;                                            //disable it. This is so that hud doesn't get physically effected 

                holder.hand[holder.cardIndex] = gameObject;        //set the array index to the new card 
                arrayIndex = holder.cardIndex;                     //set information
                orderInLayer = holder.hand.Length - holder.cardIndex; //set order in hierarchy
                holder.UpdateHUD();                                   //update the UI 
            }
            else                                                     //user doesnt have enough space or object wasnt player
            {
                return;
            }

        }
    }
}