using Grocery.Core.Helpers;
using NUnit.Framework;
using System.Linq;
using Grocery.Core.Models;
using System.Collections.Generic;

namespace TestCore
{
    [TestFixture]
    public class TestHelpers
    {
        // Password helper (parameterized)
        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=", true)]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=", true)]
        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg", false)]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ", false)]
        public void PasswordHelper_Verify(string password, string hash, bool expected)
            => Assert.AreEqual(expected, PasswordHelper.VerifyPassword(password, hash));

        private class CategorySearchContext
        {
            public List<Product> CategoryProducts { get; } = new();
            public List<Product> AllProducts { get; } = new();
            public List<Product> FilteredProducts { get; } = new();
            private string _searchText = string.Empty;
            public string SearchText
            {
                get => _searchText;
                set { _searchText = value; UpdateFiltered(); }
            }
            public void Load(IEnumerable<Product> all, IEnumerable<int> linkedIds)
            {
                AllProducts.Clear(); AllProducts.AddRange(all);
                CategoryProducts.Clear();
                foreach (var p in AllProducts.Where(p => linkedIds.Contains(p.Id))) CategoryProducts.Add(p);
                UpdateFiltered();
            }
            public void AddProduct(Product p)
            {
                if (CategoryProducts.Any(x => x.Id == p.Id)) return;
                CategoryProducts.Add(p);
                FilteredProducts.RemoveAll(x => x.Id == p.Id);
            }
            private void UpdateFiltered()
            {
                FilteredProducts.Clear();
                var linked = CategoryProducts.Select(p => p.Id).ToHashSet();
                IEnumerable<Product> src = AllProducts.Where(p => !linked.Contains(p.Id));
                if (!string.IsNullOrWhiteSpace(_searchText))
                {
                    var term = _searchText.Trim();
                    src = src.Where(p => p.Name.Contains(term, System.StringComparison.OrdinalIgnoreCase));
                }
                FilteredProducts.AddRange(src.OrderBy(p => p.Name));
            }
            public void ClearSearch() => SearchText = string.Empty;
        }

        private static CategorySearchContext BuildContext(params int[] linked)
        {
            var ctx = new CategorySearchContext();
            var products = new List<Product>
            {
                new Product(1, "Appel",10,1),
                new Product(2, "Banaan",5,1),
                new Product(3, "Ananas",3,2),
                new Product(4, "Kiwi",7,2)
            };
            ctx.Load(products, linked);
            return ctx;
        }

        // Happy path: filter excludes already linked and is case-insensitive
        [Test]
        public void CategorySearch_FilterAndExcludeLinked_Happy()
        {
            var ctx = BuildContext(2); // Banaan linked
            ctx.SearchText = "an"; // matches Banaan & Ananas, but Banaan linked
            var names = ctx.FilteredProducts.Select(p => p.Name).ToList();
            CollectionAssert.AreEqual(new[] { "Ananas" }, names);
        }

        // Unhappy path: clear search restores all unlinked (linked stay excluded)
        [Test]
        public void CategorySearch_ClearRestores_Unhappy()
        {
            var ctx = BuildContext(1); // Appel linked
            ctx.SearchText = "ki"; // matches Kiwi only
            Assert.AreEqual(1, ctx.FilteredProducts.Count);
            ctx.ClearSearch();
            var ids = ctx.FilteredProducts.Select(p => p.Id).OrderBy(i => i).ToList();
            CollectionAssert.AreEqual(new[] { 2,3,4 }, ids); // 1 (Appel) excluded
        }
    }
}