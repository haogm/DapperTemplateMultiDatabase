﻿using System;
using System.Collections;

namespace DapperExt
{
    public class SqlMapper
    {
        private static readonly Lazy<SqlMapper> lazy = new Lazy<SqlMapper>(() => new SqlMapper());
        public static SqlMapper Instance
        {
            get { return lazy.Value; }
        }
        private static readonly object LockObject = new object();
        private readonly Hashtable _ht = new Hashtable();

        public DbHelperSql GetDbHelperSql(EnumDbName dbName)
        {
            var val = _ht[dbName];
            if (val == null)
            {
                lock (LockObject)
                {
                    val = _ht[dbName];
                    if (val == null)
                    {
                        var name = dbName.ToString("G");
                        var readerConn = ConncetionReader(name);
                        var writerConn = GetConncetionWriter(name);
                        val = new DbHelperSql(readerConn,writerConn);
                        _ht[dbName] = val;
                    }
                }
            }
            return (DbHelperSql)val;
        }

        private string GetConncetionWriter(string name)
        {
            //TODO 数据库读取连接--可以是配置文件也可以是配置中心->根据实际情况添加 
            const string connStr = "Server=.;Database=Test3;uid=sa;pwd=123;Timeout=36000;Max Pool Size=512;";
            //const string connStr = "Server=0.0.0.0,10086;Database=Test3;uid=sa;pwd=xxx;Timeout=36000;Max Pool Size=512;";
            return connStr;
        }

        private string ConncetionReader(string name)
        {
            //TODO 数据库读取连接--可以是配置文件也可以是配置中心->根据实际情况添加 
            const string connStr = "Server=.;Database=Test3;uid=sa;pwd=123;Timeout=36000;Max Pool Size=512;";
            //const string connStr = "Server=0.0.0.0,10086;Database=Test3;uid=sa;pwd=xxx;Timeout=36000;Max Pool Size=512;";
            return connStr;
        }
    }
}