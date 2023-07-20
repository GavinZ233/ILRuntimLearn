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
            AudioSource audio=obj.AddComponent<AudioSource>();
            audio.loop = true;
            audio.playOnAwake = false;
            audio.name = "测试用AS";
            audio.enabled = false;

            //lesson10
            MyUnityDel1 fun=Fun1;
            fun();

            MyUnityDel2 fun2 = Fun2;
            int re = fun2(3,6);
            Debug.Log(re);

            //获取unity中的委托
            Lesson10 lesson10 = Camera.main.GetComponent<Lesson10>();
            lesson10.fun1 = Fun1;
            lesson10.fun2 = fun2;
            lesson10.fun1();
            lesson10.fun2(1,1);

        }

        public static void Fun1()
        {
            Debug.Log("IL_Fun1");
        }

        public static int Fun2(int a,int b)
        {
            Debug.Log("IL_Fun2");
            return a + b;
        }
    }
}
