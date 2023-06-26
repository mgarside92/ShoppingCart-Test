﻿using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCart.Controllers;
using ShoppingCart.Domain.Services.Interfaces;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Tests.Controllers
{
	public class CartControllerTests
	{
		private Mock<ICartService> _mockCartService;
		private CartController _cartController;

		[SetUp]
		public void Setup()
		{
			_mockCartService = new Mock<ICartService>();
			_cartController = new CartController(_mockCartService.Object);
		}

		[Test]
		public void GetCart_Successful_ReturnsOk()
		{
			var cartViewModel = new CartViewModel();
			_mockCartService.Setup(m => m.GetCart()).Returns(Result.Ok(cartViewModel));

			var result = _cartController.GetCart() as OkObjectResult;

			Assert.IsNotNull(result);
			Assert.That(result.StatusCode, Is.EqualTo(200));
			Assert.That(result.Value, Is.EqualTo(cartViewModel));
		}

		[Test]
		public void GetCart_Failed_ReturnsBadRequest()
		{
			_mockCartService.Setup(m => m.GetCart()).Returns(Result.Fail<CartViewModel>("Some error"));

			var result = _cartController.GetCart() as BadRequestObjectResult;

			Assert.IsNotNull(result);
			Assert.That(result.StatusCode, Is.EqualTo(400));
		}

		[Test]
		public void AddToCart_Successful_ReturnsOk()
		{
			var cartViewModel = new CartViewModel();
			var productId = Guid.NewGuid();

			_mockCartService.Setup(m => m.AddToCart(productId)).Returns(Result.Ok(cartViewModel));

			var result = _cartController.AddToCart(productId) as OkObjectResult;

			Assert.IsNotNull(result);
			Assert.That(result.StatusCode, Is.EqualTo(200));
			Assert.That(result.Value, Is.EqualTo(cartViewModel));
		}

		[Test]
		public void AddToCart_Failed_ReturnsBadRequest()
		{
			var productId = Guid.NewGuid();
			_mockCartService.Setup(m => m.AddToCart(productId)).Returns(Result.Fail<CartViewModel>("Some error"));

			var result = _cartController.AddToCart(productId) as BadRequestObjectResult;

			Assert.IsNotNull(result);
			Assert.That(result.StatusCode, Is.EqualTo(400));
		}

		[Test]
		public void RemoveFromCart_Successful_ReturnsOk()
		{
			var cartViewModel = new CartViewModel();
			var productId = Guid.NewGuid();

			_mockCartService.Setup(m => m.RemoveFromCart(productId)).Returns(Result.Ok(cartViewModel));

			var result = _cartController.RemoveFromCart(productId) as OkObjectResult;

			Assert.IsNotNull(result);
			Assert.That(result.StatusCode, Is.EqualTo(200));
			Assert.That(result.Value, Is.EqualTo(cartViewModel));
		}

		[Test]
		public void RemoveFromCart_Failed_ReturnsBadRequest()
		{
			var productId = Guid.NewGuid();
			_mockCartService.Setup(m => m.RemoveFromCart(productId)).Returns(Result.Fail<CartViewModel>("Some error"));

			var result = _cartController.RemoveFromCart(productId) as BadRequestObjectResult;

			Assert.IsNotNull(result);
			Assert.That(result.StatusCode, Is.EqualTo(400));
		}
	}
}
