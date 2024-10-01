using System;
using Microsoft.Xna.Framework;

namespace Sprint4BeanTeam
{
    public class CollisionDetector
    {
        public enum CollisionFrom
        {
            Left, Right, Top, Bottom, None
        }

        public static CollisionFrom DetectCollision(Rectangle gameObject1, Rectangle gameObject2)
        {

            Rectangle overlap = Rectangle.Intersect(gameObject1, gameObject2);
            if (overlap.IsEmpty || overlap.Width * overlap.Height <= 1)
            {
                return CollisionFrom.None;
            }

                int relativePosition = overlap.Height - overlap.Width;
                //remember that Width Goes L->R, but Height Goes T->B

                if (relativePosition > 0)
                {
                    if (gameObject1.X < gameObject2.X)
                        return CollisionFrom.Right;
                    else
                        return CollisionFrom.Left;
                }
                else if (relativePosition < 0)
                {
                    if (gameObject1.Y < gameObject2.Y)
                        return CollisionFrom.Bottom;
                    else
                        return CollisionFrom.Top;
                }

            return CollisionFrom.None;
        }
    }
}

