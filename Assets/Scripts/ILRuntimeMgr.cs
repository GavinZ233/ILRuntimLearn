using ILRuntime.Mono.Cecil.Pdb;
using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.IO;
using System.Threading;
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
    private void InitILRuntime()
    {
        //初始化ILRuntime相关信息，告知主线程的ID，为了能够在unity的profiler分析问题
        appDomain.UnityMainThreadID = Thread.CurrentThread.ManagedThreadId;

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
