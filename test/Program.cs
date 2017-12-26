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
            Company com = new Company();
            com.Name = "tes1t1";
            com.Email = "test11@163.com";
            var _db = new CompanyService();
            _db.Insert(com);





            //var _db = new P_TagsService();
            //string mongoStr = "mongodb://root:hao123.com@39.106.117.151:27017";

            //MongoUrl mongoUrl = new MongoUrl(mongoStr);
            //var mongoClient = new MongoClient(mongoUrl);
            //var database = mongoClient.GetDatabase("0359idatabase");



            //var i = 0;
            //_db.GetEnum().ToList().ForEach(m =>
            //{
            //    try
            //    {
            //        var collection = database.GetCollection<BsonDocument>("P_Tags");//.GetCollection("Class");
            //        string json = JsonHelper.SerializeObject(m);
            //        var document = BsonDocument.Parse(json);
            //        var id = new ObjectId(m.ID.ToString().Replace("-", "").Substring(0, 24));
            //        document.Add("_id", id);
            //        //collection.InsertOne(document);
            //        var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            //        var resut = collection.DeleteOne(filter);
            //        // collection.InsertOne(document);
            //        i++;
            //        Console.WriteLine(i + "：" + m.ID.ToString().Replace("-", "").Substring(0, 24));
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //});
            //Console.WriteLine("处理完毕");
            //Console.Read();
        }
    }

}
