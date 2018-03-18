using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RandomObject : MonoBehaviour {
	public List<GameObject> map;

    public float startX;
	public List<Transform> players;
	public float distanceMap;
    public PlayScript playScript;

    private int currentPlatformIndex = -1;
	private float spawnHalfWidth;
    private int checkTrap;

	private List<GameObject> platformGroundPool = new List<GameObject>();
    private void Awake()
    {
        
    }
    private void Start()
	{
        CreateNewPlatformIfNeeded();

	}
    private void Update()
    {
        players[0].position = playScript.player1.transform.position;
        players[1].position = playScript.player2.transform.position;
        CreateNewPlatformIfNeeded();
        //144
        foreach (GameObject platform in platformGroundPool)
        {
            if (platform.transform.position.x < Camera.main.transform.position.x - 2 * distanceMap)
            {
                CreateNewPlatformIfNeeded();
                platform.SetActive(false);
            }
        }
    }

    private GameObject GetNewPlatformGround()
	{
		//foreach(GameObject platform in platformGroundPool)
		//{
		//	if (!platform.activeInHierarchy)
		//	{
		//		return platform;
		//	}
  //      }
        int x = Random.Range(0, map.Count); 
        if (currentPlatformIndex ==0) { x = 0; }
        GameObject newPlatform = Instantiate(
			map[x],
			Vector3.zero,
			Quaternion.identity
		);
		platformGroundPool.Add(newPlatform);
		return newPlatform;
	}

    private void CreateNewPlatformIfNeeded()
	{
		while (players.Max((player) => player.position.x) > startX + currentPlatformIndex * distanceMap - distanceMap)
		{
			GameObject newPlatformGround = GetNewPlatformGround();
            newPlatformGround.transform.position = new Vector3(currentPlatformIndex * distanceMap, -80 + Random.Range(40, 70) , 1); // y la day camera (-90).
            newPlatformGround.SetActive(true);
            //RandomGround(newPlatformGround);
            //newPlatformGround.transform.localScale = new Vector3(0.1f, Random.Range(0.3f, 0.7f), 1);
            currentPlatformIndex++;

        }
	}
	private void RandomGround(GameObject newMap)
    {
        Transform[] grounds = newMap.GetComponentsInChildren<Transform>();
        foreach (Transform ground in grounds)
        {
            ground.transform.localScale = new Vector3(0.4f, Random.Range(0.3f, 0.7f), 1);
        }
    }
}
