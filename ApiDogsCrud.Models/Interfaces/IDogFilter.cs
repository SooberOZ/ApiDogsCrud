namespace ApiDogsCrud.Models.Interfaces
{
    public interface IDogFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}