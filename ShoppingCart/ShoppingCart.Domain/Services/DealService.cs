using ShoppingCart.Domain.Services.Interfaces;
using ShoppingCart.Models.Entities;
using ShoppingCart.Models.ViewModels;
using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Domain.Services
{
    public class DealService : BaseService, IDealService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDealRepository _dealRepository;

        public DealService(IUnitOfWork unitOfWork, IProductRepository productRepository, IDealRepository dealRepository) : base(unitOfWork)
        {
            _productRepository = productRepository;
            _dealRepository = dealRepository;
        }

        public double CalculateBalanceAndApplyDeals(List<CartItem> cartItems)
        {
            double balance = 0;

            foreach (var item in cartItems)
            {
                var deal = _dealRepository.GetDealByProductId(item.ProductId);
                if (deal != null)
                {
                    if (item.Quantity >= deal.DealQuantity)
                    {
                        var dealSets = item.Quantity / deal.DealQuantity;
                        var remainingItems = item.Quantity % deal.DealQuantity;

                        balance += dealSets * deal.DealPrice;
                        balance += remainingItems * item.Product.Price;
                    }
                    else
                    {
						balance += item.Quantity * item.Product.Price;
					}
                }
                else
                {
                    balance += item.Quantity * item.Product.Price;
				}
            }

            return balance;
        }

        public void SeedDeals()
        {
            var productA = _productRepository.GetAll().FirstOrDefault(p => p.Name == "A");

            if (productA != null)
            {
                var dealAExists = _dealRepository.GetAll().Any(d => d.ProductId == productA.Id && d.DealQuantity == 3 && d.DealPrice == 13);
                if (!dealAExists)
                {
                    Deal dealA = new()
                    {
                        ProductId = productA.Id,
                        DealQuantity = 3,
                        DealPrice = 13
                    };
                    _dealRepository.Add(dealA);
                }
            }

            var productB = _productRepository.GetAll().FirstOrDefault(p => p.Name == "B");

            if (productB != null)
            {
                var dealBExists = _dealRepository.GetAll().Any(d => d.ProductId == productB.Id && d.DealQuantity == 2 && d.DealPrice == 4.5);
                if (!dealBExists)
                {
                    Deal dealB = new()
                    {
                        ProductId = productB.Id,
                        DealQuantity = 2,
                        DealPrice = 4.5
                    };
                    _dealRepository.Add(dealB);
                }
            }

            _unitOfWork.SaveChanges();
        }
    }
}
