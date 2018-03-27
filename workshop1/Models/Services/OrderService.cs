﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshop1.Models.Services
{
    public class OrderService
    {
        /// <summary>
        /// 目前所有訂單
        /// </summary>
        private static IList<Order> Orders = new List<Order>()
        {
            new Order()
            {
                OrderID = 1,
                CustomerID = 1,
                EmployeeID = 1,
                OrderDate = new DateTime(2018, 3, 27),
                RequiredDate = new DateTime(2018, 3, 30),
                ShipedDate = new DateTime(2018, 3,29),
                ShipperID = 1,
                Freight = 300,
                ShipAddress = "高雄市燕巢校區高雄應用科技大學",
                ShipCity = "高雄市",
                ShipRegin = "燕巢區",
                ShipPostalCode = "705",
                ShipCountry = "台灣"
            },
            new Order()
            {
                OrderID = 2,
                CustomerID = 2,
                EmployeeID = 2,
                OrderDate = new DateTime(2018, 3, 30),
                RequiredDate = new DateTime(2018, 4, 20),
                ShipedDate = new DateTime(2018, 4,10),
                ShipperID = 2,
                Freight = 1111500,
                ShipAddress = "高雄市建功校區高雄應用科技大學",
                ShipCity = "高雄市",
                ShipRegin = "三民區",
                ShipPostalCode = "710",
                ShipCountry = "台灣"
            }
        };

        /// <summary>
        /// 取得 Orders by 條件
        /// </summary>
        /// <returns></returns>
        public IList<Order> GetOrders(OrderQueryArg arg)
        {
            CustomerService customerService = new CustomerService();

            IEnumerable<Order> currentOrders = Orders;

            // 訂單編號
            if (arg.OrderID.HasValue)
            {
                currentOrders = currentOrders.Where(m => m.OrderID == arg.OrderID.Value);
            }

            // 客戶名稱 (like 查詢)
            if (!string.IsNullOrWhiteSpace(arg.CompanyName))
            {
                currentOrders = 
                    currentOrders.Where(
                        m =>customerService.GetCompanyName(m.CustomerID).Contains(arg.CompanyName)
                    );
            }

            // 員工編號
            if (arg.EmployeeID.HasValue)
            {
                currentOrders = currentOrders.Where(m => m.EmployeeID == arg.EmployeeID.Value);
            }

            // 出貨公司
            if (arg.ShipperID.HasValue)
            {
                currentOrders = currentOrders.Where(m => m.ShipperID == arg.ShipperID.Value);
            }

            // 訂購日期
            if (arg.OrderDate.HasValue)
            {
                currentOrders = currentOrders.Where(m => m.OrderDate == arg.OrderDate.Value);
            }

            // 需要日期
            if (arg.RequiredDate.HasValue)
            {
                currentOrders = currentOrders.Where(m => m.RequiredDate == arg.RequiredDate.Value);
            }

            // 出貨日期
            //if (arg.ShipedDate.HasValue)
            //{
            //    currentOrders = currentOrders.Where(m => m.ShipedDate == arg.ShipedDate.Value);
            //}

            return currentOrders.ToList();
        }
    }
}