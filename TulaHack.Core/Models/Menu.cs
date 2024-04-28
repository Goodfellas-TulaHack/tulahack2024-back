using CSharpFunctionalExtensions;
using System.Net.NetworkInformation;

namespace TulaHack.Core.Models
{
    public class Menu
    {
        public const int MAX_STRING_LENGTH = 50;

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;

        private Menu(Guid id, string name, Guid restaurantId, Restaurant restaurant, decimal price,
            string description, string photo)
        {
            Id = id;
            Name = name;
            RestaurantId = restaurantId;
            Restaurant = restaurant;
            Price = price;
            Description = description;
            Photo = photo;
        }

        public static Result<Menu> Create(Guid id, string name, Guid restaurantId, Restaurant restaurant, decimal price,
            string description, string photo)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_STRING_LENGTH)
            {
                return Result.Failure<Menu>($"'{nameof(name)}' length must be < {MAX_STRING_LENGTH}");
            }

            var menu = new Menu(
                id,
                name,
                restaurantId,
                restaurant,
                price,
                description,
                photo
                );

            return menu;    
        }
    }
}
