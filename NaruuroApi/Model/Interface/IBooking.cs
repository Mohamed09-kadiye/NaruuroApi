namespace NaruuroApi.Model.Interface
{
    public interface IBooking
    {
        
            List<Booking> GetBookings();
            Booking GetBookingById(int ID);
            void UpdateBooking(Booking booking);
            void AddBooking(Booking booking);
            void DeleteBooking(int ID);


        void ExecuteRefreshProcedure();

    }
}
