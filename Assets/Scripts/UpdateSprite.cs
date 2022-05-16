using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private GameScript gameScript; //solitare
    // Start is called before the first frame update
    void Start()
    {
        List<string> deck = GameScript.GenerateDeck();//Calling deck from the GameScript
        gameScript = FindObjectOfType<GameScript>();//keeping GameScript in variable gameScript

        int i = 0;//start with 0
        foreach (string card in deck) //same foreach function of deck to loop all cards
        {
            if(this.name == card) 
            {
                cardFace = gameScript.cardFaces[i];
                break;
            }
            i++;//as we found correct card, now increment i
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selectable.faceUp == true) //i can modify this to clickable, i.e if the card is clickable(yop card) then faceUp == true(Peek())
        {
            spriteRenderer.sprite = cardFace;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }
}
