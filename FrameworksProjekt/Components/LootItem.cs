using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Components;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework.Content;

namespace FrameworksProjekt.Components
{
    class LootItem : Component , ILoadable, IUpdateable, IOnCollisionStay
    {

        public LootItem(GameObject go) : base(go)
        {

        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void OnCollisionStay(Collider other)
        {
            
        }

        public void Update()
        {
            
        }
    }
}
