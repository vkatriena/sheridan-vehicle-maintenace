using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Assignment2_KatrinaMehta
{
    public class Inventory
    {
        private Vehicle _vehicle;
        private SqlConnection _conn;
        private SqlDataAdapter _adapter;
        private SqlCommandBuilder _cmdBuilder;
        private DataSet _dataSet;
        private DataTable _tblInventory;

        public Inventory()
        {
            string cs = GetConnectionString();
            string query = "Select * from tblInventory";

            _vehicle = new Vehicle();
            _conn = new SqlConnection(cs);
            _adapter = new SqlDataAdapter(query, _conn);
            _cmdBuilder = new SqlCommandBuilder(_adapter);

            FillDataSet();
        }

        private string GetConnectionString()
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
            _tblInventory = _dataSet.Tables[0];

            //define primary key
            DataColumn[] pk = new DataColumn[1];
            pk[0] = _tblInventory.Columns["ID"];
            _tblInventory.PrimaryKey = pk;
        }

        public void GetAllInventory()
        {
            //Imsert Header
            foreach (DataRow row in _tblInventory.Rows)
            {
                Console.WriteLine($"{row["ID"],-5}{row["vehicleID"],-5}{row["numberOnHand"],-5}" +
                    $"{row["price"],-13}{row["cost"],-10}");
            }
        }

        public bool InventoryExists(int id)
        {
            DataRow row = _tblInventory.Rows.Find(id);
            if (row != null)
                return true;
            else
                return false;
        }

        public void GetInventoryById(int id)
        {
            DataRow row = _tblInventory.Rows.Find(id);

            if (row != null)
            {
                Console.WriteLine($"{row["ID"],-5}{row["vehicleID"],-5}{row["numberOnHand"],-5}" +
                $"{row["price"],-15}{row["cost"],-15}");
            }
        }

        public void InsertInventory(int vehicleID, int numberHand, decimal price, decimal cost)
        {
            try
            {
                if (!_vehicle.VehicleExists(vehicleID))
                {
                    throw new Exception("Vehicle does not exist. Please try again");
                }
                if (numberHand < 0)
                {
                    throw new Exception("Number on hand must be greater than 0. Please try again");

                }
                if (price < 0.0M || cost < 0.0M)
                {
                    throw new Exception("Price and cost must be greater than 0. Please try again");
                }
                DataRow newRow = _tblInventory.NewRow();
                newRow["ID"] = 0;

                newRow["vehicleID"] = vehicleID;
                newRow["numberOnHand"] = numberHand;
                newRow["price"] = price;
                newRow["cost"] = cost;
                _tblInventory.Rows.Add(newRow);

                _adapter.InsertCommand = _cmdBuilder.GetInsertCommand();
                _adapter.Update(_tblInventory);

                FillDataSet();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        public void UpdateInventory(int id, int vehicleId, int numHand, decimal price, decimal cost)
        {
            try
            {
                if (!_vehicle.VehicleExists(vehicleId))
                {
                    throw new Exception("Vehicle does not exist. Please try again");
                }
                if (numHand < 0)
                {
                    throw new Exception("Number on hand must be greater than 0. Please try again");

                }
                if (price < 0.0M || cost < 0.0M)
                {
                    throw new Exception("Price and cost must be greater than 0. Please try again");
                }

                DataRow row = _tblInventory.Rows.Find(id);

                if (row != null)
                {
                    row["vehicleID"] = vehicleId;
                    row["numberOnHand"] = numHand;
                    row["price"] = price;
                    row["cost"] = cost;

                    _adapter.UpdateCommand = _cmdBuilder.GetUpdateCommand();
                    _adapter.Update(_tblInventory);

                    FillDataSet();
                }
                else
                    throw new Exception ("\nInvalid Inventory ID\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void DeleteInventory(int id)
        {
            try
            {
                if (id < 0)
                    throw new Exception("\nInvalid Inventory ID entered. Please try again.\n");
                else if (id <= 5 && id >= 1)
                    throw new Exception("\nInvalid Inventroy ID entered. Please try again\n.");
                else
                {
                    DataRow row = _tblInventory.Rows.Find(id);
                    if (row != null)
                    {
                        row.Delete();

                        _adapter.DeleteCommand = _cmdBuilder.GetDeleteCommand();
                        _adapter.Update(_tblInventory);

                        FillDataSet();
                    }
                    else
                        throw new Exception("\nInventory ID does not exist\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nInvalid data entered.\n");
            }
        }
    }
        
}
