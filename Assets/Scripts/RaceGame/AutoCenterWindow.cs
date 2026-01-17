using UnityEngine;
using System;
using System.Runtime.InteropServices; // Нужен для работы с Windows

public class AutoCenterWindow : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    // Импортируем функции Windows для управления окнами
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    private const uint SWP_NOSIZE = 0x0001; // Флаг: не менять размер
    private const uint SWP_NOZORDER = 0x0004; // Флаг: не менять порядок окон
#endif

    // Желаемое разрешение
    public int width = 960;
    public int height = 540;

    void Start()
    {
        // 1. Сначала применяем разрешение
        Screen.SetResolution(width, height, FullScreenMode.Windowed);

        // 2. Центрируем окно (только для Windows билдов)
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        CenterWindow();
#endif
    }

    void CenterWindow()
    {
        // Получаем разрешение монитора пользователя
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;

        // Вычисляем координаты левого верхнего угла для центра
        // Формула: (ШиринаЭкрана - ШиринаОкна) / 2
        int x = (screenWidth - width) / 2;
        int y = (screenHeight - height) / 2;

        // Получаем "ручку" (ID) нашего окна
        IntPtr windowHandle = GetActiveWindow();

        // Двигаем окно в координаты X, Y
        SetWindowPos(windowHandle, IntPtr.Zero, x, y, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
    }
}