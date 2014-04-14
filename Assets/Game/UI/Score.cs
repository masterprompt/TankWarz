using System;
using System.Collections.Generic;
using UnityEngine;
using Engine;
namespace Game
{
    public class Score : MonoBehaviour
    {
        #region Fields
        public UIWidget panel;
        public UILabel title;
        public UILabel score;
        public int index;
        #endregion

        #region Monobehaviour
        public void Update()
        {
            if (index >= Player.list.Count)
            {
                panel.alpha = 0;
                return;
            }
            var player = Player.list[index];
            panel.alpha = 1;
            title.text = player.name;
            score.text = player.score.ToString();
        }
        #endregion



    }
}
