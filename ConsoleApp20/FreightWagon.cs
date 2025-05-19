using System;

namespace TrainWagons
{
    public class FreightWagon : Wagon
    {
        private string target;
        private int tonnage;

        public string Target
        {
            get => target;
            set => target = string.IsNullOrEmpty(value)
                ? throw new ArgumentException("Назначение не может быть пустым")
                : value;
        }

        public int Tonnage
        {
            get => tonnage;
            set => tonnage = value > 0 ? value : throw new ArgumentException("Тоннаж должен быть положительным");
        }

        public FreightWagon() : base()
        {
            Target = "Общее";
            Tonnage = 50;
        }

        public FreightWagon(int number, int minSpeed, string target, int tonnage) : base(number, minSpeed)
        {
            Target = target;
            Tonnage = tonnage;
        }

        

        public override void Show() =>
            Console.WriteLine(
                $"Грузовой вагон №{Number}, Максимальная скорость: {MinSpeed} км/ч, Назначение: {Target}, Тоннаж: {Tonnage}");

        public override void Init()
        {
            base.Init();
            Console.Write("Введите назначение: ");
            Target = Console.ReadLine();
            Console.Write("Введите тоннаж: ");
            Tonnage = int.Parse(Console.ReadLine());
        }

        public void ShowVirtual()
        {
            Console.WriteLine($"Грузовой вагон №{Number}, ID: {Id}, Минимальная скорость: {MinSpeed}, Тоннаж: {Tonnage}");
        }

        public override void RandomInit(Random rnd)
        {
            base.RandomInit(rnd);
            string[] purposes = { "Уголь", "Зерно", "Нефть", "Общее" };
            Target = purposes[rnd.Next(purposes.Length)];
            Tonnage = rnd.Next(20, 100);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            FreightWagon other = (FreightWagon)obj;
            return Target == other.Target && Tonnage == other.Tonnage;
        }

        public override string ToString() =>
            $"Грузовой вагон №{Number}, Максимальная скорость: {MinSpeed} км/ч, Назначение: {Target}, Тоннаж: {Tonnage}";
    }
}