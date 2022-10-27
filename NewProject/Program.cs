using NewProject;

var brands = new List<Brand>() {
    new Brand{ID = 1, Name = "Công ty AAA"},
    new Brand{ID = 2, Name = "Công ty BBB"},
    new Brand{ID = 4, Name = "Công ty CCC"},
};

var products = new List<Product>()
{
    new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
    new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
    new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
    new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
    new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
    new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
    new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
};

//Select product having price equal 400
var sanPhamGiaBang400 = from product in products
                        where product.Price == 400
                        select product;

//Select product having price greater or equal 500 and name start with("Giường")
var ketQua1 = from product in products
              where product.Price >= 500 && product.Name.StartsWith("Giường")
              select product;

var yellowProduct = from product in products
                    from color in product.Colors
                    where color == "Vàng"
                    select product;

var orderedByPriceAndNameProduct = from product in products
                                   orderby product.Price, product.Name descending
                                   select product;

var groupedByPriceProduct = from product in products
                            group product by product.Price into gr
                            let count = gr.Count()
                            where count > 1
                            select new
                            {
                                price = gr.Key,
                                count = count
                            };

var groupedByPriceAndBrandIdProduct = from product in products
                                      group product by new { product.Price, product.Brand } into gr
                                      select gr
                                      /*select new
                                      {
                                        brand = product.Brand,
                                        price = product.Price,
                                        products = gr
                                      }*/;
foreach (var item in groupedByPriceAndBrandIdProduct)
{
    /* Console.WriteLine($"{item.brand} === {item.price}");
     foreach (var product in item.products) 
     {
         Console.WriteLine(product.GetType());
         Console.WriteLine($"{product.ID} === {product.Name} === {product.Price} === {product.Brand}");
     }*/
    var list = item.GetEnumerator();
    while (list.MoveNext())
    {
        var item2 = list.Current;
        Console.Write(item2);
    }
}


//Leftouter join product x brand
var ketQua = from product in products
             join brand in brands on product.Brand equals brand.ID into t
             from brand in t.DefaultIfEmpty()
             select new {
                id = product.ID,
                price = product.Price,
                brand = (brand == null) ? null : brand.Name,
             };


