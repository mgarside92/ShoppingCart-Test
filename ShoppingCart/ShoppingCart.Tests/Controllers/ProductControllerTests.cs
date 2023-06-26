﻿using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCart.Controllers;
using ShoppingCart.Domain.Services.Interfaces;
using ShoppingCart.Models.Entities;

namespace ShoppingCart.Tests.Controllers
{
	public class ProductControllerTests
	{
		private Mock<IProductService> _mockProductService;
		private ProductController _productController;

		[SetUp]
		public void Setup()
		{
			_mockProductService = new Mock<IProductService>();
			_productController = new ProductController(_mockProductService.Object);
		}

		[Test]
		public void GetAllProducts_Successful_ReturnsOk()
		{
			var products = new List<Product>();
			_mockProductService.Setup(m => m.GetProducts()).Returns(Result.Ok(products));

			var result = _productController.GetAllProducts() as OkObjectResult;

			Assert.IsNotNull(result);
			Assert.That(result.StatusCode, Is.EqualTo(200));
			Assert.That(result.Value, Is.EqualTo(products));
		}

		[Test]
		public void GetAllProducts_Failed_ReturnsBadRequest()
		{
			_mockProductService.Setup(m => m.GetProducts()).Returns(Result.Fail<List<Product>>("Some error"));

			var result = _productController.GetAllProducts() as BadRequestObjectResult;

			Assert.IsNotNull(result);
			Assert.That(result.StatusCode, Is.EqualTo(400));
		}
	}
}
