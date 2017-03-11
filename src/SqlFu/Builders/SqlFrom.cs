using System;
using SqlFu.Builders.Crud;
using SqlFu.Builders.Expressions;
using SqlFu.Configuration;
using SqlFu.Providers;

namespace SqlFu.Builders
{
    public class SqlFrom : IBuildQueryFrom
    {
        private readonly IDbProvider _provider;
        private readonly ITableInfoFactory _infos;

        public SqlFrom(IDbProvider provider,ITableInfoFactory infos)
        {
            _provider = provider;
            _infos = infos;
        }

        public IWhere<T> From<T>(Action<IHelperOptions> cfg = null)
        {
            var data = new HelperOptions();
            cfg?.Invoke(data);
            return SqlBuilder<T>(data);
        }
        public IWhere<T> FromTemplate<T>(IQueryTemplate<T> template, Action<IHelperOptions> cfg = null)
        {
            var data = new HelperOptions();
            cfg?.Invoke(data);
            return SqlBuilder<T>(data, template);

        }


        IWhere<T> SqlBuilder<T>(HelperOptions options, IQueryTemplate<T> template = null) => new SimpleSqlBuilder<T>(
                options, _provider, _infos.GetInfo(typeof(T))
                , new ExpressionSqlGenerator(_provider.ExpressionsHelper, _infos, _provider)
                , template);

        public IWhere<T> FromAnonymous<T>(T anon, Action<IHelperOptions> options) where T : class
        {
            var data=new HelperOptions();
            options.MustNotBeNull();
            options(data);
            return SqlBuilder<T>(data);
        }
    }
}