using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Instance.StartILRuntime(() =>
        {
            ILRuntime.Runtime.Enviorment.AppDomain appDomain = ILRuntimeMgr.Instance.appDomain;
            IType type = appDomain.LoadedTypes["HotFix_Project.Lesson3_IL"];
            object obj = ((ILType)type).Instantiate(new object[] { "wqe" });
            print(obj);

            //方法名，入参个数
            IMethod getStr =type.GetMethod("get_Str", 0);
            IMethod setStr = type.GetMethod("set_Str", 1);

            //方法，执行方法的类，传参
            string str =appDomain.Invoke(getStr, obj).ToString();
            print(str);

            //设置属性
            appDomain.Invoke(setStr, obj,"321");
            str = appDomain.Invoke(getStr, obj).ToString();

            print(str);

            //第二种获取属性方法，更解约性能，无GC
            using(var method=appDomain.BeginInvoke(getStr))
            {
                method.PushObject(obj);//传入执行方法的对象
                method.Invoke();
                str=method.ReadValueType<string>();
                print(str);
            }
            using(var method=appDomain.BeginInvoke(setStr))
            {
                method.PushObject(obj);
                string tempStr = "333";
                method.PushValueType<string>(ref tempStr);
                method.Invoke();
            }
            using (var method = appDomain.BeginInvoke(getStr))
            {
                method.PushObject(obj);//传入执行方法的对象
                method.Invoke();
                str = method.ReadValueType<string>();
                print(str);
            }

            //TODO:独立获取一遍属性
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
