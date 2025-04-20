using System;

namespace TrainWagons
{
    public partial class Wagon : IInit, IComparable, ICloneable
    {
        private int number;
        private int minSpeed;

        public int Number
        {
            get => number;
            set => number = value >= 0 ? value : throw new ArgumentException("Номер не может быть отрицательным");
        }

        public int MinSpeed
        {
            get => minSpeed;
            set => minSpeed = value > 0 ? value : throw new ArgumentException("Минимальная скорость должна быть положительной");
        }

        public IdNumber Id { get; set; } = new IdNumber(0);

        public Wagon()
        {
            Number = 1;
            MinSpeed = 100;
            Id = new IdNumber(0);
        }

        public Wagon(int number, int minSpeed)
        {
            Number = number;
            MinSpeed = minSpeed;
            Id = new IdNumber(number);
        }

        public Wagon(Wagon other)
        {
            Number = other.Number;
            MinSpeed = other.MinSpeed;
            Id = new IdNumber(other.Id.Number);
        }

        public virtual void Show() => Console.WriteLine($"Вагон №{Number}, Минимальная скорость: {MinSpeed} км/ч");

        public virtual void Init()
        {
            Console.Write("Введите номер вагона: ");
            Number = int.Parse(Console.ReadLine());
            Console.Write("Введите максимальную скорость: ");
            MinSpeed = int.Parse(Console.ReadLine());
            Id = new IdNumber(Number);
        }

        public virtual void RandomInit()
        {
            throw new NotImplementedException();
        }

        public virtual void RandomInit(Random rnd)
        {
            Number = rnd.Next(1, 1000);
            MinSpeed = rnd.Next(50, 200);
            Id = new IdNumber(Number);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Wagon other = (Wagon)obj;
            return Number == other.Number && MinSpeed == other.MinSpeed && Id.Equals(other.Id);
        }

        public override string ToString() => $"Вагон №{Number}, Максимальная скорость: {MinSpeed} км/ч";

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (!(obj is Wagon other)) throw new ArgumentException("Объект не является вагоном");
            return this.Number.CompareTo(other.Number);
        }

        public object Clone()
        {
            Wagon clone = (Wagon)MemberwiseClone();
            clone.Id = new IdNumber(Id.Number);
            return clone;
        }

        public Wagon ShallowCopy() => (Wagon)MemberwiseClone();
    }
}