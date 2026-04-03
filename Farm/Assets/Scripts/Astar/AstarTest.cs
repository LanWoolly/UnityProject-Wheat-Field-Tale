using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Farm.Astar
{
    public class AstarTest : MonoBehaviour
    {
        private AStar aStar;
        [Header("用于测试")]
        public Vector2Int startPos;
        public Vector2Int finishPos;
        public Tilemap disPlayMap;
        public TileBase disPlayTile;
        public bool displayStartAndFinish;
        public bool displayPath;

        private Stack<MovementStep> npcMovementStepStack;

        [Header("测试移动NPC")]
        public NPCMovement npcMovement;
        public bool moveNpc;
        [SceneName] public string targetScene;
        public Vector2Int targetPos;
        public AnimationClip stopClip;

        private void Awake()
        {
            aStar = GetComponent<AStar>();
            npcMovementStepStack = new Stack<MovementStep>();
        }

        private void Update()
        {
            ShowPathOnGridMap();

            if (moveNpc)
            {
                var schedule = new ScheduleDetails(0, 0, 0, 0, Season.春天, targetScene, targetPos, stopClip, true);
                npcMovement.BuildPath(schedule);
                moveNpc = false;
            }
        }

        private void ShowPathOnGridMap()
        {
            if (disPlayMap != null && disPlayTile != null)
            {
                if (displayStartAndFinish)
                {
                    disPlayMap.SetTile((Vector3Int)startPos, disPlayTile);
                    disPlayMap.SetTile((Vector3Int)finishPos, disPlayTile);

                }
                else
                {
                    disPlayMap.SetTile((Vector3Int)startPos, null);
                    disPlayMap.SetTile((Vector3Int)finishPos, null);
                }

                if (displayPath)
                {
                    npcMovementStepStack.Clear();
                    var sceneName = SceneManager.GetActiveScene().name;
                    aStar.BuildPath(sceneName, startPos, finishPos, npcMovementStepStack);

                    foreach (var step in npcMovementStepStack)
                    {
                        disPlayMap.SetTile((Vector3Int)step.gridCoordinate, disPlayTile);
                        Debug.Log(step);
                    }
                }
                else
                {
                    if (npcMovementStepStack.Count > 0)
                    {
                        foreach (var step in npcMovementStepStack)
                        {
                            disPlayMap.SetTile((Vector3Int)step.gridCoordinate, null);
                        }
                        npcMovementStepStack.Clear();
                    }
                }
            }
        }
    }
}
