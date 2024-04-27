using CSharpFunctionalExtensions;
using System.Data;
using System.Numerics;

namespace TulaHack.Core.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public Guid TableId { get; set; }
        public string Date { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public int PersonsNumber { get; set; }
        public int Status { get; set; }


        public const int MAX_STRING_LENGTH = 50;


        private Booking(Guid id, Guid userId, User user, Guid restaurantId, Restaurant restaurant, Guid tableId, string date, string startTime, string endTime, int personsNumber, int status)
        {
            Id = id;
            UserId = userId;
            User = user;
            RestaurantId = restaurantId;
            Restaurant = restaurant;
            TableId = tableId;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            PersonsNumber = personsNumber;
            Status = status;
        }

        public static Result<Booking> Create(Guid id, Guid userId, User user, Guid restaurantId, Restaurant restaurant, Guid tableId, string date, string startTime, string endTime, int personsNumber, int status)
        {
            if (string.IsNullOrWhiteSpace(date) || !DateOnly.TryParse(date, out var parseDate))
            {
                return Result.Failure<Booking>($"'{nameof(date)}' empty or invalid");
            }

            if (string.IsNullOrWhiteSpace(startTime) || !TimeOnly.TryParse(startTime, out _))
            {
                return Result.Failure<Booking>($"'{nameof(startTime)}' empty or invalid");
            }

            if (string.IsNullOrWhiteSpace(endTime) || !TimeOnly.TryParse(endTime, out _))
            {
                return Result.Failure<Booking>($"'{nameof(endTime)}' empty or invalid");
            }

            if (personsNumber < 0)
            {
                return Result.Failure<Booking>($"'{nameof(personsNumber)}' connot be < 0");
            }

            var booking = new Booking(id, userId, user, restaurantId, restaurant, tableId, date, startTime, endTime, personsNumber, status);

            return Result.Success(booking);
        }
    }
}
