using Gameplay;
using LevelEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;
using static LevelEditor.LevelManager;
using NaughtyAttributes;

namespace Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        public static GameplayController Instance { get; private set; }

        [SerializeField] List<Tilemap> tilemaps = new List<Tilemap>();
        public Dictionary<int, Tilemap> layers = new Dictionary<int, Tilemap>();

        private void Awake()
        {
            Instance = this;

            foreach (Tilemap tilemap in tilemaps)
            {
                foreach (Tilemaps num in System.Enum.GetValues(typeof(Tilemaps)))
                {
                    if (tilemap.name == num.ToString())
                    {
                        if (!layers.ContainsKey((int)num)) layers.Add((int)num, tilemap);
                    }
                }
            }
        }
        public void LoadLevel(CustomTileList customTileList, string levelName)
        {
            Debug.Log("Start load level to gameplay");
            //load the json file to a leveldata
            string json = File.ReadAllText(Application.dataPath + "/" + levelName + ".json");
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            if (PlayerBubble.Instance != null)
            {
                PlayerBubble.Instance.transform.position = levelData.playerPos;
            }
            foreach (var data in levelData.layers)
            {
                if (!layers.TryGetValue(data.layer_id, out Tilemap tilemap)) break;

                //clear the tilemap
                tilemap.ClearAllTiles();

                //place the tiles
                for (int i = 0; i < data.tiles.Count; i++)
                {
                    TileBase tile = customTileList.tiles.Find(t => t.id == data.tiles[i]).tile;
                    if (tile) tilemap.SetTile(new Vector3Int(data.poses_x[i], data.poses_y[i], 0), tile);
                }
            }

            //debug
            Debug.Log("Level was loaded");
        }
    }
}
