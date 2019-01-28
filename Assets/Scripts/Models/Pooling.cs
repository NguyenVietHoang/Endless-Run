using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public delegate void OnPoolingEvent();
    public OnPoolingEvent OnPoolingReady;

    //The prefeab for instantiate the pool
    public GameObject objPrefab;
    //The position that we put the object when it was spawned
    public Transform spawnPivot;
    [HideInInspector]
    public Stack<GameObject> objs;
    //Set this param before initialize
    int poolingNb;
    public bool PoolingReady {get; private set;}  

    //Call this function before all Start function to ensure all 
    //delegate was reset
    private void Awake()
    {
        OnPoolingReady = null;
        PoolingReady = false;
    }

    //Initialize the pool
    public IEnumerator Preload(int nb)
    {
        objs = new Stack<GameObject>();
        poolingNb = nb;

        for (int i = 0; i < nb; i++)
        {
            GameObject tmp = Instantiate(objPrefab);
            tmp.SetActive(false);
            tmp.transform.position = spawnPivot.transform.position;
            objs.Push(tmp);

            yield return null;
        }
        
        //Notify the other that the pool is ready
        if(OnPoolingReady != null)
        {
            OnPoolingReady();
            OnPoolingReady = null;
        }
        PoolingReady = true;
    }

    //pop the obj for using in the game
    virtual public GameObject Spawn()
    {
        if(objs != null && objs.Count > 0 && PoolingReady)
        {
            GameObject tmp = objs.Pop();
            tmp.SetActive(true);
            tmp.transform.position = spawnPivot.position;
            return tmp;
        }
        else
        {
            return null;
        }
    }

    //Deactivate the obj and repush it into the pool 
    virtual public void Despawn(GameObject obj)
    {
        if (objs != null && PoolingReady)
        {
            obj.SetActive(false);
            obj.transform.position = spawnPivot.position;
            objs.Push(obj);
        }            
    }
}
