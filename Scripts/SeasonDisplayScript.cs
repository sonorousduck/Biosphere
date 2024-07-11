using Microsoft.Xna.Framework;
using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosphere
{
    public class SeasonDisplayScript : Script
    {

        public SeasonDisplayScript(GameObject gameObject) : base(gameObject) { }

        private Text text;

        public override void Start()
        {
            text = gameObject.GetComponent<Text>();
        }

        public override void Update(GameTime gameTime)
        {
            text.text = TimeManager.Instance.CurrentSeason.ToString();
        }
    }
}
