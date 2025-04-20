using System.Collections;

namespace TrainWagons
{
    public class MinSpeedComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Wagon wx = x as Wagon;
            Wagon wy = y as Wagon;
            if (wx == null || wy == null) throw new ArgumentException("Объекты должны быть вагонами");
            return wx.MinSpeed.CompareTo(wy.MinSpeed);
        }
    }
}