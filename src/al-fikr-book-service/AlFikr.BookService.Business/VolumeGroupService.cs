using AlFikr.BookService.Data.Models;
using AlFikr.BookService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlFikr.BookService.Business
{
    public class VolumeGroupService : ServiceBase<VolumeGroupService>
    {
        private readonly IConfiguration configuration;

        public VolumeGroupService(AlFikrContext alFikrContext, ILogger<VolumeGroupService> logger, IConfiguration configuration) : base(alFikrContext, logger)
        {
            this.configuration = configuration;
        }
   
        public IEnumerable<VolumeGroupEntity> GetVolumeGroups()
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    return connection.Query<VolumeGroupEntity>("SELECT * FROM VolumeGroup");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public VolumeGroupEntity GetVolumeGroup(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    return connection.QueryFirstOrDefault<VolumeGroupEntity>("SELECT * FROM VolumeGroup WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public int AddVolumeGroup(VolumeGroupEntity volumeGroup)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @" INSERT INTO VolumeGroup(IdDocument,NumVolume,IdGroupRefs) 
                                    VALUES(@IdDocument,@NumVolume,@IdGroupRefs)";

                    return connection.Execute(sql, volumeGroup);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public int UpdateVolumeGroup(VolumeGroupEntity volumeGroup)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @" UPDATE VolumeGroup 
                                    SET IdDocument=@IdDocument,
                                        NumVolume=@NumVolume,
                                        IdGroupRefs=@IdGroupRefs
                                    WHERE Id=@Id";

                    return connection.Execute(sql, volumeGroup);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public int DeleteVolumeGroup(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @"DELETE FROM VolumeGroup WHERE Id = @Id";

                    return connection.Execute(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
    }
}
