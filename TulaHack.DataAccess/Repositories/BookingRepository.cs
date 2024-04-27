using Microsoft.EntityFrameworkCore;
using TulaHack.Core.Models;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Repositories
{
    public class BookingRepository
    {
        private readonly TulaHackDbContext _dbContext;

        public BookingRepository(TulaHackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Booking?> GetById(Guid id)
        {
            var bookingEntity = await _dbContext.Bookings
                .AsNoTracking()
                .Include(b => b.User)
                .Include(b => b.Restaurant)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookingEntity == null) return null;

            var booking = Booking.Create(
                    bookingEntity.Id,
                    bookingEntity.UserId,
                    User.Create(
                        bookingEntity.User.Id,
                        bookingEntity.User.Login,
                        string.Empty,
                        bookingEntity.User.Role,
                        bookingEntity.User.FirstName,
                        bookingEntity.User.LastName,
                        bookingEntity.User.MiddleName,
                        bookingEntity.User.Phone
                        ).Value,
                    bookingEntity.RestaurantId,
                    Restaurant.Create(
                        bookingEntity.Restaurant.Id,
                        bookingEntity.Restaurant.Title,
                        bookingEntity.Restaurant.Subtitle,
                        bookingEntity.Restaurant.Description,
                        bookingEntity.Restaurant.UserId,
                        null,
                        bookingEntity.Restaurant.Address,
                        bookingEntity.Restaurant.Kitchen,
                        bookingEntity.Restaurant.MenuIds,
                        bookingEntity.Restaurant.Photos,
                        bookingEntity.Restaurant.Raiting,
                        bookingEntity.Restaurant.StartWorkTime,
                        bookingEntity.Restaurant.EndWorkTime,
                        bookingEntity.Restaurant.SchemeId
                        ).Value,
                    bookingEntity.TableId,
                    bookingEntity.Date,
                    bookingEntity.StartTime,
                    bookingEntity.EndTime,
                    bookingEntity.PersonsNumber,
                    bookingEntity.Status
                    );

            return booking.Value;
        }

        public async Task<List<Booking>> GetByRestaurantId(Guid id)
        {
            var bookingEntities = await _dbContext.Bookings
                .AsNoTracking()
                .Include(b => b.User)
                .Include(b => b.Restaurant)
                .Where(b => b.RestaurantId == id)
                .ToListAsync();

            var bookings = bookingEntities
                .Select(b => Booking.Create(
                    b.Id,
                    b.UserId,
                    User.Create(
                        b.User.Id,
                        b.User.Login,
                        string.Empty,
                        b.User.Role,
                        b.User.FirstName,
                        b.User.LastName,
                        b.User.MiddleName,
                        b.User.Phone
                        ).Value,
                    b.RestaurantId,
                    Restaurant.Create(
                        b.Restaurant.Id,
                        b.Restaurant.Title,
                        b.Restaurant.Subtitle,
                        b.Restaurant.Description,
                        b.Restaurant.UserId,
                        null,
                        b.Restaurant.Address,
                        b.Restaurant.Kitchen,
                        b.Restaurant.MenuIds,
                        b.Restaurant.Photos,
                        b.Restaurant.Raiting,
                        b.Restaurant.StartWorkTime,
                        b.Restaurant.EndWorkTime,
                        b.Restaurant.SchemeId
                        ).Value,
                    b.TableId,
                    b.Date,
                    b.StartTime,
                    b.EndTime,
                    b.PersonsNumber,
                    b.Status
                    ).Value)
                .ToList();

            return bookings;
        }

        public async Task<Guid> Create(Booking booking)
        {
            var bookingEntity = new BookingEntity
            {
                Id = booking.Id,
                UserId = booking.UserId,
                RestaurantId = booking.RestaurantId,
                TableId = booking.TableId,
                Date = booking.Date,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                PersonsNumber = booking.PersonsNumber,
                Status = booking.Status
            };

            await _dbContext.AddAsync(bookingEntity);
            await _dbContext.SaveChangesAsync();

            return booking.Id;
        }

        public async Task<Guid?> Update(Guid id, int status)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null) return null;

            booking.Status = status;

            await _dbContext.SaveChangesAsync();

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _dbContext.Bookings
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
