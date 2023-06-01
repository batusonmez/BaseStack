using System.CommandLine;

namespace BsCli.Commands.Index
{
    public class IndexCommand : BaseCommand
    {
        public IndexCommand(RootCommand root) : base("index", "Index commands for data tables")
        {

            var createOption = new Option<string>(
              name: "--create",
              description: "Create Table Index");
            AddOption(createOption);

            this.SetHandler((indexName) =>
            {
                Handle(indexName);
            }, createOption);

            root.AddCommand(this);
        }

        void Handle(string table)
        {
            Console.WriteLine("Indexing: " + table);
        }
    }
}
