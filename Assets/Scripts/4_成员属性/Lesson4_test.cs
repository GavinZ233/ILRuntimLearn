using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson4_test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Instance.StartILRuntime(() =>
        {
            AppDomain appDomain=ILRuntimeMgr.Instance.appDomain;

            //自行访问热更项目中的类，成员属性
            IType type = appDomain.LoadedTypes["HotFix_Project.Lesson3_IL"];
            object obj= ((ILType)type).Instantiate(new object[] {"new l3"});

            IMethod getstr=type.GetMethod("get_Str",0);
            IMethod setstr = type.GetMethod("set_Str", 1);

            string str =null;

            using(var method = appDomain.BeginInvoke(getstr))
            {
                method.PushObject(obj);
                method.Invoke();
                str=method.ReadValueType<string>();
                print("读取:"+str);
            }
            using(var method=appDomain.BeginInvoke(setstr))
            {
                method.PushObject(obj);
                string str1 = "重新写入的内容";
                method.PushValueType<string>(ref str1);
                method.Invoke();
            }
            using (var method = appDomain.BeginInvoke(getstr))
            {
                method.PushObject(obj);
                method.Invoke();
                str = method.ReadValueType<string>();
                print("读取:" + str);
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
