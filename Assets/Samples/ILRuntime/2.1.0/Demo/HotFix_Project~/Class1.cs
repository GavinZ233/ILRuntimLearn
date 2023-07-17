﻿using System;
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
        public int TestFun2(int i)
        {
            Debug.Log("有参成员方法" + i);
            return i + 10;
        }
    }


}
