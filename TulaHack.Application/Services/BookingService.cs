﻿using TulaHack.Core.Models;
using TulaHack.DataAccess.Repositories;

namespace TulaHack.Application.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;

        public BookingService(BookingRepository usersRepository)
        {
            _bookingRepository = usersRepository;
        }

        public async Task<Booking?> GetBooking(Guid id)
        {
            return await _bookingRepository.GetById(id);
        }

        public async Task<List<Booking>> GetBookingsByRestaurantId(Guid id)
        {
            return await _bookingRepository.GetByRestaurantId(id);
        }

        public async Task<Guid> CreateBooking(Booking booking)
        {
            return await _bookingRepository.Create(booking);
        }

        public async Task<Guid?> UpdateBooking(Guid id, int status)
        {
            return await _bookingRepository.Update(id, status);
        }

        public async Task<Guid> DeleteBooking(Guid id)
        {
            return await _bookingRepository.Delete(id);
        }
    }
}
