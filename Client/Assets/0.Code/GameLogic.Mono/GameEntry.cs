using UnityEngine;

namespace GameLogic.Mono
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        private void Start()
        {
            InitBuiltinComponents();
            InitCustomComponents();
        }
    }
}
