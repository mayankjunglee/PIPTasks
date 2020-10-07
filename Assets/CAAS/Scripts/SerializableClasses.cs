using System;
using System.Text;
[System.Serializable]
public class CardsJsonData
{
    public CardsData data;
    public override string ToString()
    {
        return data.ToString();
    }
}
[System.Serializable]
public class CardsData
{
    public string [] deck;
    public override string ToString()
    {
        if(deck==null) return "no deck found";
        StringBuilder sb = new StringBuilder();
        for(int i =0;i<deck.Length;i++)
        {
            sb.Append(deck[i]+",");
        }
        return sb.ToString();
    }
}