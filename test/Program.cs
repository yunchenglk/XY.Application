using MongoDB.Driver;
using System;
using System.Collections.Generic;
using XY.Services;
using XY.Util;
using System.Linq;
using MongoDB.Bson;
using XY.Entity;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var _db = new AuthorityService();

            //MongoServer mongodb = MongoServer.Create("mongodb://39.106.117.151:27017"); // 连接数据库
            //MongoDatabase mongoDataBase = mongodb.GetDatabase("0359idatabase"); // 选择数据库名
            //MongoCollection mongoCollection = mongoDataBase.GetCollection("Authority"); // 选择集合，相当于表

            //_db.GetEnum().ToList().ForEach(m =>
            //{
            //    var document = m.ToBsonDocument();
            //    document.Add("_id", m.ID);
            //    mongoCollection.Save(document);
            //    Console.WriteLine(m.ID);
            //});

            var _db = new UserService();
            MongoServer mongodb = MongoServer.Create("mongodb://39.106.117.151:27017"); // 连接数据库
            MongoDatabase mongoDataBase = mongodb.GetDatabase("0359idatabase"); // 选择数据库名
            MongoCollection mongoCollection = mongoDataBase.GetCollection("User"); // 选择集合，相当于表

            var entity = _db.GetEnum().FirstOrDefault();
            string json = JsonHelper.SerializeObject(entity);
            Console.WriteLine(json);
            var document = BsonDocument.Parse(json);

            document.Add("_id", entity.ID);
            mongoCollection.Save(document);
            Console.WriteLine(document);


        }
    }

}
