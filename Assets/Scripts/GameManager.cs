using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public int gridWidth = 20;
    public int gridHeight = 20;
    void Start()
    {
        SpawnFood();
    }

   public void  SpawnFood()
    {
        int x = Random.Range(-gridWidth / 2, gridWidth / 2) * 1;
        int y = Random.Range(-gridHeight / 2, gridHeight / 2) * 1;
        Instantiate(foodPrefab,new Vector2(x,y),Quaternion.identity);
    }
    
}
