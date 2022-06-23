using Kolokwium01.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Kolokwium01.Services
{
    public class DbService : IDbService 
    {
        private readonly IConfiguration _configuration;
        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Medicament GetMedicament(int idMedicament)
        {
            List<int> listOfPrescriptions = new List<int>();
            List<Prescription> prescriptions = new List<Prescription>();
            Medicament medicament = null;

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultCon")))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;

                com.CommandText = $"SELECT IdPrescription FROM Prescription_Medicament WHERE IdMedicament = {idMedicament}";
                con.Open();
                var dr = com.ExecuteReader();

                while (dr.Read())
                {
                    listOfPrescriptions.Add(int.Parse(dr["IdPrescription"].ToString()));
                }
                dr.Close();

                SqlCommand com2 = new SqlCommand();
                com2.Connection = con;
                int id = listOfPrescriptions[0];
                com2.CommandText = $"SELECT * FROM Prescription WHERE IdPrescription = {id}";
                var dr2 = com2.ExecuteReader();
                for (int i = 1; i < listOfPrescriptions.Count; i++)
                {
                    while (dr2.Read())
                    {
                        prescriptions.Add(new Prescription
                        {
                            IdPrescription = Int32.Parse(dr2["IdPrescription"].ToString()),
                            Date = DateTime.Parse(dr2["Date"].ToString()),
                            DueDate = DateTime.Parse(dr2["DueDate"].ToString()),
                            IdPatient = Int32.Parse(dr2["IdPatient"].ToString()),
                            IdDoctor = Int32.Parse(dr2["IdDoctor"].ToString())
                        });
                    }
                    id = listOfPrescriptions[i];
                    com2.CommandText = $"SELECT * FROM Prescription WHERE IdPrescription = {id}";
                }

                dr2.Close();

                SqlCommand com3 = new SqlCommand();
                com3.Connection = con;
                com3.CommandText = $"SELECT * FROM Medicament WHERE IdMedicament = @idMedicament";
                com3.Parameters.AddWithValue("@idMedicament", idMedicament);

                var dr3 = com3.ExecuteReader();
                while (dr3.Read())
                {
                    medicament = new Medicament
                    {
                        IdMedicament = Int32.Parse(dr3["IdMedicament"].ToString()),
                        Name = dr3["Name"].ToString(),
                        Description = dr3["Description"].ToString(),
                        Type = dr3["Type"].ToString(),
                        Prescriptions = prescriptions
                    };
                }
                dr3.Close();

            }

            return medicament;

        }





















    }
}
