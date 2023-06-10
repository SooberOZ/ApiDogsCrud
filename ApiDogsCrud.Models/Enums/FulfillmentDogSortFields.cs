using System.Runtime.Serialization;

namespace ApiDogsCrud.Models.Enums
{
    public enum FulfillmentDogSortFields
    {
        [EnumMember(Value = "Name")]
        Name,
        [EnumMember(Value = "Color")]
        Color,
        [EnumMember(Value = "TailLength")]
        TailLength,
        [EnumMember(Value = "Weight")]
        Weight
    }
}