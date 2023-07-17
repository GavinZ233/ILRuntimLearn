using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson6 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Instance.StartILRuntime(() => {
            AppDomain appDomain = ILRuntimeMgr.Instance.appDomain;

            IType type = appDomain.LoadedTypes["HotFix_Project.Lesson3_IL"];
            object obj=((ILType)type).Instantiate();

            appDomain.Invoke("HotFix_Project.Lesson3_IL","TestFun",obj,null);

            int i=(int)appDomain.Invoke("HotFix_Project.Lesson3_IL", "TestFun2", obj, 3);
            print(i);

            IMethod method1 = type.GetMethod("TestFun", 0);
            IMethod method2 = type.GetMethod("TestFun2", 1);

            appDomain.Invoke(method1,obj); 
            i=(int)appDomain.Invoke(method2,obj,233);
            print(i);


            using(var method = appDomain.BeginInvoke(method1))
            {
                method.PushObject(obj);
                method.Invoke();
            }
            using(var method=appDomain.BeginInvoke(method2))
            {
                method.PushObject(obj);
                method.PushInteger(111);
                method.Invoke();
                i=method.ReadInteger();
                print("using获得int："+i);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
