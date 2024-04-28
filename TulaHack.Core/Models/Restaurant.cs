using CSharpFunctionalExtensions;

namespace TulaHack.Core.Models
{
    public class Restaurant
    {
        public const int MAX_STRING_LENGTH = 50;

        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User? User { get; set; } = null;
        public string Address { get; set; } = string.Empty;
        public List<Guid> Kitchen { get; set; } = [];
        public List<Guid> MenuIds { get; set; } = [];
        public List<string> Photos { get; set; } = [];
        public float Raiting { get; set; } = 0f;
        public string StartWorkTime { get; set; }
        public string EndWorkTime { get; set; }
        public Guid SchemeId { get; set; }


        public Restaurant() { }

        private Restaurant(Guid id, string title, string subtitle, string description, Guid userId, User? user,
            string address, List<Guid> kitchen, List<Guid> menuIds, List<string> photos, float raiting,
            string startWorkTime, string endWorkTime, Guid schemeId)
        {
            Id = id;
            Title = title;
            Subtitle = subtitle;
            Description = description;
            UserId = userId;
            User = user;
            Address = address;
            Kitchen = kitchen;
            MenuIds = menuIds;
            Photos = photos;
            Raiting = raiting;
            StartWorkTime = startWorkTime;
            EndWorkTime = endWorkTime;
            SchemeId = schemeId;
        }

        public static Result<Restaurant> Create(Guid id, string title, string subtitle, string description, Guid userId, User? user,
            string address, List<Guid> kitchen, List<Guid> menuIds, List<string> photos, float raiting,
            string startWorkTime, string endWorkTime, Guid schemeId)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length > MAX_STRING_LENGTH)
            {
                return Result.Failure<Restaurant>($"'{nameof(title)}' connot be empty or > {MAX_STRING_LENGTH}");
            }

            if (string.IsNullOrWhiteSpace(subtitle) || subtitle.Length > MAX_STRING_LENGTH)
            {
                return Result.Failure<Restaurant>($"'{nameof(subtitle)}' connot be empty or > {MAX_STRING_LENGTH}");
            }

            if (string.IsNullOrWhiteSpace(address) || address.Length > MAX_STRING_LENGTH)
            {
                return Result.Failure<Restaurant>($"'{nameof(address)}' connot be empty or > {MAX_STRING_LENGTH}");
            }

            var restaurant = new Restaurant(id, title, subtitle, description, userId, user, address, kitchen, 
                menuIds, photos, raiting, startWorkTime, endWorkTime, schemeId);

            return Result.Success(restaurant);
        }
    }
}
