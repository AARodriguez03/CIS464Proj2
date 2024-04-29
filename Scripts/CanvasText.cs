using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasText : MonoBehaviour // to display cheat info during gameplays
{
    public TMP_Text text;
    public TMP_Text notifyText;
    public TMP_Text cardText; 
    private OptionsScript options;
    private CardHand holder;
    private GameObject card;
    private PlayerAbilities abilites; 
    private void Start()
    {
        options = GameObject.Find("PauseMenuCanvas").GetComponent<OptionsScript>(); //get options 
        int temp = PlayerPrefs.GetInt("InfiniteCard", 0); //get the saved value at the beginning ( cheat may not be initialized in time)
        holder = GameObject.FindGameObjectWithTag("Player").GetComponent<CardHand>();
        abilites = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>();

        if (temp == 1)   // if true 
        {
            text.enabled = true; // display text
            notifyText.enabled = false;

        }
        else   //if not true
        {
            text.enabled = false;   //dont 
        }
    }

    private void Update()
    {
        if (options.cheatInfinite == true && !text.enabled)  //if cheat is suddenly true yet the text is disabled 
        {//must've been toggled. Notify player
            notifyText.enabled = true;
        }

        card = holder.hand[holder.highlightIndex];


        if (card == null)
        {
            cardText.enabled = false; 
            return; 

        }

        if (abilites.mixingIndex > 0)
        {
            string cardID;
            GameObject card1 = abilites.mixingArray[0];
            GameObject card2 = abilites.mixingArray[1];
            if (card2 == null) //the first mixing card will always exist. Therefore only check for if the 2nd is null 
            {
                cardID = card1.tag;
                cardText.text = "Mixing: " + cardID + "+";
            }
            else
            {
                cardID = card1.tag + card2.tag;
                switch (cardID)                                         //instantiate the ability card based on the combo id
                {                                                      //whole bunch of nonsense. Assigns the card as one of the prefabs
                    case "FireFire":
                        cardText.text = "Mixing combo: Fire+Fire. Move 2x faster for 5 seconds";
                        break;

                    case "FireWind":
                        cardText.text = "Mixing combo: Fire+Wind. Triple jump!";
                        break;

                    case "FireEarth":
                        cardText.text = "Mixing combo: Fire+Earth. Spawn a bomb that blows breakable walls. Press 'Z' to detonate";
                        break;
                    case "FireWater":
                        cardText.text = "Mixing combo: Fire+Water. Aim in a direction to launch yourself in the opposite direction. Can be used 3 times";
                        break;

                    case "WaterWind":
                        cardText.text = "Mixing combo: Water+Wind. Teleport to wherever water projectile hits. 3 shots";
                        break;
                    case "WaterEarth":
                        cardText.text = "Mixing combo: Water+Earth. Spawn a rock on projectile impact. 1 shot";
                        break;
                    case "WaterWater":
                        cardText.text = "Mixing combo: Water+Water. 6 total shots!";
                        break;
                    case "WaterFire":
                        cardText.text = "Mixing combo: Fire+Water. Aim in a direction to launch yourself in the opposite direction. Can be used 3 times";
                        break;

                    case "EarthWind":
                        cardText.text = "Mixing combo: Earth+Wind. Create a bridge below you";
                        break;
                    case "EarthEarth":
                        cardText.text = "Mixing combo: Earth+Earth. Turn into a rock and crash through breakable floors below. Lasts 5 seconds";
                        break;
                    case "EarthWater":
                        cardText.text = "Mixing combo: Water+Earth. Spawn a rock on projectile impact. 1 shot";
                        break;
                    case "EarthFire":
                        cardText.text = "Mixing combo: Fire+Earth. Spawn a bomb that blows breakable walls. Press 'Z' to detonate";
                        break;

                    case "WindFire":
                        cardText.text = "Mixing combo: Fire+Wind. Triple jump!";
                        break;
                    case "WindEarth":
                        cardText.text = "Mixing combo: Earth+Wind. Create a bridge below you";
                        break;
                    case "WindWater":
                        cardText.text = "Mixing combo: Water+Wind. Teleport to wherever water projectile hits. 3 shots";
                        break;

                    case "WindWind":
                        cardText.text = "Mixing combo: Wind+Wind. Create a bounce pad that launches objects. Lasts 20 seconds";
                        break;


                }
            }
            cardText.enabled = true; 
            return; 
        }

        string cardInfo = card.tag; // grab the current highlighted tag 

        switch (cardInfo)  // display info based on curr index 
        {
            case "Fire":
                cardText.text = "Fire: Move 2x faster for 5 seconds";
                break;

            case "Water":
                cardText.text = "Water: Fire 3 projectiles. Aim with mouse and hold right click to aim and press left click to shoot";
                break;

            case "Earth":
                cardText.text = "Earth: Spawn a rock";
                break;

            case "Wind":
                cardText.text = "Wind: Dash forward a set distance";
                break;

            case "FireWater":
                cardText.text = "Fire+Water: Aim in a direction to launch yourself in the opposite direction. Can be used 3 times";
                break;

            case "FireEarth":
                cardText.text = "Fire+Earth: Spawn a bomb that blows breakable walls. Press 'Z' to detonate";
                break;

            case "FireWind":
                cardText.text = "Fire+Wind: Triple jump!";
                break;

            case "FireFire":
                cardText.text = "Inferno: Move 2x faster than normal fire for 5 seconds!";
                break;

            case "WaterWind":
                cardText.text = "Water+Wind: Teleport to wherever water projectile hits. 3 shots";
                break;

            case "WaterEarth":
                cardText.text = "Water+Earth: Spawn a rock on projectile impact. 1 shot";
                break;

            case "WaterWater":
                cardText.text = "Flash flood: 6 total shots!";
                break;

            case "EarthEarth":
                cardText.text = "Double Earth: Turn into a rock and crash through breakable floors below. Lasts 5 seconds";
                break;

            case "EarthWind":
                cardText.text = "Earth+Wind: Create a bridge below you";
                break;

            case "WindWind":
                cardText.text = "Double Wind: Create a bounce pad that launches objects. Lasts 20 seconds";
                break;


        }


        cardText.enabled = true;
    }
}
