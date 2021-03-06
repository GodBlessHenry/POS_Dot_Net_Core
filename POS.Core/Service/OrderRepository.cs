﻿using System.Collections.Generic;

namespace POS.Core.Service
{
    public class OrderRepository : IOrderRepository
    {
        public Order GetById(int id)
        {
            // Order should be saved in database,
            // below is just to similate fetching it from database
            return new Order
            {
                Id = id,
                WithCoupon = true, 
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Product = new Product
                        {
                            Name = "Cheerio",
                            Price = 6.99d,
                            CanUseBulkDiscount = true
                        },
                        Unit = ProductUnit.Box,
                        Quantity = 40,
                    },
                    new OrderItem
                    {
                        Product = new Product
                        {
                            Name = "Apple",
                            Price = 2.49d,
                            CanUseBulkDiscount = true
                        },
                        Unit = ProductUnit.Pound,
                        Quantity = 10
                    }
                }
            };
        }
    }
}