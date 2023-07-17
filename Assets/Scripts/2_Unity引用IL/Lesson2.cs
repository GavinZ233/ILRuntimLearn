using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ILRuntime.Runtime.Enviorment;
using System.IO;
using UnityEngine.Networking;
using ILRuntime.Mono.Cecil.Pdb;
using ILRuntime.Mono.Cecil.Mdb;
using System.Threading;

public class Lesson2 : MonoBehaviour
{
    private AppDomain appDomain;

    private MemoryStream dllStream;
    private MemoryStream pdbStream;
    // Start is called before the first frame update
    void Start()
    {
        appDomain = new AppDomain();

        StartCoroutine(LoadHotUpdateInfo());
    }
    /// <summary>
    /// 异步加载热更新的dll和pbd文件
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadHotUpdateInfo()
    {
#if UNITY_ANDROID
        UnityWebRequest request = UnityWebRequest.Get(Application.streamingAssetsPath + "/HotFix_Project.dll");
#else
        UnityWebRequest request = UnityWebRequest.Get("file:///"+Application.streamingAssetsPath + "/HotFix_Project.dll");

#endif
        yield return request.SendWebRequest();
        //如果失败
        if (request.result!=UnityWebRequest.Result.Success)
        {
            print("加载DLL文件失败" + request.responseCode + request.result);
        }

        //读取加载的DLL数据
        byte[] dll = request.downloadHandler.data;
        request.Dispose();



#if UNITY_ANDROID
        UnityWebRequest requestpbd = UnityWebRequest.Get(Application.streamingAssetsPath + "/HotFix_Project.dll");
#else
        UnityWebRequest requestpbd = UnityWebRequest.Get("file:///" + Application.streamingAssetsPath + "/HotFix_Project.pdb");

#endif
        yield return requestpbd.SendWebRequest();
        //如果失败
        if (requestpbd.result != UnityWebRequest.Result.Success)
        {
            print("加载PBD文件失败" + requestpbd.responseCode + requestpbd.result);
        }

        //读取加载的pbd数据
        byte[] dllpbd = requestpbd.downloadHandler.data;
        requestpbd.Dispose();

        //将加载的数据以流的形式传递给AppDomain对象中
        dllStream = new MemoryStream(dll);
        pdbStream = new MemoryStream(dllpbd);
        //将两个文件的内存流用于初始化appdomain
        appDomain.LoadAssembly(dllStream, pdbStream, new PdbReaderProvider());

        //初始化ILRuntime相关信息，告知主线程的ID，为了能够在unity的profiler分析问题
        appDomain.UnityMainThreadID=Thread.CurrentThread.ManagedThreadId;

        //执行热更代码的逻辑
        print("准备热更");
    }

    private void OnDestroy()
    {
        if (dllStream!=null)
        {
            dllStream.Close();
        }
        if (pdbStream !=null)
        {
            pdbStream.Close();
        }

        dllStream = null;
        pdbStream = null;

        appDomain=null;
    }
}
