using System.Runtime.Serialization;

namespace ApiDogsCrud.Models.Enums
{
    public enum SortDirection
    {
        [EnumMember(Value = "Ascending")]
        Ascending,
        [EnumMember(Value = "Descending")]
        Descending
    }
}