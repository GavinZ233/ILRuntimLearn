using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Instance.StartILRuntime(() => {
            AppDomain appDomain = ILRuntimeMgr.Instance.appDomain;

            //调用热更新中类的静态方法
            //方法一，用Appdomain的Invoke直接调用
            appDomain.Invoke("HotFix_Project.Lesson3_IL", "TestStaticFun", null,null);

            int i=(int)appDomain.Invoke("HotFix_Project.Lesson3_IL", "TestStaticFun2", null, 3);
            print(i);

            //方法二，使用IMethod调用
            IType type = appDomain.LoadedTypes["HotFix_Project.Lesson3_IL"];
            IMethod method1 = type.GetMethod("TestStaticFun", 0);
            IMethod method2 = type.GetMethod("TestStaticFun2", 1);

            appDomain.Invoke(method1,null);
            appDomain.Invoke(method2,null,2);

            //方法三，用过using
            using (var method=appDomain.BeginInvoke(method1))
            {
                method.Invoke();
            }
            using (var method = appDomain.BeginInvoke(method2))
            {
                method.PushInteger(32);
                method.Invoke();
                i=method.ReadInteger();
                print("返回的数字:" + i);

            }

        });

    }

}
