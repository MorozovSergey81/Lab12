using System;

namespace TrainWagons
{
    public class RestaurantWagon : Wagon
    {
        private string operatingHours;

        public string OperatingHours
        {
            get => operatingHours;
            set => operatingHours = string.IsNullOrEmpty(value)
                ? throw new ArgumentException("Режим работы не может быть пустым")
                : value;
        }

        public RestaurantWagon() : base()
        {
            OperatingHours = "08:00-22:00";
        }

        public RestaurantWagon(int number, int minSpeed, string operatingHours) : base(number, minSpeed)
        {
            OperatingHours = operatingHours;
        }

        public override void Show() =>
            Console.WriteLine($"Вагон-ресторан №{Number}, Максимальная скорость: {MinSpeed} км/ч, Режим работы: {OperatingHours}");

        public override void Init()
        {
            base.Init();
            Console.Write("Введите режим работы: ");
            OperatingHours = Console.ReadLine();
        }

        public void ShowVirtual()
        {
            Console.WriteLine($"Вагон-ресторан №{Number}, ID: {Id}, Минимальная скорость: {MinSpeed}");
        }

        public override void RandomInit(Random rnd)
        {
            base.RandomInit(rnd);
            OperatingHours = $"{rnd.Next(6, 12):D2}:00-{rnd.Next(18, 24):D2}:00";
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            RestaurantWagon other = (RestaurantWagon)obj;
            return OperatingHours == other.OperatingHours;
        }

        public override string ToString() =>
            $"Вагон-ресторан №{Number}, Максимальная скорость: {MinSpeed} км/ч, Режим работы: {OperatingHours}";
    }
}