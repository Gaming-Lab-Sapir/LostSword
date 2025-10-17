public static class AppQuitter
{
#if UNITY_EDITOR
    public static void Quit() => UnityEditor.EditorApplication.isPlaying = false;
#else
    public static void Quit() => UnityEngine.Application.Quit();
#endif
}

