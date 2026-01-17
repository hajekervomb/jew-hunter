using UnityEngine;

public class WindowSetup : MonoBehaviour
{
    void Awake()
    {
        // Устанавливаем разрешение 960x540 и оконный режим
        // false означает "не полный экран" (то есть оконный)
        Screen.SetResolution(960, 540, FullScreenMode.Windowed);
    }
}