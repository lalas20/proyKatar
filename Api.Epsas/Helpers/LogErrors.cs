
using Api.Epsas.Interfaces.common;
using Npgsql;

namespace Api.Epsas.Helpers
{
    public  class LogErrors : ILogErrors
    {


        public void Register(Exception e)
        {

            if (e.InnerException.Source.Equals("Npgsql")) {


                switch (Int32.Parse(((PostgresException)e.InnerException).Code))
                {
                    case 23505:
                        throw new Exception($"{((Npgsql.PostgresException)e.InnerException).ConstraintName.ToUpper().Replace('_',' ')} DUPLICADO");
                    case 22001:
                        throw new Exception(e.InnerException?.Message);
                    case 23503:
                        throw new Exception($"{Resources.Exceptions.pg_23503.ToString()} {((Npgsql.PostgresException)e.InnerException).ConstraintName.ToUpper().Replace("_FK", " ")}");
                    default:
                        break;
                }

            }

            throw new NotImplementedException(e.Message);
        }
    }
}
