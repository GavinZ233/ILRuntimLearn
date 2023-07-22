using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson13 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Instance.StartILRuntime(() =>
        {

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int TestFun(int i,int j)
    {
        return i + j;
    }
}
