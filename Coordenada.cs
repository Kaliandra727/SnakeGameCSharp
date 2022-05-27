using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Coordenada
    {

        public int x { get; set; }

        public int y { get; set; }
        public Coordenada(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
