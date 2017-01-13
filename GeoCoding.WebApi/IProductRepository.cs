using System.Collections.Generic;

namespace GeoCoding.WebApi
{
	public interface IProductRepository
	{
		IEnumerable<Product> GetAll ();

		Product Get (int id);

		Product Add (Product item);

		bool Update (Product item);

		bool Delete (int id);
	}
}