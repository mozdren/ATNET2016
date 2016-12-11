using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SharedLibs.DataContracts;
using SharedLibs.Enums;

namespace ServiceBus
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// This method should return a product with requested guid
        /// </summary>
        /// <param name="guid">Guid of a requested product</param>
        /// <returns>requested product</returns>
        public Product GetProduct(Guid guid)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var product = context.Products.FirstOrDefault(p => p.Id == guid);

                    if (product == null)
                    {
                        return new Product
                        {
                            Result = Result.ErrorFormat("Product {0} not found.", guid)
                        };
                    }

                    return new Product
                    {
                        Result = Result.SuccessFormat("Product {0} found", guid),
                        ID = guid,
                        Name = product.Name,
                        Price = product.Price.HasValue ? product.Price.Value : 0.0
                    };
                }
            }
            catch (Exception exception)
            {
                return new Product
                {
                    Result = Result.FatalFormat("ProductService.GetProduct Exception: {0}", exception.Message)
                };
            }
        }

        /// <summary>
        /// This method returns all defined products
        /// </summary>
        /// <returns>collection of all products</returns>
        public Products GetAllProducts()
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var returnCollection = new List<Product>();

                    foreach (var product in context.Products)
                    {
                        returnCollection.Add(new Product
                        {
                            ID = product.Id,
                            Name = product.Name,
                            Price = product.Price.HasValue ? product.Price.Value : 0.0
                        });
                    }

                    return new Products
                    {
                        Result = Result.Success("All products returned"),
                        Items = returnCollection
                    };
                }
            }
            catch (Exception exception)
            {
                return new Products
                {
                    Result = Result.FatalFormat("ProductService.GetProduct Exception: {0}", exception.Message)
                };
            }
        }

        /// <summary>
<<<<<<< HEAD
=======
        /// This method adds product into the datasource
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns>Result object</returns>

        public Result AddProduct(SharedLibs.DataContracts.Product product, ProductTypes productType)
        {   //TODO: FIX DTO is obsolote
            return AddProduct(product.Name, product.Price, product.ID, productType);

        /// <summary>
>>>>>>> origin/VISpocka
        /// This method adds product into datasource
        /// </summary>
        /// <param name="name">Name of a new product</param>
        /// <param name="price">Price of a new product</param>
        /// <param name="guid">ID of a new product</param>
        /// <returns>Result object</returns>
        public Result AddProduct(string name, double price, Guid guid, ProductTypes productType, bool headliner = false)
        {   
            try
            {
                
                if ((!string.IsNullOrWhiteSpace(name) && name.Length <= 50) && price >= 0)
                {
                    using (var context = new EntityModels.ServiceBusDatabaseEntities())
                    {
                        var type = context.ProductTypes.FirstOrDefault(p => p.Type == productType.ToString());


                        context.Products.Add(new EntityModels.Product() { Id = guid, Name = name, Price = price, Enabled = true, Headliner = headliner, ProductType = type ?? null}); //Tohle bude hazet chyby

                        context.SaveChanges();

                        return Result.SuccessFormat("Product {0} | {1} has been added", guid, name);
                    }
                }
                else
                {
                    return Result.Error("Provided parameters are unacceptable.");
                }
            }
            catch (Exception ex)
            {
                return Result.FatalFormat("ProductService.AddProduct Exception: {0}", ex.Message);
            }
            
        }

        /// <summary>
        /// This method edits product in case product exists
        /// </summary>
        /// <param name="guid">ID of a product</param>
        /// <param name="name">New name for a product</param>
        /// <param name="price">New price for a product</param>
        /// <returns>Modified product</returns>
        public Product EditProduct(Guid guid, string name, double price)
        {
            try
            {
                var originalProduct = GetProduct(guid);

                if (originalProduct.Result.ResultType == ResultType.Success)
                {
                    var editableProduct = new EntityModels.Product()
                    {
                        Id = originalProduct.ID,
                        Name = originalProduct.Name,
                        Price = originalProduct.Price
                    };

                    using (var context = new EntityModels.ServiceBusDatabaseEntities())
                    {
                        context.Products.Attach(editableProduct);

                        //is new name valid and different than the stored one?
                        if (!String.IsNullOrWhiteSpace(name) && name.Length <= 50 && name != editableProduct.Name)
                        {
                            editableProduct.Name = name;
                        }

                        //is price valid and different from the stored one?
                        if (price != editableProduct.Price && price >= 0)
                        {
                            editableProduct.Price = price;
                        }

                        //No changes? No need to update
                        if (context.Entry(editableProduct).State == EntityState.Unchanged)
                        {
                            originalProduct.Result = Result.WarningFormat("Product {0} was not modified.", originalProduct.ID);
                            return originalProduct;
                        }
                        //Otherwise update db and return what you should return
                        else
                        {
                            context.SaveChanges();
                            return new Product()
                            {
                                ID = editableProduct.Id,
                                Name = editableProduct.Name,
                                Price = editableProduct.Price.HasValue ? editableProduct.Price.Value : 0.0,
                                Result = Result.Success()
                            };
                        }
                    }

                }
                else
                {
                    return originalProduct;
                }
            }
            catch (Exception exception)
            {
                return new Product()
                {
                    Result =
                        Result.FatalFormat("ProductService.EditProduct exception has occured : {0}", exception.Message)
                };
            }
        }

        /// <summary>
        /// Marks product as not enabled
        /// </summary>
        /// <param name="guid">ID of a product</param>
        /// <returns>Result object</returns>
        public Result DeleteProduct(Guid guid)
        {
            try
            {   
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var product = context.Products.FirstOrDefault(p => p.Id == guid);

                    if (product != null && product.Enabled)
                    {
                        product.Enabled = false;
                        context.SaveChanges();

                        return Result.SuccessFormat("Product {0} - {1} has been deleted.", product.Id, product.Name);
                    }

                    return Result.Error("Product was not found. Please make sure product ID is valid.");
                }
            }
            catch (Exception exception)
            {
                return Result.FatalFormat("ProductService.DeleteProduct exception has occured : {0}", exception.Message);
            }
        }

    }
}
