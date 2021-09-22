using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] GameObject boxPrefab;

    public void SpawnBox()
    {
        Vector3 temp = transform.position;
        temp.z = 0;
        GameObject box_Obg = Instantiate(boxPrefab,temp,Quaternion.identity);
        
    }
    void Start()
    {
      //  SpawnBox();
    }

  
}
