using CSharpFunctionalExtensions;

namespace TulaHack.Core.Models
{
    public class Restaurant
    {
        public const int MAX_STRING_LENGTH = 50;

        public Guid Id { get; }
        public string Title { get; } = string.Empty;
        public string Subtitle { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public Guid UserId { get; }
        public User? User { get; } = null;
        public string Address { get; } = string.Empty;
        public List<Guid> Kitchen { get; } = [];
        public List<Guid> MenuIds { get; } = [];
        //public List<Menu> Menu { get; }
        public List<string> Photos { get; } = [];
        public float Raiting { get; } = 0f;
        public string StartWorkTime { get; }
        public string EndWorkTime { get; }
        public Guid SchemeId { get; }
        //public Scheme Scheme { get; }


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

            //if (string.IsNullOrWhiteSpace(kitchen) || kitchen.Length > MAX_STRING_LENGTH)
            //{
            //    return Result.Failure<Restaurant>($"'{nameof(kitchen)}' connot be empty or > {MAX_STRING_LENGTH}");
            //}

            if (raiting < 0f || raiting > 5f)
            {
                return Result.Failure<Restaurant>($"'{nameof(raiting)}' should be between 0 and 5");
            }

            var restaurant = new Restaurant(id, title, subtitle, description, userId, user, address, kitchen, menuIds, photos, raiting, startWorkTime, endWorkTime, schemeId);

            return Result.Success(restaurant);
        }
    }
}
