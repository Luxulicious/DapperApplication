using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using DapperApplication.Models;

namespace DapperApplication.Repository {
    public class EmployeeRepository {
        private SqlConnection con;
        private string connectionName = "MySQLConnection";

        private string ConnectionString(string connectionName) {
            this.connectionName = connectionName;
            string conStr = ConfigurationManager.ConnectionStrings[connectionName].ToString();
            return conStr;
        }

        private void Connect() {
            con = new SqlConnection(ConnectionString(connectionName));
        }

        public void AddEmployee(EmployeeModel employee) {

            try {
                Connect();
                con.Open();
                con.Execute("AddEmployee", employee, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) {
                //TODO Log this somewhere
                throw ex;
            } finally {
                con.Close();
            }
        }

        public List<EmployeeModel> GetEmployees() {

            try {
                Connect();
                con.Open();
                IList<EmployeeModel> employees = SqlMapper.Query<EmployeeModel>(con, "GetEmployees").ToList();
                return employees.ToList();
            }
            catch (Exception ex) {
                //TODO Log this somewhere
                throw ex;
            } finally {
                con.Close();
            }
        }

        public void UpdateEmployee(EmployeeModel employee) {

            try {
                Connect();
                con.Open();
                con.Execute("UpdateEmployee", employee, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) {
                //TODO Log this somewhere
                throw ex;
            } finally {
                con.Close();
            }
        }

        public void DeleteEmployee(int id) {
            try {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", id);
                Connect();
                con.Open();
                con.Execute("DeleteEmployee", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) {
                //TODO Log this somewhere
                throw ex;
            } finally {
                con.Close();
            }
        }
    }
}