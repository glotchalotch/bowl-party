using System.Collections;
using System.Collections.Generic;
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using UnityEngine;
using Valve.VR;

public static class InVRCheck
{
#if !UNITY_EDITOR
#if UNITY_STANDALONE_WIN
    [DllImport("user32.dll")]
    static extern int MessageBox(System.IntPtr hwnd, string lpText, string lpCaption, uint uType);
    [DllImport("user32.dll")]
    static extern System.IntPtr GetActiveWindow();
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void CheckForVR()
    {
        IEnumerator<bool> e = CheckInitialization();
        while (e.Current) e.MoveNext();
        if (SteamVR.instance == null)
        {
#if UNITY_STANDALONE_WIN
            MessageBox(GetActiveWindow(),
                "SteamVR (and/or the VR headset) is not detected or is not working correctly.\n\nIf SteamVR is launched and seems to be working correctly and you're still getting this error, let me know.",
                "SteamVR Not Detected",
                (uint)(0x00000000L | 0x00000010L));
            //this is ripped from this gist https://gist.github.com/roydejong/130a91e1835154a3acaeda78c9dfbbd7 i have no idea what the uint does lol
#endif
            Application.Quit(1);
        }
    }

    static IEnumerator<bool> CheckInitialization()
    {
        while (SteamVR.initializedState == SteamVR.InitializedStates.None || SteamVR.initializedState == SteamVR.InitializedStates.Initializing)
            yield return true;
    }
#endif
}
