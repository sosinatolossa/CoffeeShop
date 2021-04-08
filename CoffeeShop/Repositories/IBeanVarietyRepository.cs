﻿using CoffeeShop.Models;

namespace CoffeeShop.Repositories
{
    public interface IBeanVarietyRepository
    {
        void Add(BeanVariety variety);
        void Delete(int id);
        BeanVariety Get(int id);
        System.Collections.Generic.List<BeanVariety> GetAll();
        void Update(BeanVariety variety);
    }
}