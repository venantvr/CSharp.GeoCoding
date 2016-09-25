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

            if (Repository.Update(product))
            {
                return Repository.GetAll();
            }
            return null;
        }

        public bool DeleteProduct(int id)
        {
            if (Repository.Delete(id))
            {
                return true;
            }
            return false;
        }
    }
}