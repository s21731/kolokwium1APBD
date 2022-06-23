using Kolokwium01.Models;

namespace Kolokwium01.Services
{
    public interface IDbService
    {
        public Medicament GetMedicament(int idMedicament);
        public void DeletePatient(int idPatient);


    }
}
