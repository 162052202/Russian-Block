using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spwaner : MonoBehaviour
{
    public GameObject[] groups;


    // Start is called before the first frame update
    void Start()
    {
        Spwanernext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spwanernext()
    {
        int i = Random.Range(0,groups.Length);
        GameObject ins = Instantiate(groups[i], transform.position, Quaternion.identity) as GameObject;
    }
    
}
