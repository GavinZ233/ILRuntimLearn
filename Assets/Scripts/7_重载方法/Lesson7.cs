using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson7 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Instance.StartILRuntime(() =>
        {
            AppDomain appDomain = ILRuntimeMgr.Instance.appDomain;

            IType type = appDomain.LoadedTypes["HotFix_Project.Lesson3_IL"];
            object obj= ((ILType)type).Instantiate();

            appDomain.Invoke("HotFix_Project.Lesson3_IL", "TestFun",obj);//无参
            appDomain.Invoke("HotFix_Project.Lesson3_IL", "TestFun", obj,3);//int参数
            //appDomain.Invoke("HotFix_Project.Lesson3_IL", "TestFun", obj,"重载内容");//string参数
            //参数数量相同，但是类型不同时，使用方法名可能报错，因为不能明确调用的方法


            IMethod method1 = type.GetMethod("TestFun", 0);
            IMethod method2 = type.GetMethod("TestFun", 1);

            appDomain.Invoke(method1, obj);
            appDomain.Invoke(method2, obj,3);


            //对appdomain传入目标类型，得到itype的改类型
            IType stringType =appDomain.GetType(typeof(string));
            //放入参数列表中，将获取到的itype放入list
            List<IType> list = new List<IType>();
            list.Add(stringType);

            method2 =type.GetMethod("TestFun",list,null);

            using (var method=appDomain.BeginInvoke(method2))
            {
                method.PushObject(obj);
                string str = "dsaf";
                method.PushValueType<string>(ref str);
                method.Invoke();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
