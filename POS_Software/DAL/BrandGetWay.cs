﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DAL
{
    class BrandGetWay:MyBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }

        public bool Insert()
        {
            MyCommand = CommandBuilder("insert into brand(name, origin) values(@name, @origin)");
            MyCommand.Parameters.AddWithValue("@name", Name);
            MyCommand.Parameters.AddWithValue("@origin", Origin);

            return ExecuteNQ(MyCommand);
        }

        public bool Update()
        {
            MyCommand = CommandBuilder("update brand set name = @name, origin = @origin where id = @id");
            MyCommand.Parameters.AddWithValue("@id", Id);
            MyCommand.Parameters.AddWithValue("@name", Name);
            MyCommand.Parameters.AddWithValue("@origin", Origin);

            return ExecuteNQ(MyCommand);
        }

        public bool Delete()
        {
            MyCommand = CommandBuilder("delete from brand where id=@id");

            MyCommand.Parameters.AddWithValue("@id", Id);

            return ExecuteNQ(MyCommand);
        }

        public bool SelectById()
        {
            MyCommand = CommandBuilder("select id, name, origin from brand where id = @id");

            MyCommand.Parameters.AddWithValue("@id", Id);


            MyReader = ExecuteReader(MyCommand);

            while (MyReader.Read())
            {
                Name = MyReader["name"].ToString();
                Origin = MyReader["origin"].ToString();
                return true;
            }
            return false;
        }

        public DataSet Select()
        {
            MyCommand = CommandBuilder("select id, name, origin from brand");
            if (!string.IsNullOrEmpty(Search))
            {
                MyCommand.CommandText += " where (name like @search or origin like @search)";
                MyCommand.Parameters.AddWithValue("@search", "%" + Search + "%");
            }
            return ExecuteDataSet(MyCommand);
        }
    }
}
