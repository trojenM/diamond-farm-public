using DorudonGames.Runtime.Misc;
using UnityEngine;

namespace DorudonGames.Runtime.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        private void Start()
        {
            Application.targetFrameRate = CommonTypes.DEFAULT_FPS;
        }
    }
}
