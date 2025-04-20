namespace TrainWagons
{//этот узел является главным кирпичем по сути он хранит данные и содержит ссылки на следующий и предыдущий
    public class Node<T> where T : ICloneable//здесь двунаправленный список состоит из узлов где каждый из них содержит данные и ссылки на следующий и на предыдущий элемент
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}