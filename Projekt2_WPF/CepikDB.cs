using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_WPF
{
    class CepikDB : DbContext
    {
        public CepikDB() : base()
        {
            Database.SetInitializer<CepikDB>(new CreateDatabaseIfNotExists<CepikDB>());
        }
        public DbSet<Cars> Pojazdy { get; set; }
        public DbSet<Drivers> Kierowcy { get; set; }
        public DbSet<DrivingLicense> PrawoJazdy { get; set; }
        public DbSet<InsurancePolicy> Polisa { get; set; }
        public DbSet<TechnicalReview> BadanieTechniczne { get; set; }
        public DbSet<CarDocsAndInfo> Informacje { get; set; } 
        public DbSet<Users> Uzytkownicy { get; set; }
    }
}
