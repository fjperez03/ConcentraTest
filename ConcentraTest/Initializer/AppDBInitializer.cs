using ConcentraTest.Models;

namespace ConcentraTest.Initializer
{
    public class AppDBInitializer
    {
        public static void Seed(IApplicationBuilder app) { 
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ConcentraContext>();

                if (!context.Statuses.Any())
                {
                    context.Statuses.AddRange(new Status
                    {
                        Status1 = "Activo"
                    },
                    new Status
                    {
                        Status1 = "Inactivo"
                    });

                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new User
                    {
                        UserName = "Admin",
                        Password = "123456"
                    });

                    context.SaveChanges();
                }
                if (!context.PersonTypes.Any())
                {
                    context.PersonTypes.AddRange(new PersonType
                    {
                        StatusId = 1,
                        UserId = 1,
                        Wdate = DateTime.Now,
                        PersonType1 = "Persona Física"
                    },
                    new PersonType
                    {
                        StatusId = 1,
                        UserId = 1,
                        Wdate = DateTime.Now,
                        PersonType1 = "Persona Jurídica"
                    });

                    context.SaveChanges();
                }
                if (!context.VehicleTypes.Any())
                {
                    context.VehicleTypes.AddRange(new VehicleType
                    {
                        StatusId = 1,
                        UserId = 1,
                        Wdate = DateTime.Now,
                        VehicleType1 = "Publico",
                        PlateType = "A",
                        PlatePrice = 1200 
                    },
                    new VehicleType
                    {
                        StatusId = 1,
                        UserId = 1,
                        Wdate = DateTime.Now,
                        VehicleType1 = "Privado",
                        PlateType = "F",
                        PlatePrice = 1200
                    },
                    new VehicleType
                    {
                        StatusId = 1,
                        UserId = 1,
                        Wdate = DateTime.Now,
                        VehicleType1 = "Transporte",
                        PlateType = "T",
                        PlatePrice = 1500
                    },
                    new VehicleType
                    {
                        StatusId = 1,
                        UserId = 1,
                        Wdate = DateTime.Now,
                        VehicleType1 = "Pesado",
                        PlateType = "P",
                        PlatePrice = 2000
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
