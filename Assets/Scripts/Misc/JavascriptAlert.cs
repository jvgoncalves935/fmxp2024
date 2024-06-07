using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public static class JavascriptAlert
{
    [DllImport("__Internal")]
    private static extern void StackOverflowExceptionMessage();

    public static void Call() {
        StackOverflowExceptionMessage();
    }
}
