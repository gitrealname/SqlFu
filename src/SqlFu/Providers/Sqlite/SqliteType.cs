using System;

namespace SqlFu.Providers.Sqlite
{
    public class SqliteType:DbTypes
    {
        public const string Integer = "Integer";
        public const string Real = "Real";
        public const string Text = "Text";
        public const string Blob = "Blob";
        public const string Numeric = "Numeric";

        public SqliteType()
        {
            this[typeof(bool)] = Integer;
			this[typeof(bool?)] = Integer;
			this[typeof(string)] = Text;
			this[typeof(float)] = Real;
			this[typeof(double)] = Real;
            this[typeof(int)] = Integer;
            this[typeof(int?)] = Integer;
            this[typeof(Guid)] = Blob;
            this[typeof(Guid?)] = Blob;
            this[typeof(byte[])] = Blob;
            this[typeof(DateTimeOffset)] = Text;
            this[typeof(DateTimeOffset?)] = Text;
            this[typeof(TimeSpan)] = Text;
            this[typeof(TimeSpan?)] = Text;
        }
    }
}