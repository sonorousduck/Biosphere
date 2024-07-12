using Biosphere.Managers;
using Microsoft.Xna.Framework;
using SequoiaEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biosphere
{
    public class SelectedItemCursorScript : Script
    {
        public SelectedItemCursorScript(GameObject go) : base(go) { }

        private Transform transform;

        public override void Start()
        {
            transform = gameObject.GetComponent<Transform>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 test = gameObject.GetParent().GetComponent<Transform>().position;
            Debug.WriteLine(test);
            Vector2 test1 = GridWorldManager.GetPositionOfGrid(test);

            transform.position = GridWorldManager.GetPositionOfGrid(test) ;

            //transform.position = Vector2.Zero; 

            
        }
    }
}
