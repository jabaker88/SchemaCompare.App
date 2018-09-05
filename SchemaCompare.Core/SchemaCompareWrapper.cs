using Microsoft.SqlServer.Dac.Compare;
using System;
using System.Data.SqlClient;

namespace SchemaCompare.Core
{
    public class SchemaCompareWrapper
    {
        private SchemaCompareDacpacEndpoint _sourceDacPac;
        private SchemaCompareDatabaseEndpoint _enpointDb;
        private SchemaComparisonResult _compareResult;
        private string _dacPacFileLocation;
        private string _enpointConnectionString;

        public SchemaCompareWrapper(string dacPacFileLocation, string enpointConnectionString)
        {
            _dacPacFileLocation = dacPacFileLocation;
            _enpointConnectionString = enpointConnectionString;
            _sourceDacPac = new SchemaCompareDacpacEndpoint(dacPacFileLocation);
            _enpointDb = new SchemaCompareDatabaseEndpoint(enpointConnectionString);
        }

        public SchemaComparisonResult Compare(string compareFileLocation = null)
        {
            var compare = new SchemaComparison(_sourceDacPac, _enpointDb);
            compare.Options.BlockOnPossibleDataLoss = false;
             
            if (compareFileLocation != null)
                compare.SaveToFile(compareFileLocation);

            compare = new SchemaComparison(compareFileLocation);

            _compareResult = compare.Compare();

            return _compareResult;
        }

        public SchemaComparePublishResult Publish()
        {
            return _compareResult.PublishChangesToTarget();
        }

        public SchemaCompareScriptGenerationResult Script()
        {
            var dbName = new SqlConnection(_enpointConnectionString).Database;

            return _compareResult.GenerateScript(dbName);
        }
    }
}
