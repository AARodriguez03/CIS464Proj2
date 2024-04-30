using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardDealer : MonoBehaviour
{
    public GameObject[] dealerCards = new GameObject[10];  //array of at most 10 cards consisting of prefabs
    private GameObject[] dealerHand;  //actual dealer hand (will be explained later)
    private int validSpots;           // will get the number of validspots/ num of cards in dealerCards array 
    private BoxCollider2D box;        //bpx collider 
    public TMP_Text dealerText; 

    private void Start()
    {
        dealerText.enabled = false; 
        box = GetComponent<BoxCollider2D>();  //get the box 
        for ( int x = 0; x < dealerCards.Length; x++ )   //get the num of cards predecided 
        {
            if (dealerCards[x] != null)
            {
               validSpots++;
            }
        }

        dealerHand = new GameObject[validSpots];     //make a new array of that many spots 

        for (int x = 0; x < dealerHand.Length; x++)  // instantiate the prefab from the other array and put it into the dealersHand 
        {
            GameObject curr = Instantiate(dealerCards[x]);
            dealerHand[x] = curr; 
        }
    }
    //essentially, we cannot just use the dealerCards array because those are prefabs and as such, unity will crash if we try to access them
    //instead, we Instantiate whatever prefab is in the spot to then add to the array so that we can manipulate it. We basically make a clone that won't effect the actual prefab in memory 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        GameObject objHit = collision.gameObject; // get the gameobject

        if (objHit.tag == "Player")  //when the player is hit and the dealer is able to give cards 
        {
            if ( PlayerPrefs.GetInt("Hints") == 1)
            {
                dealerText.text = "Good Luck!";
            }
            dealerText.enabled = true; 
            box.enabled = false;
            CardHand playerHand = objHit.GetComponent<CardHand>();  //get the card hand 

            for (int x = 0; x < validSpots; x++) // for every valid spot 
            { 
                GameObject currCard = dealerHand[x];  //get the card
                Card cardInfo = currCard.GetComponent<Card>();//gain access to its info 
                currCard.GetComponent<SpriteRenderer>().enabled = false; //disable the sprite cuz it just appears randomly during this process for some reason idk 


                cardInfo.arrayIndex = playerHand.cardIndex;                             //set card info 
                cardInfo.orderInLayer = playerHand.hand.Length - playerHand.cardIndex;

                playerHand.hand[playerHand.cardIndex] = dealerHand[x];                //give the player the card 
                playerHand.UpdateHUD();

            }

             
        }
    }
}
