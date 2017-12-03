using MongoDB.Driver;
using System;
using System.Collections.Generic;
using XY.Services;
using XY.Util;
using System.Linq;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            ClassService _db = new ClassService();
            _db.GetEnum().ToList().ForEach(m =>
            {
                Console.WriteLine(m.ID);
            });


            //MongoServer mongodb = MongoServer.Create("mongodb://39.106.117.151:27017"); // 连接数据库
            //MongoDatabase mongoDataBase = mongodb.GetDatabase("0359idatabase"); // 选择数据库名
            //MongoCollection mongoCollection = mongoDataBase.GetCollection("user"); // 选择集合，相当于表

            //Console.WriteLine(JsonHelper.SerializeObject(role));
            //var json = JsonHelper.SerializeObject(role);
            //mongodb.Connect();
            //mongoCollection.Insert(role);
            //Console.WriteLine("添加成功");

        }
    }

}
