using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameScript : MonoBehaviour //GameScript == Solitare
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject[] bottomPos;
    public GameObject[] topPos;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public List<string>[] bottoms;
    public List<string>[] tops;

    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();

    
    public List<string> deck;
    // public List<string> genDeck;
    
    // public Stack<string> deck;
    // Start is called before the first frame update
    void Start()
    {
        bottoms = new List<string>[] {bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6 };
        // PlayCards()//using a button
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCards()
    {
        deck = GenerateDeck();//generated deck is named 'deck'
        Shuffle(deck);//the generated 'deck' is shuffled using shuffle function

        //test the cards in the deck
        
        foreach (string card in deck) //use this line in DeckDeal() to instantiate cardPrefab in Hierarcy
        {
            print(card);
            
        }
        
        GameSort();
        StartCoroutine(DeckDeal());//this function instantiates the cardPrefab
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach(string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }
        return newDeck;
    }

    // public static Stack<string> GenerateDeck()
    // {
    //     Stack<string> newDeck = new Stack<string>();
    //     foreach(string s in suits)
    //     {
    //         foreach (string v in values)
    //         {
    //             newDeck.Push(v + s);
    //         }
    //     }
    //     return newDeck;
    // }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();

        int n = list.Count;
        while (n>1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    IEnumerator DeckDeal()
    {
        for (int i = 0; i < 7; i++)
        {
            float yOffset = 0; //start yOffset with 0
            float zOffset = 0.03f;//start zOffset with 0.03
            
            foreach(string card in bottoms[i]) //can use defined deck in any function
            // for(int i=0; i<deck.Count; i++)
            {   
                yield return new WaitForSeconds(0.01f);
                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), Quaternion.identity, bottomPos[i].transform); //Instantiating a new gameobject called newCard
                newCard.name = card;//assign the names of card(from PlayCards()) to this new generated cardPrefabs
                if(card == bottoms[i][bottoms[i].Count -1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }
                
                yOffset = yOffset + 0.3f; //after instantiating card with starting offsets, add 0.1f to every current position of card in y axis
                zOffset = zOffset + 0.03f;//after instantiating card with starting offsets, add 0.03f to every current position of card in z axis
                
            }
        }
        
    }

    void GameSort()
    {
        for(int i = 0; i < 7; i++)
        {
            for (int j = i; j < 7; j++)
            {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }
}
