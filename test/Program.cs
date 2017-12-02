using MongoDB.Driver;
using System;
using XY.Entity;
using XY.Util;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Role role = new Role(); 
            role.Name = "名称";



            MongoServer mongodb = MongoServer.Create("mongodb://39.106.117.151:27017"); // 连接数据库
            MongoDatabase mongoDataBase = mongodb.GetDatabase("0359idatabase"); // 选择数据库名
            MongoCollection mongoCollection = mongoDataBase.GetCollection("user"); // 选择集合，相当于表

            Console.WriteLine(JsonHelper.SerializeObject(role));
            var json = JsonHelper.SerializeObject(role);
            mongodb.Connect();
            mongoCollection.Insert(role);
            Console.WriteLine("添加成功");

        }
    }

}
