using System;
using UnityEngine;
public class CardData
{
    private CardType mCardType = CardType.NONE;
    private CardValue mCardValue = CardValue.NONE;


    public void SetCardData(string cardName)
    {
        if(cardName==null)
        {
            Debug.LogError("Can't pass null value to card.");
            return;
        }
        if(cardName.Length>=2)
        {
            string cardType = cardName.Substring(0,1);
            string cardValue = cardName.Substring(1,cardName.Length-1);
            mCardType = GetCardType(cardType);
            mCardValue = GetCardValue(cardValue);
        }
        if(mCardType==CardType.NONE||mCardValue==CardValue.NONE)
        {
            Debug.LogError($"CardName {cardName} is not a valid format");
        }
    }
    private CardType GetCardType(string value)
    {
        switch(value)
        {
            case "C":
                return CardType.CLUB;
            case "H":
                return CardType.HEART;
            case "D":
                return CardType.DIAMOND;
            case "S":
                return CardType.SPADE;
        }
        return CardType.NONE;
    }
    private CardValue GetCardValue(string value)
    {
        switch(value)
        {
            case "A":
                return CardValue.A;
            case "2":
                return CardValue.TWO;
            case "3":
                return CardValue.THREE;
            case "4":
                return CardValue.FOUR;
            case "5":
                return CardValue.FIVE;
            case "6":
                return CardValue.SIX;
            case "7":
                return CardValue.SEVEN;
            case "8":
                return CardValue.EIGHT;
            case "9":
                return CardValue.NINE;
            case "10":
                return CardValue.TEN;
            case "J":
                return CardValue.J;
            case "Q":
                return CardValue.Q;
            case "K":
                return CardValue.K;
        }
        return CardValue.NONE;
    }
}
