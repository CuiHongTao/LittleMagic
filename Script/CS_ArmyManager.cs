using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ArmyManager : MonoBehaviour
{
    private static Dictionary<string, CharacterScript> blueCharacters = new Dictionary<string, CharacterScript>();
    private static Dictionary<string, CharacterScript> redCharacters = new Dictionary<string, CharacterScript>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int GetRedCount()
    {
        return redCharacters.Count;
    }

    public static int GetBlueCount()
    {
        return blueCharacters.Count;
    }

    //redArmy增加角色
    public static void AddRedCharacter(string character, CharacterScript characterScript)
    {
        redCharacters.Add(character, characterScript);
    }

    //blueArmy增加角色
    public static void AddBlueCharacter(string character, CharacterScript characterScript)
    {
        blueCharacters.Add(character, characterScript);
    }

    //改变角色位置
    public static void Move(string oldPosition, string newPosition, List<string> list)
    {
        Dictionary<string, CharacterScript> Characters = null;
        if (redCharacters.ContainsKey(oldPosition))
        {
            Characters = redCharacters;
        }
        else if (blueCharacters.ContainsKey(oldPosition))
        {
            Characters = blueCharacters;
        }
        else return;
        if (oldPosition.Equals(newPosition) || Characters[oldPosition].color != CS_TurnOverPanel.turn)
        {
            return;
        }
        if (Characters[oldPosition].moved) return;
        CharacterScript cha = Characters[oldPosition];
        Characters.Remove(oldPosition);
        Characters.Add(newPosition, cha);
        Characters[newPosition].Move(list);
    }

    // 获取位置角色
    public static CharacterScript GetCharacter(string position)
    {
        if (redCharacters.ContainsKey(position))
        {
            return redCharacters[position];
        }
        else if (blueCharacters.ContainsKey(position))
        {
            return blueCharacters[position];
        }
        return null;
    }

    public static CharacterScript GetCharacter(int i)
    {
        if (CS_TurnOverPanel.turn == FinalVar.RED)
        {
            return new List<CharacterScript>(redCharacters.Values)[i];
        }
        else
        {
            return new List<CharacterScript>(blueCharacters.Values)[i];
        }
    }
}
