﻿using ShoppingCart.Repositories.Repositories.Interfaces;

namespace ShoppingCart.Domain.Services
{
    public abstract class BaseService
    {
        public readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
