using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lesson11_Test 
{
    public int valuePublic;
    protected int valueProtected;

    public virtual int ValuePor
    {
        get;
        set;
    }

    public virtual void TestFun(string str)
    {
        Debug.Log("TestFun:"+str);
    }


    public abstract void TestAbstract(int value);
}
