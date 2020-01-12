using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using ThreadPriority = System.Threading.ThreadPriority;

public class LevelHandler : MonoBehaviour
{
    private readonly Stack<Chain> chainList = new Stack<Chain>();
    private Stack<Chain> assembledChainList;

    private Cell[] cells;

    private Thread chainCollectorThread;
    private Thread checkFinishedChainsThread;

    private int cropCount;
    private int finishCellId;
    private int minTurns = int.MaxValue;
    private int startCellId;

    private bool start;
    private DateTime startTime;
    [SerializeField] private Texture2D texture2D; 
    public Texture2D[] texture2DArray;

    public void DisableTest()
    {
        start = false;
    }

    public void StartTest()
    {
        CreateAndFillGrid(texture2D);
    }

    private void CreateAndFillGrid(Texture2D levelTexture)
    {
        start = true;
        startTime = DateTime.Now;
        minTurns = int.MaxValue;
        Debug.LogError("Start time: " + startTime);
        assembledChainList = new Stack<Chain>();
        cropCount = 0;
        cells = new Cell[levelTexture.width * levelTexture.height];
        var cellsDictionary = new Dictionary<Vector2Int, Cell>();
        Color cellColor;
        var counter = 0;

        for (var i = 0; i < levelTexture.width; i++)
        {
            for (var j = levelTexture.height - 1; j >= 0; j--)
            {
                var tmpCell = new Cell(counter, new Vector2Int(i, j));

                cellColor = levelTexture.GetPixel(i, j);

                switch (cellColor.ToHexString())
                {
                    case "FFD800":
                        tmpCell.IsBusy = true;
                        cellsDictionary.Add(tmpCell.Position, tmpCell);
                        cropCount++;
                        cells[counter] = tmpCell;
//                            Debug.LogError("yellow");
                        break;
                    case "00FF00":
                        startCellId = counter;
                        cells[counter] = tmpCell;
                        cellsDictionary.Add(tmpCell.Position, tmpCell);

//                            Debug.LogError("green");
                        break;
                    case "000000":

//                            Debug.LogError("black");
                        break;
                    case "FF0000":
                        finishCellId = counter;
                        cells[counter] = tmpCell;
                        cellsDictionary.Add(tmpCell.Position, tmpCell);
//                            Debug.LogError("red");
                        break;
                    case "FFFFFF":
                        cells[counter] = tmpCell;
                        cellsDictionary.Add(tmpCell.Position, tmpCell);
//                            Debug.LogError("white");
                        break;
                    default:
                        Debug.LogError($"No definition found {cellColor.ToHexString()}");
                        break;
                }

                counter++;
            }
        }


        foreach (var cell in cells)
        {
            if (cell != null)
            {
                var neighbors = new List<int>();
                var newPos = cell.Position + Vector2Int.up;
                if (cellsDictionary.ContainsKey(newPos))
                {
                    neighbors.Add(cellsDictionary[newPos].Id);
                }

                newPos = cell.Position + Vector2Int.right;
                if (cellsDictionary.ContainsKey(newPos))
                {
                    neighbors.Add(cellsDictionary[newPos].Id);
                }

                newPos = cell.Position + Vector2Int.down;
                if (cellsDictionary.ContainsKey(newPos))
                {
                    neighbors.Add(cellsDictionary[newPos].Id);
                }

                newPos = cell.Position + Vector2Int.left;
                if (cellsDictionary.ContainsKey(newPos))
                {
                    neighbors.Add(cellsDictionary[newPos].Id);
                }

                cell.NeighborIDs = neighbors;
            }
        }

        var chain = new Chain(startCellId, new HashSet<int>(), new List<int>(), cells);
        chainList.Push(chain);
        chainCollectorThread = new Thread(ChainCollectorRoutine) {Priority = ThreadPriority.Highest};
        checkFinishedChainsThread = new Thread(ChainCheckerRoutine) {Priority = ThreadPriority.Lowest};

        chainCollectorThread.Start();
        checkFinishedChainsThread.Start();
    }


    private void ChainCollectorRoutine()
    {
        while (chainList.Count > 0)
        {
            if (!start)
            {
                print("ABORT. Min turns::" + minTurns);
                return;
            }

            var tmp = chainList.Pop();
            if (minTurns > tmp.numberOfTurns)
            {
                CheckChain(tmp);
            }
        }

        Debug.LogError("End time: " + DateTime.Now + " MinTurns::" + minTurns);
    }

    private void CheckChain(Chain chain)
    {
//        Debug.LogWarning("Worked");
        if (chain.currentCellId == finishCellId)
        {
            lock (assembledChainList)
            {
                assembledChainList.Push(chain);
                if (checkFinishedChainsThread.IsAlive == false)
                {
                    checkFinishedChainsThread = new Thread(() => ChainCheckerRoutine());
                    checkFinishedChainsThread.Priority = ThreadPriority.Lowest;
                    checkFinishedChainsThread.Start(); // запускаем поток
                }
            }
        }
        else
        {
            foreach (var neighborId in cells[chain.currentCellId].NeighborIDs)
            {
                if (!chain.CheckCellInChain(neighborId) && minTurns > chain.numberOfTurns)
                {
                    chainList.Push(new Chain(neighborId, chain.UsedCellsHashSet, chain.UsedCellsList, cells,
                        chain.numberOfTurns));
                }
            }
        }
    }


    private void ChainCheckerRoutine()
    {
        lock (assembledChainList)
        {
            while (assembledChainList.Count > 0)
            {
                if (!start)
                {
                    print("ABORT. Min turns::" + minTurns);
                    return;
                }

                var chain = assembledChainList.Pop();
                CheckAssembledChain(chain);
            }
        }
    }

    private void CheckAssembledChain(Chain chain)
    {
        var counter = 0;
        var turns = 0;
        var direction = Vector2Int.zero;
        var lastPos = Vector2Int.zero;


        foreach (var variableCell in chain.UsedCellsList.ToArray())
        {
            if (lastPos == Vector2Int.zero)
            {
                lastPos = cells[variableCell].Position;
            }
            else
            {
                if (direction == Vector2Int.zero)
                {
                    direction = lastPos - cells[variableCell].Position;
                }
            }

            var tmp = lastPos - cells[variableCell].Position;
            if (tmp != direction)
            {
                turns++;
                direction = tmp;
            }


            lastPos = cells[variableCell].Position;
        }

        foreach (var cellId in chain.UsedCellsList)
        {
            if (cells[cellId].IsBusy)
            {
                counter++;
            }
        }

        if (counter == cropCount)
        {
            //Debug.LogError("Собрали весь урожай. Поворотов " + turns);
            if (minTurns > turns)
            {
                minTurns = turns;
            }

//            var workTime = DateTime.Now - startTime;
//            Debug.LogError("Work time: " + workTime);
        }
        else
        {
            //Debug.LogError("Собрали не весь урожай. ");
        }
    }

    public void CreateLevelAssets()
    {
        foreach (var texture in texture2DArray)
        {
            Level asset = ScriptableObject.CreateInstance<Level>();
            asset.levelTexturePath = AssetDatabase.GetAssetPath(texture);
            var str = asset.levelTexturePath.Replace("Assets/Content/Resources/Textures/Maps/", "");
            str = str.Replace(".png", "");
            if (asset.levelTexture == null)
            {
                asset.levelTexture = texture;
            }

            EditorUtility.SetDirty(asset);
            AssetDatabase.CreateAsset(asset, "Assets/Content/Resources/Levels/level_" + str + ".asset");

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}

public struct Chain
{
    public int numberOfTurns;
    public int currentCellId;
    public HashSet<int> UsedCellsHashSet;
    public List<int> UsedCellsList;

    public bool CheckCellInChain(int id)
    {
        return UsedCellsHashSet.Contains(id);
    }

    private void AddCell(int id)
    {
        UsedCellsHashSet.Add(id);
        UsedCellsList.Add(id);
        currentCellId = id;
    }

    public Chain(int id, HashSet<int> usedCellsHashSet, List<int> usedCellsList, Cell[] cells, int turns = 0) : this()
    {
        numberOfTurns = turns;
        UsedCellsHashSet = new HashSet<int>(usedCellsList);
        UsedCellsList = new List<int>(usedCellsList);

        if (usedCellsList.Count >= 3)
        {
            if (cells[usedCellsList[usedCellsList.Count - 1]].Position -
                cells[usedCellsList[usedCellsList.Count - 2]].Position !=
                cells[usedCellsList[usedCellsList.Count - 2]].Position -
                cells[usedCellsList[usedCellsList.Count - 3]].Position)
            {
                numberOfTurns++;
            }
        }

        AddCell(id);
    }
}

public class Cell
{
    public readonly int Id;
    public bool IsBusy;
    public List<int> NeighborIDs;
    public Vector2Int Position;

    public Cell(int id, Vector2Int position)
    {
        Id = id;
        Position = position;
        NeighborIDs = new List<int>();
    }
}

public enum Directions
{
    Top = 0,
    Left = 1,
    Right = 2,
    Bottom = 3
}