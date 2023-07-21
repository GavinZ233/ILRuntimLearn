using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace HotFix_Project
{
    class TestLesson11 : Lesson11_Test
    {
        public override int ValuePor { 
            get => valueProtected ;
            set => valueProtected = value; }


        public override void TestFun(string str)
        {
            base.TestFun(str);
            Debug.Log("热更工程中TestFun：" + str);
        }
        public override void TestAbstract(int value)
        {
            Debug.Log("热更工程中TestAbstract："+value);
        }
    }
}
