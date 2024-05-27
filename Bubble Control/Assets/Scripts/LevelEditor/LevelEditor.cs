using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

namespace LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        public static LevelEditor Instance { get; private set; }

        [SerializeField] Tilemap defaultTilemap;
        Tilemap currentTilemap
        {
            get
            {
                if (LevelManager.Instance.layers.TryGetValue((int)LevelManager.Instance.tiles[_selectedTileIndex].tilemap, out Tilemap tilemap))
                {
                    return tilemap;
                }
                else
                {
                    return defaultTilemap;
                }
            }
        }
        TileBase currentTile
        {
            get
            {
                return LevelManager.Instance.tiles[_selectedTileIndex].tile;
            }
        }

        [SerializeField] int _selectedTileIndex;


        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            SetMouseTile();
        }
        private void Update()
        {
            Vector3Int pos = currentTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //place tile with left click
            if (Input.GetMouseButton(0)) PlaceTile(pos);
            //delete tile with right click
            if (Input.GetMouseButton(1)) DeleteTile(pos);
        }

        void SetMouseTile()
        {
            MouseEditor.Instance.SetMouseIcon(GetSpriteFromTileBase(currentTile));
        }
        Sprite GetSpriteFromTileBase(TileBase tileBase)
        {
            if (tileBase is Tile tile)
            {
                return tile.sprite;
            }
            else if (tileBase is RuleTile ruleTile)
            {
                // Assuming you want the first sprite from the RuleTile
                return ruleTile.m_TilingRules.FirstOrDefault()?.m_Sprites?.FirstOrDefault();
            }
            // Handle other types of TileBase if necessary
            return null;
        }
        /// <summary>
        /// Place down the current tile on the current tilemap at pos
        /// </summary>
        /// <param name="pos"></param>
        void PlaceTile(Vector3Int pos)
        {
            currentTilemap.SetTile(pos, currentTile);
        }

        /// <summary>
        /// Delete the tile on the current tilemap at pos
        /// </summary>
        /// <param name="pos"></param>
        void DeleteTile(Vector3Int pos)
        {
            currentTilemap.SetTile(pos, null);
        }
    }
}