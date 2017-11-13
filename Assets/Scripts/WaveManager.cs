using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public bool activate = false;
    public int waveToSpawn = 1;
    public GameObject enemyPrefab;
    public List<Wave> waves;
    public List<GameObject> enemyList;


	// Use this for initialization
	void Start () {
        enemyList = null;
	}

    // Update is called once per frame
    void Update () {
		if (activate == true)
        {
            SpawnWave(waveToSpawn);
        }
	}

    public void SpawnWave(int waveNumber)
    {
        if (waveNumber - 1 >= waves.Count)
        {
            Debug.Log("Error: specified wave not defined.");
            activate = false;

        }
        else
        {
            foreach (WaveData data in waves[waveNumber - 1].waveData)
            {
                var tempEnemy = Instantiate(data.enemyPrefabToSpawn, data.locationToSpawn, Quaternion.identity);
                enemyList.Add(tempEnemy);
            }
            activate = false;
        }
    }
}

[System.Serializable]
public class WaveData
{
    public GameObject enemyPrefabToSpawn;
    public Vector3 locationToSpawn;
}

[System.Serializable]
public class Wave
{
    public List<WaveData> waveData;
}
