using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2_Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Instance.StartILRuntime(() => {
            print("在mian里得到il加载完毕");    
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
