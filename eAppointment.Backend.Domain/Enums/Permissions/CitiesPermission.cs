using Ardalis.SmartEnum;

namespace eAppointment.Backend.Domain.Enums.Permissions
{
    public sealed class CitiesPermission : SmartEnum<CitiesPermission>
    {
        public static readonly CitiesPermission GetAllCities = 
            new("Permissions.Cities.GetAllCities", 1);

        public CitiesPermission(string name, int value) : base(name, value)
        {
        }
    }
}
