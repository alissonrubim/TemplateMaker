using System;
using System.Collections.Generic;
using System.Text;

namespace {{SolutionName}}.Oracle.Adapter.Helpers
{
    internal static class OracleHelper
    {
        const string ORACLE_TRUE = "Y";
        const string ORACLE_FALSE = "N";

        public static string GetOracleBoolean(bool value)
        {
            return value ? ORACLE_TRUE : ORACLE_FALSE;
        }

        public static bool GetBoolean(string value)
        {
            return value == ORACLE_TRUE;
        }
    }
}
