using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Sample05
{
    class Program
    {
        static void Main(string[] args)
        {
            //test_Inser();
            //test_mult_insert();
            //test_delete();
            //test_mult_delete();
            //test_update();
            //test_mult_update();
            //test_select_one();
            //test_select_mult();
            //test_insert_comment();
            test_select_content_with_comment();
            Console.ReadKey();
        }

        /// <summary>
        /// 测试单条插入
        /// </summary>
        static void test_Inser()
        {
            var content = new Content
            {
                title = "标题1",
                content = "内容1"
            };
            //示例
            using(var conn = new SqlConnection("Data Source=127.0.0.1;User ID=sa;Password=pwd123456;Initial Catalog=Mark.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"INSERT INTO [content]
                                      (title,[content],status,add_time,modify_time)
                                     Values(@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"测试插入：插入了{result}条数据");
            }
        }

        /// <summary>
        /// 测试批量插入
        /// </summary>
        static void test_mult_insert()
        {
            var contens = new List<Content>
            {
                new Content
                {
                    title="批量插入标题1",
                    content="批量插入内容1"
                },
                new Content
                {
                    title="批量插入标题2",
                    content="批量插入内容2"
                },
            };

            using(var conn = new SqlConnection("Data Source=127.0.0.1;User ID=sa;Password=pwd123456;Initial Catalog=Mark.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"INSERT INTO [content]
                                      (title,[content],status,add_time,modify_time)
                                     Values(@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, contens);
                Console.WriteLine($"测试批量插入：插入了{result}条数据");
            }
        }

        /// <summary>
        /// 测试删除单条数据
        /// </summary>
        static void test_delete()
        {
            var content = new Content()
            {
                id = 1,
            };

            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=sa;Password=pwd123456;Initial Catalog=Mark.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_delete = @"Delete [content]
                                      Where ID = @id";
                var result = conn.Execute(sql_delete, content);
                Console.WriteLine($"测试删除数据：删除了{result}条数据");
            }
        }

        /// <summary>
        /// 测试一次批量删除两条数据
        /// </summary>
        static void test_mult_delete()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                id=2,

            },
               new Content
            {
                id=3,

            },
        };

            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=sa;Password=pwd123456;Initial Catalog=Mark.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_delete = @"DELETE FROM [content]
WHERE   (id = @id)";
                var result = conn.Execute(sql_delete, contents);
                Console.WriteLine($"test_mult_del：删除了{result}条数据！");
            }
        }


        /// <summary>
        /// 测试修改单条数据
        /// </summary>
        static void test_update()
        {
            var content = new Content
            {
                id = 5,
                title = "标题5",
                content = "内容5",

            };
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=sa;Password=pwd123456;Initial Catalog=Mark.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_update = @"UPDATE  [Content]
SET         title = @title, [content] = @content, modify_time = GETDATE()
WHERE   (id = @id)";
                var result = conn.Execute(sql_update, content);
                Console.WriteLine($"test_update：修改了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试一次批量修改多条数据
        /// </summary>
        static void test_mult_update()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                id=6,
                title = "批量修改标题6",
                content = "批量修改内容6",

            },
               new Content
            {
                id =7,
                title = "批量修改标题7",
                content = "批量修改内容7",

            },
        };

            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=sa;Password=pwd123456;Initial Catalog=Mark.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_update = @"UPDATE  [Content]
SET         title = @title, [content] = @content, modify_time = GETDATE()
WHERE   (id = @id)";
                var result = conn.Execute(sql_update, contents);
                Console.WriteLine($"test_mult_update：修改了{result}条数据！");
            }
        }

        /// <summary>
        /// 查询单笔
        /// </summary>
        static void test_select_one()
        {
            using(var conn = new SqlConnection("Data Source=127.0.0.1;User ID=sa;Password=pwd123456;Initial Catalog=Mark.Cms;Pooling=true;Max Pool Size=100;"))
            {
                var sql_select = "select * from [content] where id=@id";
                var result = conn.QueryFirstOrDefault<Content>(sql_select, new { id = 4 });
                Console.WriteLine($"test_select_one：查到的数据为：标题：{result.title}，内容：{result.content}，状态：{result.status}");
            }
        }

        /// <summary>
        /// 查询多笔
        /// </summary>
        static void test_select_mult()
        {
            using(var conn = new SqlConnection("Data Source=127.0.0.1;User ID=sa;Password=pwd123456;Initial Catalog=Mark.Cms;Pooling=true;Max Pool Size=100;"))
            {
                var sql_select = "select * from [content] where id in @ids";
                var result = conn.Query<Content>(sql_select, new { ids = new int[] { 5, 6 } });
                foreach(var item in result)
                {
                    Console.WriteLine($"test_select_mult：标题：{item.title}，内容：{item.content}，状态：{item.status}");
                }
            }
        }

        /// <summary>
        /// 测试添加评论
        /// </summary>
        static void test_insert_comment()
        {
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=sa;Password=pwd123456;Initial Catalog=Mark.Cms;Pooling=true;Max Pool Size=100;"))
            {
                var comments = new List<Comment>
                {
                    new Comment
                    {
                         content_id = 5,
                         content="评论1"
                    },
                    new Comment
                    {
                        content_id = 5,
                        content = "评论2"
                    }
                };

                var sql_insert = @"INSERT INTO [comment] 
                               (content_id,content)
                               Values(@content_id,@content)";
                var result = conn.Execute(sql_insert, comments);
                Console.WriteLine($"test_insert_comment：插入评论数：{result}");
            }
        }

        /// <summary>
        /// 关联查询
        /// </summary>
        static void test_select_content_with_comment()
        {
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=sa;Password=pwd123456;Initial Catalog=Mark.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_select = @"select * from content where id=@id;
                                select * from comment where content_id = @id;";
                using (var result = conn.QueryMultiple(sql_select,new { id = 5}))
                {
                    var content = result.ReadFirstOrDefault<ContentWithComment>();
                    content.comments = result.Read<Comment>();
                    Console.WriteLine($"test_select_content_with_comment:内容5的评论数量{content.comments.Count()}");
                }
            }
        }
    }
}
