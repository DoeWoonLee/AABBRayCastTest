using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AABBRayCastTest
{
    class MyDraw
    {
        private Pen pen = new Pen(Color.Black);
        private Brush brush = new SolidBrush(Color.Green);

        private Point m_StartPt;
        private Point m_Mouse;
        private bool m_HasClick = false;
        private Rectangle m_Rectangle = new Rectangle(150,140, 500,50);

        float m_t = 0f;
        float m_dirX = 0f;
        float m_dirY = 0f;
        bool m_HasCollision = false;
        public void Render(Graphics g)
        {
            g.DrawRectangle(pen, m_Rectangle);

            if(m_HasClick)
            {
                g.DrawLine(pen, m_StartPt, m_Mouse);

                if(m_HasCollision)
                {
                    float x = m_StartPt.X + m_dirX * m_t;
                    float y = m_StartPt.Y + m_dirY * m_t;

                    g.DrawEllipse(pen, x - 3, y - 3, 6, 6);
                }
            }
        }
        void Swap(ref float f1, ref float f2)
        {
            float temp = f1;
            f1 = f2;
            f2 = temp;
        }
        public void RayCheck()
        {
            m_HasCollision = false;
            if (m_StartPt == m_Mouse)
                return;

            float dtX = m_Mouse.X - m_StartPt.X;
            float dtY = m_Mouse.Y - m_StartPt.Y;

            float length = (float)Math.Sqrt((double)(dtX * dtX + dtY * dtY));
            float dirX = dtX / length;
            float dirY = dtY / length;

            m_dirX = dirX;
            m_dirY = dirY;

            float originX = m_StartPt.X;
            float originY = m_StartPt.Y;

            float rcpX = 1.0f / dirX;
            float rcpY = 1.0f / dirY;

            float bl = m_Rectangle.X;
            float bt = m_Rectangle.Y;
            float br = m_Rectangle.X + m_Rectangle.Width;
            float bb = m_Rectangle.Y + m_Rectangle.Height;


            float tmin = (bl - originX) * rcpX;
            float tmax = (br - originX) * rcpX;
            float tymin = (bt - originY) * rcpY;
            float tymax= (bb - originY) * rcpY;

            if(dirX < 0f)
            {
                Swap(ref tmin, ref tmax);
            }
            if(dirY < 0f)
            {
                Swap(ref tymin, ref tymax);
            }

            if ((tmin > tymax) || (tymin > tmax))
            {
                return;
            }
            if (tymin > tmin)
                tmin = tymin;
            if (tymax < tmax)
                tmax = tymax;

            m_t = tmin;
            m_HasCollision = true;
        }
        public Point StartPt
        {
            set
            {
                m_HasClick = true;
                m_StartPt = value;
            }
        }
        public Point Mouse
        {
            set
            {
                m_Mouse = value;
            }
        }
    }
}
