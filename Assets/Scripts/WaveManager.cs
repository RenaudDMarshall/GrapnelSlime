using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public GameObject enemyPrefab;
    public List<WaveData> waveOne;
    public List<GameObject> enemyList;
    public bool activate = false;

	// Use this for initialization
	void Start () {
        enemyList = null;

        waveOne[0].enemyPrefabToSpawn = enemyPrefab;
        waveOne[0].locationToSpawn = new Vector3(6, 3, -1);
	}
	
	// Update is called once per frame
	void Update () {
		if (activate == true)
        {
            SpawnWave(1);
        }
	}

    public void SpawnWave(int waveNumber)
    {
        switch (waveNumber)
        {
            case 1:
                foreach (WaveData data in waveOne)
                {
                    var tempEnemy = Instantiate(data.enemyPrefabToSpawn, data.locationToSpawn, Quaternion.identity);
                    enemyList.Add(tempEnemy);
                }
                activate = false;
                break;
            default:
                break;
        }
    }
}

[System.Serializable]
public class WaveData
{
    public GameObject enemyPrefabToSpawn;
    public Vector3 locationToSpawn;
}
