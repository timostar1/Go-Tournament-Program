using System;
using System.Collections.Generic;

namespace GoTournamentProgram.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        public IEnumerable<string> GetCommandLineArguments()
        {
            return Environment.GetCommandLineArgs();
        }
    }
}
