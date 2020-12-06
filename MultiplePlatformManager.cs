using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePlatformManager : MonoBehaviour
{
    [SerializeField]
    protected GameObject platformToSpawn;
    [SerializeField]
    protected enum Direction { Clockwise, CounterClockwise }
    [SerializeField]
    protected Direction direction;
    [SerializeField]
    protected bool spawnPlatformsAtPath;
    [SerializeField]
    protected float speed;
    [Tooltip("This is the paths the platforms will follow; if spawnPlatformsAtPath not checked, the platforms will all spawn at each path point")]
    public List<Vector2> numberOfPaths = new List<Vector2>();
    [Tooltip("If you want to setup custom locations on the path for the platforms, you set the initial position of each platform here")]
    public List<Vector2> platformSpawns = new List<Vector2>();
    [Tooltip("If you want to setup custom locations on the path for the platforms, you set the initial index on the path of each platform here; if the platform is in between two paths, the greater of the two is where it would move to for clockwise")]
    public List<int> platformSpawnNextIndex = new List<int>();
    protected List<GameObject> platforms = new List<GameObject>();

    protected virtual void Start()
    {
        if (spawnPlatformsAtPath)
        {
            for (int i = 0; i < numberOfPaths.Count; i++)
            {
                platforms.Add(Instantiate(platformToSpawn, numberOfPaths[i], Quaternion.identity));
                platforms[i].transform.position = new Vector3(platforms[i].transform.position.x, platforms[i].transform.position.y, 0);
                platforms[i].GetComponent<MoveablePlatform>().index = i;
                CheckPosition(platforms[i]);
            }
        }
        else
        {
            for (int i = 0; i < platformSpawns.Count; i++)
            {
                platforms.Add(Instantiate(platformToSpawn, platformSpawns[i], Quaternion.identity));
                platforms[i].transform.position = new Vector3(platforms[i].transform.position.x, platforms[i].transform.position.y, 0);
                if (direction == Direction.Clockwise)
                {
                    platforms[i].GetComponent<MoveablePlatform>().index = platformSpawnNextIndex[i];
                }
                else
                {
                    int next = platformSpawnNextIndex[i] - 1;
                    if(next < 0)
                    {
                        next = numberOfPaths.Count - 1;
                    }
                    platforms[i].GetComponent<MoveablePlatform>().index = next;

                }
                CheckPosition(platforms[i]);
            }
        }

    }

    protected virtual void LateUpdate()
    {
        foreach (GameObject platform in platforms)
        {
            MovePlatform(platform);
        }
    }

    protected virtual void MovePlatform(GameObject platform)
    {
        Vector3 position = new Vector3(numberOfPaths[platform.GetComponent<MoveablePlatform>().index].x, numberOfPaths[platform.GetComponent<MoveablePlatform>().index].y, 0);
        platform.transform.position = Vector2.MoveTowards(platform.transform.position, position, Time.deltaTime * speed);
        if (platform.transform.position == position)
        {
            CheckPosition(platform);
        }
    }

    protected virtual void CheckPosition(GameObject platform)
    {
        if (direction == Direction.Clockwise)
        {
            platform.GetComponent<MoveablePlatform>().index++;
            if (platform.GetComponent<MoveablePlatform>().index == numberOfPaths.Count)
            {
                platform.GetComponent<MoveablePlatform>().index = 0;
            }
        }
        else
        {
            platform.GetComponent<MoveablePlatform>().index--;
            if (platform.GetComponent<MoveablePlatform>().index == -1)
            {
                platform.GetComponent<MoveablePlatform>().index = numberOfPaths.Count - 1;
            }
        }
    }
}