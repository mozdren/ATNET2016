﻿using System;
using System.Collections.Generic;
using SharedLibs.DataContracts;

namespace ServiceBus.Mock
{
    /// <summary>
    /// This is just a mock of a Product service to provide the developers samples of outputs and interfaces they
    /// might encounter when using the service. This is definitely not intended to simulate whole functionality!
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Adds product
        /// </summary>
        /// <param name="product">product to be added</param>
        /// <returns>success if added sucessfuly</returns>
        public Result AddProduct(SharedLibs.DataContracts.Product product)
        {
            return Result.SuccessFormat("Product added");
        }

        /// <summary>
        /// Adds product
        /// </summary>
        /// <param name="product">product to be added</param>
        /// <returns>success if added successfully</returns>
        public Result AddProduct(string name, double price, Guid guid)
        {
            return Result.SuccessFormat("Product added");
        }

        /// <summary>
        /// Deletes a product from a database (just a mock - does nothing)
        /// </summary>
        /// <param name="guid">identifier of a product</param>
        /// <returns></returns>
        public Result DeleteProduct(Guid guid)
        {
            return Result.SuccessFormat("Product deleted");
        }

        /// <summary>
        /// Edits a product
        /// </summary>
        /// <param name="guid">product identifier</param>
        /// <param name="name">product name</param>
        /// <param name="price">produt price</param>
        /// <returns>edited product</returns>
        public Product EditProduct(Guid guid, string name, double price)
        {
            return new Product
            {
                Result = Result.SuccessFormat("Product Edited"),
                ID = guid,
                Name = name,
                Price = price
            };
        }

        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns></returns>
        public Products GetAllProducts()
        {
            return new Products
            {
                Result = Result.Success("Products found"),
                Items = new List<Product> {
                    new Product { ID = Guid.Empty, Name = "Mock 1", Price = 10.0 },
                    new Product { ID = Guid.Empty, Name = "Mock 2", Price = 20.0 },
                    new Product { ID = Guid.Empty, Name = "Mock 3", Price = 30.0 },
                    new Product { ID = Guid.Empty, Name = "Mock 4", Price = 40.0 },
                    new Product { ID = Guid.Empty, Name = "Mock 5", Price = 50.0 },
                    new Product { ID = Guid.Empty, Name = "Mock 6", Price = 60.0 }
                }
            };
        }

        /// <summary>
        /// returns a specific product
        /// </summary>
        /// <param name="guid">guid of the product</param>
        /// <returns>product with specific Guid if found</returns>
        public Product GetProduct(Guid guid)
        {
            return new Product { Result = Result.Success("Product found"), ID = guid, Name = "Mock", Price = 10.0 };
        }
    }
}
