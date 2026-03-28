using Farm.Map;
using UnityEngine;

namespace Farm.CropPlant
{
    public class CropGenerator : MonoBehaviour
    {
        private Grid currentGrid;
        public int seedItemID;
        public int growthDays;

        private void Awake()
        {
            currentGrid = FindFirstObjectByType<Grid>();
        }

        private void OnEnable()
        {
            EventHandler.GenerateCropEvent += GenerateCrop;
        }

        private void OnDisable()
        {
            EventHandler.GenerateCropEvent -= GenerateCrop;
        }
        private void GenerateCrop()
        {
            Vector3Int cropGridPos = currentGrid.WorldToCell(transform.position);
            Debug.Log(cropGridPos);

            if (seedItemID != 0)
            {
                var tile = GirdMapManager.Instance.GetTileDetailsOnMousePosition(cropGridPos);

                if (tile == null)
                {
                    tile = new TileDetails();
                }
                tile.girdX = cropGridPos.x;
                tile.gridY = cropGridPos.y;
                tile.daysSinceWatered = -1;
                tile.seedItemID = seedItemID;
                tile.growthDays = growthDays;

                GirdMapManager.Instance.UpdateTileDetails(tile);
            }
        }
    }

}
