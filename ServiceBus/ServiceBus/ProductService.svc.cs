﻿using System;
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
        public SharedLibs.DataContracts.Product GetProduct(Guid guid)
        {
            try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    var service = new ProductTypeServiceProxyClass.ProductTypeServiceClient();
                    var product = context.Products.FirstOrDefault(p => p.Id == guid);

                    if (product == null)
                    {
                        return new SharedLibs.DataContracts.Product
                        {
                            Result = SharedLibs.DataContracts.Result.ErrorFormat("Product {0} not found.", guid)
                        };
                    }

                    #region FillBasketItems
                    var queryBasketItems = from contextItem in product.BasketItems
                        select new SharedLibs.DataContracts.BasketItem()
                        {
                            Id = contextItem.Id,
                            Quantity = contextItem.Quantity,
                            Product = new SharedLibs.DataContracts.Product() { ID = contextItem.ProductId }, 
                                    // TODO: or = null ? Since it must be this method param.
                            Result = Result.Success()
                        };

                    var listOfBasketItems = queryBasketItems.ToList();
                    #endregion            
                    
                    #region FillRepairs

                    var queryRepairs = from contextItem in product.Repairs
                        select new SharedLibs.DataContracts.Repair()
                        {
                            Result = Result.Success() //TODO: Finish the respective DTO.
                        };

                    var listOfRepairs = queryRepairs.ToList();
                    #endregion

                    #region FillReservations    

                    var queryReservations = from contextItem in product.Reservations
                        select new SharedLibs.DataContracts.Reservation()
                        {
                            Result = Result.Success() //TODO: Finish the respective DTO.
                        };

                    var listOfReservations = queryReservations.ToList();

                    #endregion

                    #region FillStorageItems

                    var queryStorageItems = from contextItem in product.StorageItems
                        select new SharedLibs.DataContracts.StorageItem()
                        {
                            Result = Result.Success() //TODO: Finish the respective DTO.
                        };

                    var listOfStorageItems = queryStorageItems.ToList();
                    #endregion

                    return new SharedLibs.DataContracts.Product
                    {
                        Result = SharedLibs.DataContracts.Result.SuccessFormat("Product {0} found", guid),
                        ID = guid,
                        Name = product.Name,
                        Price = product.Price.HasValue ? product.Price.Value : 0.0,
                        Enabled = product.Enabled,
                        Headliner = product.Headliner,
                        ProductType = service.GetProductType((ProductTypes) product.ProductType.Id),
                        BasketItems = listOfBasketItems,
                        Repairs = listOfRepairs,
                        Reservations = listOfReservations,
                        StorageItems = listOfStorageItems
                    };
                }
            }
            catch (Exception exception)
            {
                return new SharedLibs.DataContracts.Product
                {
                    Result = SharedLibs.DataContracts.Result.FatalFormat("ProductService.GetProduct Exception: {0}", exception.Message)
                };
            }
        }

        /// <summary>
        /// This method returns all defined products
        /// </summary>
        /// <returns>collection of all products</returns>
        public SharedLibs.DataContracts.Products GetAllProducts()
        {
            try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    var returnCollection = new List<SharedLibs.DataContracts.Product>();

                    foreach (var product in context.Products)
                    {
                        returnCollection.Add(GetProduct(product.Id));
                    }

                    return new SharedLibs.DataContracts.Products
                    {
                        Result = SharedLibs.DataContracts.Result.Success("All products returned"),
                        Items = returnCollection
                    };
                }
            }
            catch (Exception exception)
            {
                return new SharedLibs.DataContracts.Products
                {
                    Result = SharedLibs.DataContracts.Result.FatalFormat("ProductService.GetProduct Exception: {0}", exception.Message)
                };
            }
        }

        /// <summary>
        /// This method adds product into the datasource. Defaultly product is not main product.
        /// </summary>
        /// <param name="product">Product object</param>
        /// <param name="productType">ID of a product type</param>
        /// <param name="headliner">Is this product supposed to be main product?</param>
        /// <returns>Result object</returns>
        public Result AddProduct(SharedLibs.DataContracts.Product product, ProductTypes productType, bool headliner = false)
        {
            return AddProduct(product.Name, product.Price, product.ID, productType, headliner);
        }

        /// <summary>
        /// This method adds product into datasource. Defaultly product is not main product. 
        /// </summary>
        /// <param name="name">Name of a new product</param>
        /// <param name="price">Price of a new product</param>
        /// <param name="guid">ID of a new prodcut</param>
        /// <param name="productType">ID of a product type</param>
        /// <param name="headliner">Is this product supposed to be main product?</param>
        /// <returns>Result object</returns>
        public Result AddProduct(string name, double price, Guid guid, ProductTypes productType, bool headliner = false )
        {
            try
            {
                if ((!string.IsNullOrWhiteSpace(name) && name.Length <= 50) && price >= 0)
                {
                    using (var context = new ServiceBusDatabaseEntities())
                    {
                        //TODO: It does not work.
                        /* This always trys to store new product type, so when I forced type name to be unique it fails
                         * Why it stores new type when already existing type is sucessfully returned from db context?
                        context.Products.Add(new Product() { Id = guid, Name = name, Price = price, Headliner = headliner, ProductType = ProductType.GetProductType((int)productType) }); 
                        context.SaveChanges();
                        */

                        var product = new Product();

                        product.Id = guid;
                        product.Enabled = true;
                        product.Headliner = headliner;
                        product.Name = name;
                        product.Price = price;
                        product.ProductType = ProductType.GetProductType((int)productType);

                        context.Products.Add(product);
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
                return Result.FatalFormat("ProductService.AddProduct Exception: {0}", ex.InnerException.Message);
            }
            
        }

        /// <summary>
        /// This method edits product in case product exists
        /// </summary>
        /// <param name="guid">ID of a product</param>
        /// <param name="name">New name for a product</param>
        /// <param name="price">New price for a product</param>
        /// <param name="productType">Type of a product</param>
        /// <returns>Modified product</returns>
        //TODO: Rewrite! 
        
        public SharedLibs.DataContracts.Product EditProduct(Guid guid, string name, double price, ProductType productType = null, bool enabled = true, bool headliner = false)
        {
            try
            {
                    using (var context = new ServiceBusDatabaseEntities())
                    {
                        var product = context.Products.FirstOrDefault(p => p.Id == guid);

                        if (product != null)
                            {
                                context.Products.Attach(product);

                                //is new name valid and different than the stored one?
                                if (!String.IsNullOrWhiteSpace(name) && name.Length <= 50 && name != product.Name)
                                {
                                    product.Name = name;
                                }

                                //is price valid and different from the stored one?
                                if (price != product.Price && price >= 0)
                                {
                                    product.Price = price;
                                }
                            
                                if (productType != null && productType.Id != product.ProductType.Id)
                                {
                                    
                                    product.ProductType = context.ProductTypes.FirstOrDefault(p => p.Id == productType.Id);
                                   //TODO Why this doesn't make it EntityState.Changed ?
                                }

                                /*
                                //Well what else must be done when this is changed? This won't be trivial I guess.
                                if (!headliner)
                                {
                                    product.Headliner = true;
                                }

                                if (!enabled)
                                {
                                    product.Enabled = false;
                                }
                                */
                                //No changes? No need to update
                                if (context.Entry(product).State == EntityState.Unchanged && context.Entry(product.ProductType).State == EntityState.Unchanged)
                                {
                                    return new SharedLibs.DataContracts.Product()
                                        {
                                            Result = Result.WarningFormat("No attributes has been changed. {0} was not modified", product.Id)
                                        };
                                }
                                //Otherwise update db and return what you should return
                                else
                                {
                                    context.SaveChanges();
                                    return new SharedLibs.DataContracts.Product()
                                    {
                                        ID = product.Id,
                                        Name = product.Name,
                                        Price = product.Price.HasValue ? product.Price.Value : 0.0,
                                        Result = Result.Success()
                                    };
                                } 
                        }
                        return new SharedLibs.DataContracts.Product() {Result = Result.ErrorFormat("Product {0} was not found", guid)};
                    }
            }
            catch (Exception exception)
            {
                return new SharedLibs.DataContracts.Product()
                {
                    Result =
                        Result.FatalFormat("ProductService.EditProduct exception has occured : {0}", exception.Message)
                };
            }
        }

        /// <summary>
        /// Delete product item from datasource
        /// </summary>
        /// <param name="guid">ID of a product</param>
        /// <returns>Result object</returns>
        public Result DeleteProduct(Guid guid)
        {
            try
            {   
                using (var context = new ServiceBusDatabaseEntities())
                {
                    var product = context.Products.FirstOrDefault(p => p.Id == guid);

                    if (product != null && product.Enabled == true)
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
