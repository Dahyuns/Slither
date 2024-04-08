using UnityEngine;
using System;

namespace WiggleQuest
{
    /// <summary>
    /// �̺�Ʈ�� Ŭ����
    /// </summary>
    public class ResetGame : MonoBehaviour
    {
        //�̺�Ʈ
        public static event Action Reset;

        public static void PerformReset()
        {
            Reset?.Invoke();
        }
    }
}