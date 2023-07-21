using ILRuntime.Runtime.Enviorment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MyUnityDel1();
public delegate int MyUnityDel2(int i,int j);

public class Lesson10 : MonoBehaviour
{
    public MyUnityDel1 fun1;
    public MyUnityDel2 fun2;

    public Action action;
    public Func<int, int, int> funAction;
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Instance.StartILRuntime(() => {
            ILRuntime.Runtime.Enviorment.AppDomain appDomain = ILRuntimeMgr.Instance.appDomain;
            appDomain.Invoke("HotFix_Project.ILRuntimeMain","Start",null,null);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
