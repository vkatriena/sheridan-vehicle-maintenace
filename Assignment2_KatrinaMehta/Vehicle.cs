using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Assignment2_KatrinaMehta
{

    public class Vehicle
    {
        private SqlConnection _conn;
        private SqlDataAdapter _adapter;
        private SqlCommandBuilder _cmdBuilder;
        private DataSet _dataSet;
        private DataTable _tblVehicles;

        public Vehicle()
        {
            string cs = GetConnectionString();
            string query = "Select * from tblVehicle";

            _conn = new SqlConnection(cs);
            _adapter = new SqlDataAdapter(query, _conn);
            _cmdBuilder = new SqlCommandBuilder(_adapter);

            FillDataSet();
        }

        static string GetConnectionString()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("config.json");
            IConfiguration config = configurationBuilder.Build();

            return config["ConnectionStrings:CarRepairMdf"];
        }

        private void FillDataSet()
        {
            _dataSet = new DataSet();

            _adapter.Fill(_dataSet);
            _tblVehicles = _dataSet.Tables[0];

            //define primary key
            DataColumn[] pk = new DataColumn[1];
            pk[0] = _tblVehicles.Columns["ID"];
            _tblVehicles.PrimaryKey = pk;
        }

        public void GetAllVehicles()
        {
            foreach (DataRow row in _tblVehicles.Rows)
            {
                Console.WriteLine($"{row["ID"],-5}{row["make"],-15}{row["model"],-10}{row["year"],-5}{row["condition"],-10}");

            }
        }
        public bool VehicleExists(int id)
        {
            DataRow row = _tblVehicles.Rows.Find(id);
            if (row != null)
                return true;
            else 
                return false;


        }

        public void GetVehicleById(int id)
        {
            DataRow row = _tblVehicles.Rows.Find(id);

            if (row != null)
            {
                Console.WriteLine($"{row["ID"],-5}{row["make"],-15}{row["model"],-10}{row["year"],-5}{row["condition"],-10}");
            }
            else
                Console.WriteLine("\nInvalid Vehicle ID. Please try again\n");
        }

        public void InsertVehicle(string make, string model, string year, string condition)
        {
            try
            {

                DataRow newRow = _tblVehicles.NewRow();
                newRow["ID"] = 0;
                newRow["make"] = make.ToUpper();
                newRow["model"] = model.ToUpper();
                newRow["year"] = year;
                newRow["condition"] = condition.ToUpper();
                _tblVehicles.Rows.Add(newRow);

                _adapter.InsertCommand = _cmdBuilder.GetInsertCommand();
                _adapter.Update(_tblVehicles);


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void UpdateVehicle(int id, string make, string model, string year, string condition)
        {
            try
            {
                if (id < 0)
                    throw new Exception("\nInvalid Vehicle ID entered.\n");
                else
                {
                    DataRow row = _tblVehicles.Rows.Find(id);

                    if (row != null)
                    {
                        row["make"] = make.ToUpper();
                        row["model"] = model.ToUpper();
                        row["year"] = year;
                        row["condition"] = condition.ToUpper();
                        _adapter.UpdateCommand = _cmdBuilder.GetUpdateCommand();
                        _adapter.Update(_tblVehicles);

                        FillDataSet();
                    }
                    else
                        throw new Exception("\nVehicle ID does not exist. Please try again\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void DeleteVehicle(int id)
        {
            try 
            {
                if (id < 0)
                    throw new Exception("\nInvalid Vehicle ID entered. Please try again.\n");
                else if (id <= 5 && id >= 1)
                    throw new Exception("\nInvalid Vehicle ID entered. Please try again\n.");
                else
                {
                    DataRow row = _tblVehicles.Rows.Find(id);
                    if (row != null)
                    {
                        row.Delete();

                        _adapter.DeleteCommand = _cmdBuilder.GetDeleteCommand();
                        _adapter.Update(_tblVehicles);

                        FillDataSet();
                    }
                    else
                        throw new Exception("\nVehicle ID does not exist\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }
    }

}
