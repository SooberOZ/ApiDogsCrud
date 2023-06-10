using ApiDogsCrud.Models.Enums;

namespace ApiDogsCrud.Models.Interfaces
{
    public interface ISortableFilter<T>
        where T : struct, Enum
    {
        public T OrderBy { get; set; }
        public SortDirection Direction { get; set; }
    }
}