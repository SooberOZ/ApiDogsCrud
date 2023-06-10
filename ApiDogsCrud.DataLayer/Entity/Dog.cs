namespace ApiDogsCrud.DataLayer.Entity
{
    public class Dog
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}