using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    class GameObjectDirector
    {
        private IGOBuilder builder;

        public GameObjectDirector(IGOBuilder builder)
        {
            this.builder = builder;
        }

        public GameObject Construct()
        {
            builder.BuildGameObject();

            return builder.GetResult(); 
        }
    }
}
