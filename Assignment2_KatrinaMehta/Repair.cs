using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Assignment2_KatrinaMehta
{
    public class Repair
    {
        private Inventory _inventory;
        private SqlConnection _conn;
        private SqlDataAdapter _adapter;
        private SqlCommandBuilder _cmdBuilder;
        private DataSet _dataSet;
        private DataTable _tblRepair;

        public Repair()
        {
            string cs = GetConnectionString();
            string query = "Select * from tblRepair";

            _inventory = new Inventory();
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
            _tblRepair = _dataSet.Tables[0];

            //define primary key
            DataColumn[] pk = new DataColumn[1];
            pk[0] = _tblRepair.Columns["ID"];
            _tblRepair.PrimaryKey = pk;
        }

        public void GetAllRepairs()
        {
            foreach(DataRow row in _tblRepair.Rows)
            {
                Console.WriteLine($"{row["ID"],-5}{row["inventoryID"],-5}{row["whatToRepair"],-30}");
            }
        }

        public void GetRepairById(int id)
        {
            DataRow row = _tblRepair.Rows.Find(id);

            if (row != null)
            {
                Console.WriteLine($"{row["ID"],-5}{row["inventoryID"],-5}{row["whatToRepair"],-30}");
            }
            else
                throw new Exception("\nInvalid Repair ID\n");
        }

        public void InsertRepair(int inventoryId, string repair)
        {
            try
            {
                if (!_inventory.InventoryExists(inventoryId))
                {
                    throw new Exception("\nInventory ID does not exist\n");
                }
                DataRow newRow = _tblRepair.NewRow();
                newRow["ID"] = 0;

                newRow["inventoryID"] = inventoryId;
                newRow["whatToRepair"] = repair;
                _tblRepair.Rows.Add(newRow);

                _adapter.InsertCommand = _cmdBuilder.GetInsertCommand();
                _adapter.Update(_tblRepair);

                FillDataSet();

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateRepair(int id, int inventoryId, string repair)
        {
            try
            {
                DataRow row = _tblRepair.Rows.Find(id);
                if (row != null)
                {
                    if (!_inventory.InventoryExists(inventoryId))
                    {
                        throw new Exception("\nVehicle ID does not exist\n");
                    }

                    row["inventoryID"] = inventoryId;
                    row["whatToRepair"] = repair;

                    _adapter.UpdateCommand = _cmdBuilder.GetUpdateCommand();
                    _adapter.Update(_tblRepair);

                    FillDataSet();
                }
                else
                {
                    throw new Exception("\nInvalid Repair ID");
                }

            }
            catch(Exception e)
            {
                Console.WriteLine("\nInvalid Data entered.\n");
            }
        }

        public void DeleteRepair(int id)
        {
            try
            {
                if (id < 0)
                    throw new Exception("\nInvalid Repair ID entered. Please try again.\n");
                else if (id <= 5 && id >= 1)
                    throw new Exception("\nInvalid Repair ID entered. Please try again\n.");
                else
                {
                    DataRow row = _tblRepair.Rows.Find(id);
                    if (row != null)
                    {
                        row.Delete();

                        _adapter.DeleteCommand = _cmdBuilder.GetDeleteCommand();
                        _adapter.Update(_tblRepair);

                        FillDataSet();
                    }
                    else
                        throw new Exception("\nRepair ID does not exist\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nInvalid Data entered.\n");
            }
        }
    }
    

   
}
