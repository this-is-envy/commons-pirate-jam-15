using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName = "ScriptableObjects/Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    [TextArea(15, 20)]
    public string description;
    public int cost;
    public Sprite sprite;
    //todo: Add effects
    public EffectType effectType;


    

}
