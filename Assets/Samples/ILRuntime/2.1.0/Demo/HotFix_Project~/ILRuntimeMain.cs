using UnityEngine;


namespace HotFix_Project
{
    class ILRuntimeMain
    {
        /// <summary>
        /// 把逻辑控制权交给热更工程
        /// 启动函数
        /// </summary>
        public static void Start()
        {
            //lesson9 il调用unity
            GameObject obj = new GameObject("ILRuntime创建物体");
            obj.transform.position = new Vector3(10, 10, 10);
            Debug.Log(obj.transform.position);

            //obj.AddComponent<render>
        }
    }
}
