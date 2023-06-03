using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_4
{
    public class Rebro
    {
        public Point FirstPoint { get; set; }
        public Point SecondPoint { get; set; }
        public int Value { get; set; }

        public Rebro()
        {
            FirstPoint = null;
            SecondPoint = null;
            Value = 99999999;
        }

        public Rebro(Point firstPoint, Point secondPoint, int value)
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            Value = value;
        }
        public Point anotherPoint(Point p)
        {
            return p == FirstPoint ? SecondPoint : FirstPoint;
        }

    }
}
