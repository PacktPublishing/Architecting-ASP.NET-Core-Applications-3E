using Core;
using Local;
using Sql;

var sqlDataPersistence = new SqlDataPersistence();
var localDataPersistence = new LocalDataPersistence();

var service = new SomeService();
service.Operation(localDataPersistence);
service.Operation(sqlDataPersistence);