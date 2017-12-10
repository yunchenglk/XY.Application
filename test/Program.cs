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

            var _db = new wx_userweixinService();
            MongoServer mongodb = MongoServer.Create("mongodb://39.106.117.151:27017"); // 连接数据库
            MongoDatabase mongoDataBase = mongodb.GetDatabase("0359idatabase"); // 选择数据库名
            MongoCollection mongoCollection = mongoDataBase.GetCollection("wx_userweixin"); // 选择集合，相当于表

            var i = 0;
            _db.GetEnum().ToList().ForEach(m =>
            {
                string json = JsonHelper.SerializeObject(m);
                var document = BsonDocument.Parse(json);
                document.Add("_id", new ObjectId(m.ID.ToString().Replace("-", "").Substring(0, 24)));
                mongoCollection.Save(document);
                i++;
                Console.WriteLine(i + "：" + m.ID.ToString().Replace("-", "").Substring(0, 24));
            });
            Console.WriteLine("处理完毕");
            Console.Read();
        }
    }

}
