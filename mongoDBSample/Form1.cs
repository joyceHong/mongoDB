using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mongoDBSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string connString = "mongodb://192.168.1.63:27017/blog";
                MongoClient _client = new MongoClient(connString);
                MongoServer _server = _client.GetServer();
                _server.Connect();
                // 連接到 db
                MongoDatabase db = _server.GetDatabase("blog");
                MongoCollection collection = db.GetCollection("Post");


                //新增資料
                BsonDocument inserData1 = new BsonDocument{
                    {"Title","c# "},
                    {"Body","this is c#"},
                };
                collection.Insert(inserData1);

                                
                ////刪除資料
                //var query = Query<Post>.EQ(p => p.Title, "c#");
                //collection.Remove(query);

                //修改資料
                //var query = Query<Post>.EQ(p => p.Title, "c# ");
                //var update = Update<Post>.Set(p => p.Title, "c# update");
                //collection.Update(query, update);

                //Post postObj
                //person.Name = "Jim";
                ObjectId objctID= new ObjectId("54a3699d0e925b162c4e2fdd");

                collection.Save(new Post()
                {
                     Id = objctID,
                     Title="c# ",
                     Body="update c# ''",
                     CharCount=10,                  
                });

                //查詢資料
                MongoCursor<Post> cursor = collection.FindAllAs<Post>();
                var list = cursor.ToList();
                radGridView1.DataSource = list.ToArray();

                _server.Disconnect();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


    public class Post
    {
       
        public ObjectId Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Body
        {
            get;
            set;
        }

        public int CharCount
        {
            get;
            set;
        }

        public IList<Comment> Comments
        {
            get;
            set;
        }
    }


    public class Comment
    {
        public DateTime TimePosted
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Body
        {
            get;
            set;
        }
    }
}
