using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[CreateAssetMenu(fileName = "New Placable", menuName = "ScriptableObjects/Placeable")]
public class PlaceableSO : ScriptableObject {
    public string name;
    public string description;
    public int value;
}