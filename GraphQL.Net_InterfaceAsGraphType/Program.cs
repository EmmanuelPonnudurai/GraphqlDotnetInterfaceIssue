using GraphQL.SystemTextJson;
using GraphQL.Types;
using System;
using System.Threading.Tasks;

namespace GraphQL.Net_InterfaceAsGraphType
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var schema = new Schema { Query = new StarWarsQuery() };
                var writer = new DocumentWriter();
                var json = await schema.ExecuteAsync(writer, execOptions =>
                {
                    execOptions.Query = "{ hero { id name } }";
                });

                Console.WriteLine(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public interface IDroid
    {
        string Id { get; set; }

        string Name { get; set; }
    }

    public class Droid
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class DroidType : ObjectGraphType<IDroid> // works when we do ObjectGraphType<Droid>
    {
        public DroidType()
        {
            Field(x => x.Id).Description("The Id of the Droid.");
            Field(x => x.Name).Description("The name of the Droid.");
        }
    }

    public class StarWarsQuery : ObjectGraphType
    {
        public StarWarsQuery()
        {
            Field<DroidType>(
              "hero",
              resolve: context => new Droid { Id = "1", Name = "R2-D2" }
            );
        }
    }
}
