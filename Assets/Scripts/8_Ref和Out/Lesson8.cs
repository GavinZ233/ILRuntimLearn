using ILRuntime.CLR.Method;
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

            IMethod methodName = type.GetMethod("TestFun3",3);
            

            List<int> list = new List<int>() { 1,2,3,4};
            using(var method = appDomain.BeginInvoke(methodName))
            {
                //需要先压入ref和out 
                method.PushObject(list); //压入第一个ref的数据
                method.PushObject(null); //压入第二个ref的数据

                //再压入对象和参数
                method.PushObject(obj);
                method.PushInteger(100);

                //压入ref和out的索引
                method.PushReference(0);
                method.PushReference(1);

                //执行
                method.Invoke();

                //通过read按顺序读取ref，out,返回值最后获取
                list=method.ReadObject<List<int>>(0);
                float f =method.ReadFloat(1);
                float reValue=method.ReadFloat();

                print("列表数量："+list.Count+"浮点数"+f+"返回值"+reValue);
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
