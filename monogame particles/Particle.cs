using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame_particles
{
    class Particle
    {
        public Vector2 position;
        public Vector2 velocity;
        int maxX;
        int maxY;
        public Color color;
        int boundWidth = 0;
        public int group;
        public Particle(Vector2 position, Color color, int maxX, int maxY, int group)
        {
            this.position = position;
            this.color = color;
            this.maxX = maxX;
            this.maxY = maxY;
            this.group = group;
        }
        public Particle(Color color, int maxX, int maxY)
        {
            position = new Vector2(10, 10);
            this.color = color;
            this.maxX = maxX;
            this.maxY = maxY;
        }
        public void Update(Vector2 cursorPos, bool mouseDown, Particle[] particles)
        {
            Console.WriteLine(position.ToString());
            //Attract(cursorPos, mouseDown);
            foreach(Particle p in particles)
            {
                if(p.group == group)
                {
                    velocity += Attract(p.position);
                }
            }
            velocity *= .975f;
            position += velocity;
            if (position.X < boundWidth)
            {
                position.X = boundWidth;
                velocity.X *= -1;
            }
            else if (position.X >= maxX - boundWidth)
            {
                position.X = maxX - boundWidth - 1;
                velocity.X *= -1;
            }
            if (position.Y < boundWidth)
            {
                position.Y = boundWidth;
                velocity.Y *= -1;
            }
            else if (position.Y >= maxY - boundWidth)
            {
                position.Y = maxY - boundWidth - 1;
                velocity.Y *= -1;
            }
        }

        Vector2 Attract(Vector2 pos)
        {
            Vector2 d = position - pos;
            var angle = Math.Atan2(d.Y, d.X);
            Vector2 acceleration = new Vector2();
            /*acceleration.X = (1 / d.Length()) * (float)Math.Cos(angle);
            acceleration.Y = (1 / d.Length()) * (float)Math.Sin(angle);*/
            acceleration = d;
            acceleration.Normalize();
            return acceleration;
        }

        Vector2 AttractCursor(Vector2 cursorPos, bool mouseDown)
        {
            Vector2 d = position - cursorPos;
            var angle = Math.Atan2(d.Y, d.X);
            Vector2 acceleration = new Vector2();
            if (d.Length() > 10 && d.Length() < 300)
            {
                if (!mouseDown)
                {
                    acceleration.X = (20 / d.Length()) * (float)Math.Cos(angle);
                    acceleration.Y = (20 / d.Length()) * (float)Math.Sin(angle);
                    return acceleration;
                }
                else
                {
                    acceleration.X = (250 / d.Length()) * (float)Math.Cos(angle);
                    acceleration.Y = (250 / d.Length()) * (float)Math.Sin(angle);
                    return acceleration;
                }
            }
            return acceleration;
        }
    }
}
