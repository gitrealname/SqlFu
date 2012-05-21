using System.Linq;
using CavemanTools.Testing;
using SqlFu;
using Tests;

namespace Benchmark.Tests
{
    public class SqlFuTests:PerformanceTests
    {
        private DbAccess _db;

        public SqlFuTests()
        {
            _db = new DbAccess(Config.Connex, DBType.SqlServer);
            _db.KeepAlive = true;
        }
        public override void FetchSingleEntity(BenchmarksContainer bc)
        {
            bc.Add(id=>
                       {
                           _db.Get<sfPosts>(5);
                       },"SqlFu Get");
            bc.Add(id=>
                       {
                           _db.FirstOrDefault<sfPosts>("select * from sfPosts where id=@0",5);
                       },"SqlFu FirstOrDefault");
        }

        public override void FetchSingleDynamicEntity(BenchmarksContainer bc)
        {
            bc.Add(id =>
            {
                _db.FirstOrDefault<dynamic>("select * from sfPosts where id=@0", 5);
            }, "SqlFu dynamic");
        }

        public override void QueryTop10(BenchmarksContainer bc)
        {
            bc.Add(id=>
                       {
                           _db.Query<sfPosts>("select top 10 * from sfposts where id>@0 order by id",5).ToArray();
                       },"SqlFu");
        }

        public override void QueryTop10Dynamic(BenchmarksContainer bc)
        {
            bc.Add(id =>
            {
                _db.Query<dynamic>("select top 10 * from sfposts where id>@0 order by id", 5).ToArray();
            }, "SqlFu");
        }

        public override void PagedQuery_Skip0_Take10(BenchmarksContainer bc)
        {
            bc.Add(id =>
            {
                _db.PagedQuery<sfPosts>(0,10,"select * from sfposts where id>@0 order by id", 5);
            }, "SqlFu");
        }

        public override void ExecuteScalar(BenchmarksContainer bc)
        {
            bc.Add(id =>
            {
                _db.ExecuteScalar<int>("select authorid from sfposts where id=@0 order by id", 5);
            }, "SqlFu scalar int");

            //bc.Add(id =>
            //{
            //    _db.ExecuteScalar<int?>("select topicid from sfposts where id=@0 order by id", 5);
            //}, "SqlFu scalar null to nullable");
            //bc.Add(id =>
            //{
            //    _db.ExecuteScalar<PostType>("select Type from sfposts where id=@0 order by id", 5);
            //}, "SqlFu scalar enum");
        }

        public override void MultiPocoMapping(BenchmarksContainer bc)
        {
            bc.Add(id =>
            {
                _db.FirstOrDefault<PostViewModel>("select *,id as Author_Id,title as Author_Name from sfposts where id=@0 order by id",5);
            }, "SqlFu");
        }

        public override void Inserts(BenchmarksContainer bc)
        {
            var p = sfPosts.Create();
            bc.Add(id =>
                       {
                           _db.Insert(p);
                       }, "SqlFu");
        }

        public override void Updates(BenchmarksContainer bc)
        {
            bc.Add(id =>
            {
                _db.Update<sfPosts>(new{Id=3,Title="updated"});
            }, "SqlFu");
        }
    }
}