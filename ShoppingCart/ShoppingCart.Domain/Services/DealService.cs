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

                // calculate balance
				balance += deal != null ? BalanceWithDeal(item, deal) : BalanceWithoutDeal(item);
			}

			return balance;
		}

		private double BalanceWithDeal(CartItem item, Deal deal)
		{
			double balance = 0;

            // if quantity is less than deal requirements then return balance without deal applied
			if (item.Quantity < deal.DealQuantity)
			{
				return BalanceWithoutDeal(item);
			}

			var dealSets = item.Quantity / deal.DealQuantity;
			var remainingItems = item.Quantity % deal.DealQuantity;

            // calculate balance at deal price
			balance += dealSets * deal.DealPrice;

            // calculate balance of remaining items
			balance += remainingItems * item.Product.Price;

			return balance;
		}

		private double BalanceWithoutDeal(CartItem item)
		{
			return item.Quantity * item.Product.Price;
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
