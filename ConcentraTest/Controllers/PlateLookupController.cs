using AutoMapper;
using ConcentraTest.DTOs;
using ConcentraTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConcentraTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlateLookupController : ControllerBase
    {
        private readonly ConcentraContext _context;
        public PlateLookupController(ConcentraContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<PlateLookUp>> Get(string id)
        {
            IList<PlateLookUp> lst = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_context.Database.GetConnectionString()))
                {
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "USP_getPlates";
                        cmd.Parameters.AddWithValue("@ClientID", id);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Connection = con;

                        await con.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                lst = new List<PlateLookUp>();
                                while (await reader.ReadAsync())
                                {
                                    lst.Add(new PlateLookUp
                                    {
                                        PlateType = !await reader.IsDBNullAsync(reader.GetOrdinal("PlateType")) ? reader.GetString(reader.GetOrdinal("PlateType")) : String.Empty,
                                        Plate = !await reader.IsDBNullAsync(reader.GetOrdinal("Plate")) ? reader.GetString(reader.GetOrdinal("Plate")) : String.Empty,
                                        plateValue = !await reader.IsDBNullAsync(reader.GetOrdinal("plateValue")) ? reader.GetDecimal(reader.GetOrdinal("plateValue")) : 0,
                                        wDate = !await reader.IsDBNullAsync(reader.GetOrdinal("wDate")) ? reader.GetDateTime(reader.GetOrdinal("wDate")) : DateTime.MinValue,
                                        clientData = new ClientData
                                        {
                                            ClientId = !await reader.IsDBNullAsync(reader.GetOrdinal("ClientId")) ? reader.GetString(reader.GetOrdinal("ClientId")) : String.Empty,
                                            Name = !await reader.IsDBNullAsync(reader.GetOrdinal("Name")) ? reader.GetString(reader.GetOrdinal("Name")) : String.Empty,
                                            LastName= !await reader.IsDBNullAsync(reader.GetOrdinal("LastName")) ? reader.GetString(reader.GetOrdinal("LastName")) : String.Empty,
                                            PersonType = !await reader.IsDBNullAsync(reader.GetOrdinal("PersonType")) ? reader.GetString(reader.GetOrdinal("PersonType")) : string.Empty,
                                            Birthdate = !await reader.IsDBNullAsync(reader.GetOrdinal("Birthdate")) ? reader.GetDateTime(reader.GetOrdinal("Birthdate")) : DateTime.MinValue
                                        }
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return lst;
        }
    }
}
