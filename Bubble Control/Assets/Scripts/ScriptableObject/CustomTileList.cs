using LevelEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Customtile List", menuName = "LevelEditor/CustomTileList")]
public class CustomTileList : ScriptableObject
{
    public List<CustomTile> tiles;
}