using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FlyGenerator : MonoBehaviour
{
    public GameObject flyPrefab;
    public Queue<Fly> flies;
    
    // 蚊子生成间隔
    private float generateDelTime;
    // 蚊子飞入时间
    private float flyInTime;
    // 蚊子吸血准备时间
    public float flyReadyTime { get; private set; }

    private float generateTime;
    private int fliesNum;
    private int maxNum;
    private List<Vector3> startPosList;
    private List<Vector3> stopPosList;
    private float width;
    private float height;

    public GameSwitch gameManager;

    void Start()
    {
        // 数值在这里设置
        generateDelTime = 3f;
        flyInTime = 2f;
        flyReadyTime = .8f;
        
        width = 10f;
        height = 14f;
        generateTime = 0f;
        fliesNum = 0;
        flies = new Queue<Fly>();
        startPosList = new List<Vector3>();
        stopPosList = new List<Vector3>();
        GenerateStartPos(width, height, 50);
        GenerateStopPos(width, height, 1.0f, 50);
        maxNum = stopPosList.Count;
    }


    void Update()
    {
        generateTime += Time.deltaTime;
        if (gameManager.gameTime < 30f) 
        {
            generateDelTime = Random.Range(2f, 4f);
            flyInTime = 3f;
        }
        else if (gameManager.gameTime < 60f)
        {
            generateDelTime = Random.Range(1f,2f);
            flyInTime = 1f;
        }
        else
        {
            generateDelTime = Random.Range(0.5f, 0.8f);
            flyInTime = 1f;
        }

        if (generateTime > generateDelTime)
        {
            generateTime = 0f;
            Generate(Random.Range(1,3));
        }
    }

    private void Generate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            fliesNum++;
            int idx = fliesNum % maxNum;
            Fly fly = Instantiate(flyPrefab, startPosList[idx] + transform.position, Quaternion.identity).GetComponent<Fly>();
            fly.flyInTime = flyInTime;
            fly.flyReadyTime = flyReadyTime;
            flies.Enqueue(fly);
            fly.inPos = startPosList[idx] + transform.position;
            fly.stopPos = stopPosList[idx] + transform.position;
            fly.curvePos = fly.inPos + fly.stopPos;
        }
    }

    private void GenerateStartPos(float width, float height, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int direction = Random.Range(0, 4);
            Vector3 pos;
            switch (direction)
            {
                case 0:
                    pos = new Vector3(0f, Random.Range(0, height), 0);
                    startPosList.Add(pos);
                    break;
                case 1:
                    pos = new Vector3(width, Random.Range(0, height), 0);
                    startPosList.Add(pos);
                    break;
                case 2:
                    pos = new Vector3(Random.Range(0, width), 0, 0);
                    startPosList.Add(pos);
                    break;
                case 3:
                    pos = new Vector3(Random.Range(0, width), height, 0);
                    startPosList.Add(pos);
                    break;
            }
        }
    }

    private void GenerateStopPos(float width, float height, float minDist, int count)
    {
        float cellSize = minDist / Mathf.Sqrt(2);
        int cols = Mathf.FloorToInt(width / cellSize);
        int rows = Mathf.FloorToInt(height / cellSize);
        Vector3?[,] grid = new Vector3?[rows, cols];

        float x = width / 2;
        float y = height / 2;
        Vector3 firstPos = new Vector3(x, y, 0);
        grid[Mathf.FloorToInt(y / cellSize), Mathf.FloorToInt(x / cellSize)] = firstPos;
        stopPosList.Add(firstPos);
        
        for (int total = 1; total < count; total++)
        {
            int randIdx = Random.Range(0, stopPosList.Count);
            Vector3 pos = stopPosList[randIdx];
            bool found = false;
            for (int n = 0; n < 30; n++)
            {
                Vector3 sample = generateRandomPointAround(pos, minDist);
                int col = Mathf.FloorToInt(sample.x / cellSize);
                int row = Mathf.FloorToInt(sample.y / cellSize);
                if (0 <= col && col < cols && 0 <= row && row < rows && grid[row, col] == null)
                {
                    bool ok = true;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (0 <= col + i && col + i < cols && 0 <= row + j && row + j < rows)
                            {
                                Vector3? neighbor = grid[row + j, col + i];
                                if (neighbor != null)
                                {
                                    float d = Vector3.Distance(sample, (Vector3)neighbor);
                                    if (d < minDist)
                                        ok = false;
                                }
                            }
                        }
                    }
                    if (ok)
                    {
                        found = true;
                        grid[row, col] = sample;
                        stopPosList.Add(sample);
                        break;
                    }
                }
            }
            if (!found)
                stopPosList.Remove(pos);
        }

    }
    private Vector3 generateRandomPointAround(Vector3 point, float minDist)
    {
        float r1 = Random.Range(0f, 1f);
        float r2 = Random.Range(0f, 1f);
        //random radius between mindist and 2 * mindist
        float radius = minDist * (r1 + 1);
        //random angle
        float angle = 2 * Mathf.PI * r2;
        //the new point is generated around the point (x, y)
        float newX = point.x + radius * Mathf.Cos(angle);
        float newY = point.y + radius * Mathf.Sin(angle);
        return new Vector3(newX, newY, 0);
    }
}
