using System;

namespace TrainWagons
{
    public class IdNumber
    {
        private int number;
        public int Number
        {
            get => number;
            set => number = value >= 0 ? value : throw new ArgumentException("Номер не может быть отрицательным");
        }

        public IdNumber(int number) => Number = number;
        public override string ToString() => $"ID: {Number}";
        public override bool Equals(object obj) => obj is IdNumber id && Number == id.Number;
    }
}