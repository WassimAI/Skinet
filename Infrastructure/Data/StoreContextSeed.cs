using System.Text.Json;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            /*
            below we get the context object and check if we have data in the tables, if not we seed then :)
            We get the json data into an object (brandsData) then we deserialize (from Json to C# Object) then we loop each item in the object (all brands) 
            and we add it to the table of the database! Of course we save changes! :) Amazing!
            We call for this method (SeedAsync) from the program.cs class :)
            */
            try
            {
                if(!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach(var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }
                    await context.Database.OpenConnectionAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductBrands ON");
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductBrands OFF");
                    await context.Database.CloseConnectionAsync();
                }

                if(!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach(var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.Database.OpenConnectionAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductTypes ON");
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductTypes OFF");
                    await context.Database.CloseConnectionAsync();
                }

                if(!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach(var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}