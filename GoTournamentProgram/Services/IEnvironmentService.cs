using System;
using System.Collections.Generic;

namespace GoTournamentProgram.Services
{
    public interface IEnvironmentService
    {
        IEnumerable<string> GetCommandLineArguments();
    }
}
