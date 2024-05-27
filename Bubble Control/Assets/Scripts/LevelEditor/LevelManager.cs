using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using NaughtyAttributes;
using GDC.Managers;
using GDC.Enums;

namespace LevelEditor
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        private void Awake()
        {
            //set up the instance
            if (Instance == null) Instance = this;
            else Destroy(this);

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

        public List<CustomTile> tiles = new List<CustomTile>();
        [SerializeField] List<Tilemap> tilemaps = new List<Tilemap>();
        public Dictionary<int, Tilemap> layers = new Dictionary<int, Tilemap>();
        [SerializeField] string levelName;
        public enum Tilemaps
        {
            Sky = 30,
            Background = 40,
            Ground = 50
        }

        [Button]
        void Savelevel()
        {
            //create a new leveldata
            LevelData levelData = new LevelData();

            //set up the layers in the leveldata
            foreach (var item in layers.Keys)
            {
                levelData.layers.Add(new LayerData(item));
            }

            foreach (var layerData in levelData.layers)
            {
                if (!layers.TryGetValue(layerData.layer_id, out Tilemap tilemap)) break;

                //get the bounds of the tilemap
                BoundsInt bounds = tilemap.cellBounds;

                //loop trougth the bounds of the tilemap
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    for (int y = bounds.min.y; y < bounds.max.y; y++)
                    {
                        //get the tile on the position
                        TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));
                        //find the temp tile in the custom tiles list
                        CustomTile temptile = tiles.Find(t => t.tile == temp);

                        //if there's a customtile associated with the tile
                        if (temptile != null)
                        {
                            //add the values to the leveldata
                            layerData.tiles.Add(temptile.id);
                            layerData.poses_x.Add(x);
                            layerData.poses_y.Add(y);
                        }
                    }
                }

            }

            //save the data as a json
            string json = JsonUtility.ToJson(levelData, true);
            File.WriteAllText(Application.dataPath + "/" + levelName + "json", json);

            //debug
            Debug.Log("Level was saved");
        }

        [Button]
        void LoadLevel()
        {
            //load the json file to a leveldata
            string json = File.ReadAllText(Application.dataPath + "/" + levelName + ".json");
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            foreach (var data in levelData.layers)
            {
                if (!layers.TryGetValue(data.layer_id, out Tilemap tilemap)) break;

                //clear the tilemap
                tilemap.ClearAllTiles();

                //place the tiles
                for (int i = 0; i < data.tiles.Count; i++)
                {
                    TileBase tile = tiles.Find(t => t.id == data.tiles[i]).tile;
                    if (tile) tilemap.SetTile(new Vector3Int(data.poses_x[i], data.poses_y[i], 0), tile);
                }
            }

            //debug
            Debug.Log("Level was loaded");
        }
        [Button]
        void LoadGameplay()
        {
            GameManager.Instance.LoadSceneManually(SceneType.GAMEPLAY, TransitionType.LEFT, SoundType.NONE, (() => Debug.Log("AAA")));
        }
    }

    [System.Serializable]
    public class LevelData
    {
        public List<LayerData> layers = new List<LayerData>();
    }

    [System.Serializable]
    public class LayerData
    {
        public int layer_id;
        public List<string> tiles = new List<string>();
        public List<int> poses_x = new List<int>();
        public List<int> poses_y = new List<int>();

        public LayerData(int id)
        {
            layer_id = id;
        }
    }
}