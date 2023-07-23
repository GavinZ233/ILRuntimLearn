using ILRuntime.CLR.Method;
using ILRuntime.CLR.Utils;
using ILRuntime.Mono.Cecil.Pdb;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntimeAdapter;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ILRuntimeMgr : MonoBehaviour
{
    private static ILRuntimeMgr instance;
    private MemoryStream dllStream;
    private MemoryStream pdbStream;
    private bool isStart=false;

    public AppDomain appDomain;
    public static ILRuntimeMgr Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("ILRuntimeMgr");
                instance = obj.AddComponent<ILRuntimeMgr>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }
    /// <summary>
    /// 用于启动ILRuntime初始化方法
    /// </summary>
    public void StartILRuntime(UnityAction callBack=null)
    {
        if (!isStart)
        {
            isStart = true;
            appDomain = new AppDomain();
            StartCoroutine(LoadHotUpdateInfo(callBack));
        }
    }
    /// <summary>
    /// 停止热更
    /// </summary>
    public void StopILRuntime()
    {
        if (dllStream != null)
            dllStream.Close();
        if (pdbStream != null)
            pdbStream.Close();

        dllStream = null;
        pdbStream = null;

        appDomain = null;

        isStart = false;
    }


    /// <summary>
    /// 初始化ILRuntime相关内容
    /// </summary>
    private unsafe void InitILRuntime()
    {
        ////初始化其他
        //appDomain.DelegateManager.RegisterDelegateConvertor<MyUnityDel1>((act) =>
        //{
        //    return new MyUnityDel1(() =>
        //    {
        //        ((System.Action)act)();
        //    });
        //});
        //注册委托
        appDomain.DelegateManager.RegisterFunctionDelegate<System.Int32, System.Int32, System.Int32>();
        ////注册委托转换器
        //appDomain.DelegateManager.RegisterDelegateConvertor<MyUnityDel2>((act) =>
        //{
        //    return new MyUnityDel2((i,j) =>
        //    {
        //        return((System.Func<System.Int32,System.Int32,System.Int32>)act)(i,j);
        //    });
        //});
        //注册跨域继承适配器
        appDomain.RegisterCrossBindingAdaptor(new Lesson11_TestAdapter());
        //注册携程继承适配器
        appDomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());


        //CLR重定向内容，要卸载clr绑定之前
        System.Type debugType=typeof(Debug);
        //得到要重定向方法的方法信息
        MethodInfo methodInfo =debugType.GetMethod("Log",new System.Type[] {typeof(object)});
        appDomain.RegisterCLRMethodRedirection(methodInfo, MyLog);

        //注册CLR绑定
        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appDomain);

        //初始化ILRuntime相关信息，告知主线程的ID，为了能够在unity的profiler分析问题
        appDomain.UnityMainThreadID = Thread.CurrentThread.ManagedThreadId;

    }

    unsafe StackObject* MyLog(ILIntepreter __intp, StackObject* __esp, List<object>  __mStack, CLRMethod __method, bool isNewObj)
    {
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
        StackObject* ptr_of_this_method;
        StackObject* __ret = ILIntepreter.Minus(__esp, 1);

        ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
        System.Object @message = (System.Object)typeof(System.Object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (ILRuntime.CLR.Utils.Extensions.TypeFlags)0);
        __intp.Free(ptr_of_this_method);

        //获取堆栈信息
        var stackTrace = __domain.DebugService.GetStackTrace(__intp);

        UnityEngine.Debug.Log(message + "\n" + stackTrace);

        return __ret;
    }
    /// <summary>
    /// 热更初始化完毕后，执行的内容
    /// </summary>
    private void ILRuntimeLoadOverDoSomething()
    {
        //执行热更代码的逻辑
        print("准备热更");

    }



    /// <summary>
    /// 异步加载热更新的dll和pbd文件
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadHotUpdateInfo(UnityAction callBack)
    {
#if UNITY_ANDROID
        UnityWebRequest request = UnityWebRequest.Get(Application.streamingAssetsPath + "/HotFix_Project.dll");
#else
        UnityWebRequest requestdll = UnityWebRequest.Get("file:///" + Application.streamingAssetsPath + "/HotFix_Project.dll");

#endif
        yield return requestdll.SendWebRequest();
        //如果失败
        if (requestdll.result != UnityWebRequest.Result.Success)
        {
            print("加载DLL文件失败" + requestdll.responseCode + requestdll.result);
        }

        //读取加载的DLL数据
        byte[] dllbytes = requestdll.downloadHandler.data;
        requestdll.Dispose();



#if UNITY_ANDROID
        UnityWebRequest requestpdb = UnityWebRequest.Get(Application.streamingAssetsPath + "/HotFix_Project.dll");
#else
        UnityWebRequest requestpdb = UnityWebRequest.Get("file:///" + Application.streamingAssetsPath + "/HotFix_Project.pdb");

#endif
        yield return requestpdb.SendWebRequest();
        //如果失败
        if (requestpdb.result != UnityWebRequest.Result.Success)
        {
            print("加载PDB文件失败" + requestpdb.responseCode + requestpdb.result);
        }

        //读取加载的pbd数据
        byte[] pbdbytes = requestpdb.downloadHandler.data;
        requestpdb.Dispose();

        //将加载的数据以流的形式传递给AppDomain对象中
        dllStream = new MemoryStream(dllbytes);
        pdbStream = new MemoryStream(pbdbytes);
        //将两个文件的内存流用于初始化appdomain
        appDomain.LoadAssembly(dllStream, pdbStream, new PdbReaderProvider());

        InitILRuntime();

        ILRuntimeLoadOverDoSomething();

        //当ILRuntime启动完毕后，想要在外部执行的内容
        callBack?.Invoke();
    }


}
