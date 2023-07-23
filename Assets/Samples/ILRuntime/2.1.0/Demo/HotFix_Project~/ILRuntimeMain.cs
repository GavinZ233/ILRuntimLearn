using System.Collections;
using UnityEngine;


namespace HotFix_Project
{
    public delegate void MyILRuntimeDel1();
    public delegate int MyILRuntimeDel2(int i, int j);

    class ILRuntimeMain
    {



        /// <summary>
        /// 把逻辑控制权交给热更工程
        /// 启动函数
        /// </summary>
        public static void Start()
        {
            //lesson9 il调用unity
            //GameObject obj = new GameObject("ILRuntime创建物体");
            //obj.transform.position = new Vector3(10, 10, 10);
            //Debug.Log(obj.transform.position);

            ////obj.AddComponent<render>
            //AudioSource audio=obj.AddComponent<AudioSource>();
            //audio.loop = true;
            //audio.playOnAwake = false;
            //audio.name = "测试用AS";
            //audio.enabled = false;


            //Lesson10 lesson10 = Camera.main.GetComponent<Lesson10>();

            //lesson10
            //MyUnityDel1 fun=Fun1;
            //fun();

            //MyUnityDel2 fun2 = Fun2;
            //int re = fun2(3,6);
            //Debug.Log(re);

            //获取unity中的委托
            //lesson10.fun1 = Fun1;
            //lesson10.fun2 = fun2;
            //lesson10.fun1();
            //lesson10.fun2(1,1);

            //lesson10.action = Fun1;
            //lesson10.funAction = Fun2;
            //lesson10.action();
            //int reInt=lesson10.funAction(1, 2);
            //Debug.Log("反参:"+reInt);


            //MyILRuntimeDel1 funil1 = Fun1;
            //MyILRuntimeDel2 funil2 = Fun2;
            //funil1();
            //int funil2int = funil2(4,5);
            //Debug.Log("il内部委托反参" + funil2int);


            //跨域继承
            //TestLesson11 testLesson11 = new TestLesson11();
            //testLesson11.TestFun("IL内调用");
            //testLesson11.TestAbstract(3);
            //testLesson11.valuePublic = 99;
            //Debug.Log(testLesson11.valuePublic);


            //System.DateTime currentTime = System.DateTime.Now;
            //for (int i = 0; i < 100000; i++)
            //{
            //    Lesson13.TestFun(i, i);
            //}
            //Debug.Log("花费时间：" + (System.DateTime.Now - currentTime).TotalMilliseconds + "ms");


            //GameObject monoContent = new GameObject();
            //ILRuntimeMono ilmono=monoContent.AddComponent<ILRuntimeMono>();
            //ilmono.startEvent += (() =>
            //{
            //    Debug.Log("start");

            //});
            //ilmono.updateEvent += (() =>
            //{
            //    Debug.Log("update");
            //});


            //Lesson16
            Lesson16 lesson=Camera.main.GetComponent<Lesson16>();
            lesson.StartCoroutine(Lesson16Test());
        }

        public static IEnumerator Lesson16Test()
        {
            for (int i = 0; i < 6; i++)
            {
                Debug.Log(i);
                yield return new WaitForSeconds(1f);

            }

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
