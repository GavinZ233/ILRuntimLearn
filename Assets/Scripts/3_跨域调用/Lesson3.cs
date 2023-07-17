using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Lesson3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ILRuntimeMgr.Instance.StartILRuntime(() =>
        {
            ILRuntime.Runtime.Enviorment.AppDomain ad =ILRuntimeMgr.Instance.appDomain;
            //此时初始化的类，并不能用类名装载，因为在编译器中已经把他当作一个自定义类，只能用object
            object obj=ad.Instantiate("HotFix_Project.Lesson3_IL");
            print(obj);

            obj = ad.Instantiate("HotFix_Project.Lesson3_IL",new object[] {"qwe"});
            print(obj);

            //appdomain中loadedtypers字典获取itype类型，强转为iltype后调用instantiate方法
            IType type = ad.LoadedTypes["HotFix_Project.Lesson3_IL"];
            obj=((ILType)type).Instantiate();
            print("itype："+type);

            obj = ((ILType)type).Instantiate(new object[] {"adsa"});
            print("itype有参构造：" + type);

            //得到IType对象，在得到构造函数实例化
            ConstructorInfo info  =type.ReflectionType.GetConstructor(new Type[0]);
            obj=info.Invoke(null); 
            print(obj);

            info=type.ReflectionType.GetConstructor(new Type[] {typeof(string)});
            obj=info.Invoke(new object[] {"sadasd"});
            print(obj);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
