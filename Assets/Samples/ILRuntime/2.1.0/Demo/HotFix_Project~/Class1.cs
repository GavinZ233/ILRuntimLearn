using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    class Lesson3_IL
    {
        private string str;
        public string Str { get { return str; } set { str = value; } }
        public Lesson3_IL()
        {

        }
        public Lesson3_IL(string str)
        {
            this.str = str;
        }

        //Lesson5静态方法
        public static void TestStaticFun()
        {
            Debug.Log("无参静态方法");
        }

        public static int TestStaticFun2(int i)
        {
            Debug.Log("有参静态方法" + i);
            return i + 10;
        }

        public void TestFun()
        {
            Debug.Log("无参成员方法");

        }

        public void TestFun(int i)
        {
            Debug.Log("重载函数int "+i);

        }

        public void TestFun(string str)
        {
            Debug.Log("重载函数str " + str);

        }
        public int TestFun2(int i)
        {
            Debug.Log("有参成员方法" + i);
            return i + 10;
        }

        public float TestFun3(int i,ref List<int> list,out float f)
        {
            f = 0.5f;
            list.Add(5);
            for (int j = 0; j < list.Count; j++)
            {
                Debug.Log(list[j]);
            }

            return i + list.Count + f;
        }
    }


}
