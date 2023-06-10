using ApiDogsCrud.DataLayer.Entity;

namespace ApiDogsCrud.DataLayer.Specification
{
    public class GetDogByNameSpecification : BaseSpecification<Dog>
    {
        public GetDogByNameSpecification(string name)
        {
            AddCriteria(d => d.Name == name);
        }
    }
}