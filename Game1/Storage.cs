using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using NovaOrm;
namespace Slingshot
{
    public class Storage
    {
        NovaDb _db;

        public Storage(string host, string database, string user, string password)
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = host;
            csb.InitialCatalog = database;
            csb.UserID = user;
            csb.Password = password;
            _db = new NovaDb(csb.ToString());

            if (!_db.TableExists("Simulations"))
            {
                _db.Create("Simulations")
                    .Column("Id", "INT IDENTITY(1,1)")
                    .Column("Type", "varchar(250)")
                    .Column("StartTime", "DATETIME default CURRENT_TIMESTAMP")
                    .Column("EndTime", "DATETIME")
                    .Column("PopulationSize", "INT")
                    .Column("Duration", "INT")
                    .Column("MutationRate", "INT")
                    .Column("CrossoverRate", "INT")
                    .Column("Complexity", "INT")
                    .Column("SelectionPressure", "INT")
                    .Column("Generations", "INT")
                    .Column("Score", "INT")
                    .Column("Chromosome", "text")
                    .Execute();
            }
            if (!_db.TableExists("Generations"))
            {
                _db.Create("Generations")
                    .Column("Id", "INT IDENTITY(1,1)")
                    .Column("StartTime", "DATETIME default CURRENT_TIMESTAMP")
                    .Column("Generation", "INT")
                    .Column("Simulation", "INT")
                    .Column("FittestScore", "INT")
                    .Column("FittestChromosome", "text")
                    .Execute();
            }
        }

        public int NewSimulation(Configuration config, int generations)
        {
            var sim = _db.Table("Simulations", "Id").New();

            sim["Type"] = "Walker";
            sim["StartTime"] = DateTime.Now;
            sim["PopulationSize"] = config.PopulationSize.Value;
            sim["Duration"] = config.Duration.Value;
            sim["MutationRate"] = config.MutationRate.Value;
            sim["CrossoverRate"] = config.CrossoverRate.Value;
            sim["Complexity"] = config.Complexity.Value;
            sim["SelectionPressure"] = config.SelectionPressure.Value;
            sim["Generations"] = generations;

            return (int)sim.Save();
        }
        public void EndSimulation(int simulation, Animal fittest)
        {
            var sim = _db.Table("Simulations", "Id").Find(simulation);

            sim["EndTime"] = DateTime.Now;
            sim["Score"] = fittest.Fitness;
            sim["Chromosome"] = fittest.Chromosome.ToString();

            sim.Save();
        }
        public int SaveGeneration(int simulation, int generation, Animal fittest)
        {
            var gen = _db.Table("Generations", "Id").New();

            gen["Generation"] = generation;
            gen["Simulation"] = simulation;
            gen["FittestScore"] = fittest.Fitness;
            gen["FittestChromosome"] = fittest.Chromosome.ToString();

            return (int)gen.Save();
        }
    }
}
