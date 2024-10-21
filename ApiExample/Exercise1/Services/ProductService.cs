namespace Exercise1.Services
{
    public class ProductService
    {
        public Entity.Product ConvertToEntity(Models.ProductModel model)
        {
            return new Entity.Product
            {
                Name = model.Name,
                Price = model.Price
            };
        }

        public Models.ProductModel ConvertToModel(Entity.Product entity)
        {
            return new Models.ProductModel
            {
                Name = entity.Name,
                Price = entity.Price
            };
        }
    }
}
