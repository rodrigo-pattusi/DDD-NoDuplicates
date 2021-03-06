﻿using System;
using Xunit;

namespace NoDuplicatesDesigns._01_Database
{
    public class ProductUpdateNameTests
    {
        private ProductRepository _productRepository = new ProductRepository();
        private const string TEST_NAME = "Test Name";
        private const int TEST_ID1 = 1;
        private const int TEST_ID2 = 2;

        public ProductUpdateNameTests()
        {
            SeedData();
        }

        private void SeedData()
        {
            _productRepository.Add(new Product { Id = TEST_ID1, Name = TEST_NAME });
            _productRepository.Add(new Product { Id = TEST_ID2, Name = Guid.NewGuid().ToString() });
        }

        [Fact]
        public void UpdatesNameGivenNewUniqueName()
        {
            var product = _productRepository.GetById(TEST_ID2);
            string newName = Guid.NewGuid().ToString();

            product.Name = newName;

            Assert.Equal(newName, product.Name);
        }

        [Fact]
        public void UpdatesNameGivenCurrentName()
        {
            var product = _productRepository.GetById(TEST_ID2);
            string newName = product.Name;

            product.Name = newName;

            Assert.Equal(newName, product.Name);
        }

        [Fact]
        public void InsertsNewProductGivenUniqueName()
        {
            var product = new Product() { Id = 4 };
            string newName = Guid.NewGuid().ToString();
            product.Name = newName;

            _productRepository.Add(product);

            Assert.Equal(newName, product.Name);
        }

        [Fact]
        public void ThrowsExceptionGivenDuplicateNameAfterUpdate()
        {
            var product = _productRepository.GetById(TEST_ID2);
            product.Name = TEST_NAME;
            var result = Assert.Throws<Exception>(() => _productRepository.Update(product));

            Assert.Equal("Duplicate name.", result.Message);
        }

        [Fact]
        public void ThrowsExceptionGivenDuplicateNameOnInsert()
        {
            var newproduct = new Product() { Id = 3 };
            newproduct.Name = TEST_NAME;
            var result = Assert.Throws<Exception>(() => _productRepository.Add(newproduct));

            Assert.Equal("Duplicate name.", result.Message);
        }
    }
}
