using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;



namespace MAS_PROJECT.dbmagazyn
{
    /// <summary>
    /// This class represents a model of our database. Is required by EF core.
    /// </summary>
    public partial class dbmagazynContext : DbContext
    {
        public dbmagazynContext()
        {
        }

        public dbmagazynContext(DbContextOptions<dbmagazynContext> options)
            : base(options)
        {
            
        }

        /// <summary>
        /// This is representation of tables in database.
        /// </summary>
        public virtual DbSet<Producent> Producents { get; set; }
        public virtual DbSet<ElementOdziezy> ElementyOdzieziezy { get; set; }
        public virtual DbSet<Garnitur> Garnitury { get; set; }
        public virtual DbSet<Akcesoria> Akcesoria { get; set; }
        public virtual DbSet<Osoba> Osoby { get; set; }
        public virtual DbSet<Zamowienie> Zamowienia { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //INSERT HERE YOUR CONNECTION STRING
            var constring = "CONSTRING";
            //migrations don't work with this line (nullref exception)
            // optionsBuilder.UseMySQL(ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString);
           // optionsBuilder.UseMySQL(constring);
            
            //using Lazy Loading Proxies
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseMySQL(constring);
        }

        /// <summary>
        /// This metod actually creates a database model using EF core with Fluent API. 
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Producent Table
            modelBuilder.Entity<Producent>(entity => {

                entity.ToTable("producenci");

                entity.Property(e => e.nazwa)
                .HasColumnName("nazwa")
                .HasColumnType("varchar(30)")
                .IsRequired();

                

            });

            //Element odziezy Table           
            modelBuilder.Entity<ElementOdziezy>(entity => {

                entity.ToTable("elementyodziezy");

                entity.HasKey(e => e.TowarId);

                entity.Property(e => e.rozmiar)
                .HasColumnName("rozmiar")
                .HasColumnType("varchar(3)")
                .IsRequired();

                //mapping argb of color instead of color
                entity.Property(e => e.Argb)
                .HasColumnName("kolor")
                .HasColumnType("varchar(30)")
                .IsRequired();
         
                entity.Property(e => e.opis)
                .HasColumnName("opis")
                .HasColumnType("varchar(100)")
                .IsRequired().HasMaxLength(100);

                entity.Property(e => e.nazwa)
               .HasColumnName("nazwa")
               .HasColumnType("varchar(30)")
               .IsRequired();

                entity.Property(e => e.koszt)
               .HasColumnName("koszt")
               .HasColumnType("double")
               .IsRequired();

                entity.Property(e => e.czyZarezerwowany)
                .HasColumnName("czyZarezerwowany")
                .HasColumnType("boolean")
                .IsRequired()
                .HasDefaultValue(false);

                entity.Ignore(e => e.className);

                // Using TPH inheritance
                entity.HasDiscriminator<int>("elementodziezyType")
                .HasValue<Marynarka>(1)
                .HasValue<Spodnie>(2)
                .HasValue<DodatkowyElement>(3)
                .HasValue<Kamizelka>(4)
                .HasValue<Koszula>(5);               
                   
         
            });


            //Garnitur Table
            modelBuilder.Entity<Garnitur>(entity => 
            {
                entity.ToTable("garnitury");

                entity.HasKey(e => e.GarniturId);

                entity.HasOne(e => e.Marynarka)
                .WithOne(e => e.Garnitur)
                .HasForeignKey<Garnitur>(e => e.MarynarkaId)
                .IsRequired(false);

                entity.HasOne(e => e.Spodnie)
                .WithOne(e => e.Garnitur)
                .HasForeignKey<Garnitur>(e => e.SpodnieId)
                .IsRequired(false); ;

                entity.HasOne(e => e.DodatkowyElement)
                .WithOne(e => e.Garnitur)
                .HasForeignKey<Garnitur>(e => e.DodatkowyElementId)
                .IsRequired(false);

                entity.Property(e => e.nazwa)
                .HasColumnName("nazwa")
                .HasColumnType("varchar(30)")
                .IsRequired();

                entity.Property(e => e.opis)
                .HasColumnName("opis")
                .HasColumnType("varchar(200)")
                .HasMaxLength(2000);

                entity.Property(e => e.koszt)
                 .HasColumnName("koszt")
                 .HasColumnType("double")
                 .IsRequired();

              


            });

            //Akcesoria Table
            modelBuilder.Entity<Akcesoria>(entity => 
            {
                entity.ToTable("akcesoria");

                entity.HasKey(e => e.TowarId);

                entity.Property(e => e.nazwa)
                .HasColumnName("nazwa")
                .HasColumnType("varchar(30)")
                .IsRequired();

                entity.Property(e => e._rodzaj)
                .HasColumnName("rodzaj")
                .HasColumnType("varchar(30)")
                .IsRequired();

                entity.Property(e => e.koszt)
                .HasColumnName("koszt")
                .HasColumnType("double")
                .IsRequired();

                entity.HasOne(e => e.Garnitur)
                 .WithMany(e => e.Akcesorias)
                 .OnDelete(DeleteBehavior.Cascade)
                 .IsRequired(false);

                

                entity.Property(e => e.czyZarezerwowany)
               .HasColumnName("czyZarezerwowany")
               .HasColumnType("boolean")
               .IsRequired()
               .HasDefaultValue(false);

                entity.Ignore(c => c.className);

            });

            //Osoba Table
            modelBuilder.Entity<Osoba>(entity =>
            {
                entity.ToTable("osoby");

                entity.HasKey(e => e.OsobaId);

                entity.Property(e => e.imie)
                .HasColumnName("imie")
                .HasColumnType("varchar(30)")
                .IsRequired();

                entity.Property(e => e.nazwisko)
                .HasColumnName("nazwisko")
                .HasColumnType("varchar(30)")
                .IsRequired();

                entity.Property(e => e.nr_telefonu)
                .HasColumnName("nr_telefonu")
                .HasColumnType("varchar(15)")
                .IsRequired();

                entity.Property(e => e.adresa)
               .HasColumnName("adresa")
               .HasColumnType("varchar(50)")
               .IsRequired();

                entity.Property(e => e.OsobaTypes)
               .HasColumnName("osobaTyp")
               .HasColumnType("int")
               .IsRequired();

                entity.Property(e => e._procent_rabatu)
                .HasColumnName("procent_rabatu")
                .HasColumnType("int");

                entity.Property(e => e.suma_all_zakupow)
                .HasColumnName("suma_all_zakupow")
                .HasColumnType("double");

                entity.Property(e => e.data_zatrudnienia)
                .HasColumnName("data_zatrudnienia")
                .HasColumnType("date");

                entity.Property(e => e.pensja)
                .HasColumnName("pensja")
                .HasColumnType("int");

                entity.Property(e => e.il_zamowien)
                .HasColumnName("il_zamowien")
                .HasColumnType("int");

                 entity.Property(e => e._staz)
                .HasColumnName("staz")
                .HasColumnType("int");

                entity.HasMany(e => e.ZamowieniesKlient)
                .WithOne(e => e.OsobaKlient)
                .HasForeignKey(e => e._OsobaKlientOsobaId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.ZamowieniesPracownik)
               .WithOne(e => e.OsobaPracownik)
               .HasForeignKey(e => e._OsobaPracownikOsobaId)
               .OnDelete(DeleteBehavior.Cascade);
            });

            //Zamowienia Table
            modelBuilder.Entity<Zamowienie>(entity =>
            {
                entity.ToTable("zamowienia");

                entity.HasKey(e => e.ZamowienieId);

                entity.Property(e => e.opis)
                .HasColumnName("opis")
                .HasColumnType("varchar(200)")
                .HasMaxLength(200);
                

                entity.Property(e => e._status)
                .HasColumnName("status")
                .HasColumnType("varchar(30)")
                .IsRequired();

                entity.HasOne(e => e.OsobaPracownik)
                 .WithMany(e => e.ZamowieniesPracownik)
                 .HasForeignKey(e => e._OsobaPracownikOsobaId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .IsRequired(false);

                entity.HasOne(e => e.OsobaKlient)
                .WithMany(e => e.ZamowieniesKlient)
                .HasForeignKey(e => e._OsobaKlientOsobaId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

              
      

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
