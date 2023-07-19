using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson8 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Instance.StartILRuntime(() =>
        {
            AppDomain appDomain = ILRuntimeMgr.Instance.appDomain;//拿到appdomain，
            IType type = appDomain.LoadedTypes["HotFix_Project.Lesson3_IL"];//从appdomain的已加载类中找到指定类型
            object obj=((ILType)type).Instantiate();  //实例化该类型

            //TODO:任务11-1  04：10  


        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
