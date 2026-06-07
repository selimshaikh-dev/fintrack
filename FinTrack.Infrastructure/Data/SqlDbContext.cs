using Dapper;
using FinTrack.Application.Common.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Data
{
    public abstract class SqlDbContext<TEntity> where TEntity : class
    {
        private readonly SqlConnection _con;
        private SqlTransaction _trans;

        protected SqlDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            _con = new SqlConnection(connectionString);
        }

        private async Task ConnectionOpenAsync()
        {
            if (_con.State == ConnectionState.Closed)
                await _con.OpenAsync();
        }

        private void ConnectionClose()
        {
            if (_con.State == ConnectionState.Open)
                _con.Close();
        }

        public async Task<(Task<IEnumerable<T1>>, Task<IEnumerable<T2>>, Task<IEnumerable<T2>>)> GetMultipleByQueryAsync<T1, T2, T3>(string sqlQuery, DynamicParameters parameter)
        {
            await using var connection = _con;

            try
            {
                await ConnectionOpenAsync();
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var resultList = await connection.QueryMultipleAsync(sqlQuery, parameter);

                var t1 = resultList.ReadAsync<T1>();
                var t2 = resultList.ReadAsync<T2>();
                var t3 = resultList.ReadAsync<T2>();

                ConnectionClose();
                return (t1, t2, t3);
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<(Task<IEnumerable<T1>>, Task<IEnumerable<T2>>, Task<IEnumerable<T2>>)> GetMultipleBySPAsync<T1, T2, T3>(string sqlQuery, DynamicParameters parameter)
        {
            await using var connection = _con;

            try
            {
                await ConnectionOpenAsync();
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var resultList = await connection.QueryMultipleAsync(sqlQuery, parameter, null, 180, CommandType.StoredProcedure);

                var t1 = resultList.ReadAsync<T1>();
                var t2 = resultList.ReadAsync<T2>();
                var t3 = resultList.ReadAsync<T2>();

                ConnectionClose();
                return (t1, t2, t3);
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<IEnumerable<TEntity>> GetListByQueryAsync(string sqlQuery, DynamicParameters parameter)
        {
            await using var connection = _con;

            try
            {
                await ConnectionOpenAsync();
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var resultList = await connection.QueryAsync<TEntity>(sqlQuery, parameter);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<IEnumerable<TEntity>> GetListBySPAsync(string sqlQuery, DynamicParameters parameter)
        {
            await using var connection = _con;

            try
            {
                await ConnectionOpenAsync();
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var resultList = await connection.QueryAsync<TEntity>(sqlQuery, parameter, null, 180, CommandType.StoredProcedure);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<TEntity> GetSingleByQueryAsync(string sqlQuery, DynamicParameters parameter)
           {
            await using var connection = _con;

            try
            {
                await ConnectionOpenAsync();
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var resultList = await connection.QueryFirstOrDefaultAsync<TEntity>(sqlQuery, parameter);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<TEntity> GetSingleBySPAsync(string sqlQuery, DynamicParameters parameter)
        {
            await using var connection = _con;

            try
            {
                await ConnectionOpenAsync();
                DefaultTypeMap.MatchNamesWithUnderscores = true;
                var resultList = await connection.QueryFirstOrDefaultAsync<TEntity>(sqlQuery, parameter, null, 180, CommandType.StoredProcedure);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<string> GetSingleStringByQueryAsync(string sqlQuery, DynamicParameters parameter)
        {
            await using var connection = _con;

            try
            {
                await ConnectionOpenAsync();
                var resultList = await connection.QueryFirstOrDefaultAsync<string>(sqlQuery, parameter);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<string> GetSingleStringBySPAsync(string sqlQuery, DynamicParameters parameter)
        {
            await using var connection = _con;

            try
            {
                await ConnectionOpenAsync();
                var resultList = await connection.QueryFirstOrDefaultAsync<string>(sqlQuery, parameter, null, 180, CommandType.StoredProcedure);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<Int32> GetSingleIntByQueryAsync(string sqlQuery, DynamicParameters parameter)
        {

            await using var connection = _con;
            try
            {
                await ConnectionOpenAsync();
                var resultList = await connection.QueryFirstOrDefaultAsync<Int32>(sqlQuery, parameter);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<Int32> GetSingleIntBySPAsync(string sqlQuery, DynamicParameters parameter)
        {

            await using var connection = _con;
            try
            {
                await ConnectionOpenAsync();
                var resultList = await connection.QueryFirstOrDefaultAsync<Int32>(sqlQuery, parameter, null, 180, CommandType.StoredProcedure);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<DateTime> GetSingleDateTimeByQueryAsync(string sqlQuery, DynamicParameters parameter)
        {

            await using var connection = _con;
            try
            {
                await ConnectionOpenAsync();
                var resultList = await connection.QueryFirstOrDefaultAsync<DateTime>(sqlQuery, parameter);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<DateTime> GetSingleDateTimeBySPAsync(string sqlQuery, DynamicParameters parameter)
        {

            await using var connection = _con;
            try
            {
                await ConnectionOpenAsync();
                var resultList = await connection.QueryFirstOrDefaultAsync<DateTime>(sqlQuery, parameter, null, 180, CommandType.StoredProcedure);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<Result> SetSingleAsync(string sqlQuery, DynamicParameters parameter)
        {
            await using var connection = _con;
            try
            {
                await ConnectionOpenAsync();
                await using (_trans = connection.BeginTransaction())
                {
                    try
                    {
                        var affectedRows = await connection.ExecuteAsync(sqlQuery, parameter, commandType: CommandType.StoredProcedure, transaction: _trans);
                        await _trans.CommitAsync();
                        ConnectionClose();
                        var result = parameter.Get<string>("@message");
                        return Result.Success(result);
                    }
                    catch (Exception ex)
                    {
                        await _trans.RollbackAsync();
                        return Result.Failure(new List<string> { ex.Message });
                    }
                }
            }
            catch (Exception ex)
            {
                ConnectionClose();
                return Result.Failure(new List<string> { ex.Message });
            }
            finally
            {
                ConnectionClose();
            }
        }
        public async Task<Result> SetMultipleAsync(string sqlQuery, List<DynamicParameters> parameter, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            await using (var connection = new SqlConnection(connectionString))

            {
                await connection.OpenAsync();
                await using (_trans = connection.BeginTransaction())
                {
                    try
                    {
                        var affectedRows = await connection.ExecuteAsync(sqlQuery, parameter, commandType: CommandType.StoredProcedure, transaction: _trans);
                        await _trans.CommitAsync();
                        ConnectionClose();
                        return Result.Success();
                    }
                    catch (Exception ex)
                    {
                        await _trans.RollbackAsync();
                        return Result.Failure(new List<string> { ex.Message });
                    }
                }
            }
        }
        public async Task<DateTime> GetServerDateTimeAsync(string sqlQuery, DynamicParameters parameter)
        {

            await using var connection = _con;
            try
            {
                await ConnectionOpenAsync();
                var resultList = await connection.QueryFirstOrDefaultAsync<DateTime>(sqlQuery, parameter);
                ConnectionClose();
                return resultList;
            }
            catch (Exception ex)
            {
                ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }
        }

        //public async Task<IEnumerable<T>> GetListAsync<T>(string sqlQuery, DynamicParameters parameter)
        //{
        //    await using var connection = _con;

        //    try
        //    {
        //        await ConnectionOpenAsync();
        //        DefaultTypeMap.MatchNamesWithUnderscores = true;
        //        var resultList = await connection.QueryAsync<T>(sqlQuery, parameter);
        //        ConnectionClose();
        //        return resultList;
        //    }
        //    catch (Exception ex)
        //    {
        //        ConnectionClose();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        ConnectionClose();
        //    }
        //}

        //public async Task<T> GetSingleAsync<T>(string sqlQuery, DynamicParameters parameter)
        //{
        //    await using var connection = _con;

        //    try
        //    {
        //        await ConnectionOpenAsync();
        //        DefaultTypeMap.MatchNamesWithUnderscores = true;
        //        var resultList = await connection.QueryFirstOrDefaultAsync<T>(sqlQuery, parameter,null,180,CommandType.StoredProcedure);
        //        ConnectionClose();
        //        return resultList;
        //    }
        //    catch (Exception ex)
        //    {
        //        ConnectionClose();
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        ConnectionClose();
        //    }
        //}

    }
}
