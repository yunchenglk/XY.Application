using MongoDB.Driver;
using System;
using System.Collections.Generic;
using XY.Services;
using XY.Util;
using System.Linq;
using MongoDB.Bson;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            ClassService _db = new ClassService();
            // var entity = _db.GetEnum().FirstOrDefault();
            //var o = JsonHelper.Add(entity,"_id",Guid.NewGuid());




            MongoServer mongodb = MongoServer.Create("mongodb://39.106.117.151:27017"); // 连接数据库
            MongoDatabase mongoDataBase = mongodb.GetDatabase("0359idatabase"); // 选择数据库名
            MongoCollection mongoCollection = mongoDataBase.GetCollection("Class"); // 选择集合，相当于表


            mongodb.Connect();
            _db.GetEnum().ToList().ForEach(m =>
            {
                BsonDocument o = (BsonDocument)JsonHelper.Add(m, "_id", m.ID.ToString());
                mongoCollection.Insert<object>(o);
                Console.WriteLine(JsonHelper.SerializeObject(o));
            });



        }
    }

}
