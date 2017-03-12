using System.Collections;

namespace GeoCoding.WebApi
{
    public class ProductController
    {
        private static readonly IProductRepository Repository = new ProductRepository();

        public IEnumerable GetAllProducts()
        {
            return Repository.GetAll();
        }

        public Product PostProduct(Product item)
        {
            return Repository.Add(item);
        }

        public IEnumerable PutProduct(int id, Product product)
        {
            product.Id = id;

            return Repository.Update(product) ? Repository.GetAll() : null;
        }

        public bool DeleteProduct(int id)
        {
            return Repository.Delete(id);
        }
    }
}