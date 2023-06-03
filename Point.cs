using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_4
{
    public class Point
    {
        static public int maxValue = 999999999;
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int Value { get; set; }
        public int Index { get; set; }
        public Point prev;
        public List<Rebro> Rebra { get; set; }

        public Point()
        {
            IsChecked = false;
            Value = maxValue;
            Rebra = new List<Rebro>();
        }
        public Point(string name, int index) : this()
        {
            Name = name;
            Index = index;
        }
        public Point(bool isChecked, int value)
        {
            IsChecked = isChecked;
            Value = value;
        }
        public Point(string name, bool isChecked, int value) : this(isChecked, value)
        {
            Name = name;
        }
        public override string ToString()
        {
            return $"({Name})";
        }
    }
}
