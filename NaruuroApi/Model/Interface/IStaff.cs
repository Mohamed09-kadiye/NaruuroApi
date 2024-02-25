namespace NaruuroApi.Model.Interface
{
    public interface IStaff
    {

        List<StaffM> GetAllStaff();
        StaffM GetStaffById(int id);
        void AddStaff(StaffM staff);
        void UpdateStaff(StaffM staff);
        void DeleteStaff(int id);


    }
}