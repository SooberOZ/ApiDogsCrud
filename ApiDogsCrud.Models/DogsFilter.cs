using ApiDogsCrud.Models.Enums;
using ApiDogsCrud.Models.Interfaces;

namespace ApiDogsCrud.Models
{
    public class DogsFilter : IDogFilter, ISortableFilter<FulfillmentDogSortFields>

    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public SortDirection Direction { get; set; }
        public FulfillmentDogSortFields OrderBy { get; set; }
    }
}