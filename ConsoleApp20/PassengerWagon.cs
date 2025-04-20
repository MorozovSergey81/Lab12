using System;

namespace TrainWagons
{
    public class PassengerWagon : Wagon
    {
        private int sleepingPlaces;
        private int seats;

        public int SleepingPlaces
        {
            get => sleepingPlaces;
            set => sleepingPlaces = value >= 0 ? value : throw new ArgumentException("Количество спальных мест не может быть отрицательным");
        }

        public int Seats
        {
            get => seats;
            set => seats = value >= 0 ? value : throw new ArgumentException("Количество сидячих мест не может быть отрицательным");
        }

        public PassengerWagon() : base()
        {
            SleepingPlaces = 20;
            Seats = 30;
        }

        public PassengerWagon(int number, int minSpeed, int sleepingPlaces, int seats) : base(number, minSpeed)
        {
            SleepingPlaces = sleepingPlaces;
            Seats = seats;
        }

        public override void Show() =>
            Console.WriteLine($"Пассажирский вагон №{Number}, Максимальная скорость: {MinSpeed} км/ч, Спальных мест: {SleepingPlaces}, Сидячих мест: {Seats}");

        public override void Init()
        {
            base.Init();
            Console.Write("Введите количество спальных мест: ");
            SleepingPlaces = int.Parse(Console.ReadLine());
            Console.Write("Введите количество сидячих мест: ");
            Seats = int.Parse(Console.ReadLine());
        }

        public void ShowVirtual()
        {
            Console.WriteLine($"Пассажирский вагон №{Number}, ID: {Id}, Минимальная скорость: {MinSpeed}, Количество спальных мест: {SleepingPlaces}");
        }

        public override void RandomInit(Random rnd)
        {
            base.RandomInit(rnd);
            SleepingPlaces = rnd.Next(10, 50);
            Seats = rnd.Next(20, 100);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            PassengerWagon other = (PassengerWagon)obj;
            return SleepingPlaces == other.SleepingPlaces && Seats == other.Seats;
        }

        public override string ToString() =>
            $"Пассажирский вагон №{Number}, Максимальная скорость: {MinSpeed} км/ч, Спальных мест: {SleepingPlaces}, Сидячих мест: {Seats}";
    }
}